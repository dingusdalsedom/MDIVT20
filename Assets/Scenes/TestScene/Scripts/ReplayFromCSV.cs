using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayFromCSV : MonoBehaviour
{
    /*
     * Quadratic interpolation of three points.
     */
    private void ThreePoint(bool list, Vector3 a, Vector3 b, Vector3 c) {
        // list => looking at
        // !list => location
        for(float i = 0.0f; i <= 1.0f; i += 1/InterpolationSteps)
        {
            Vector3 newVec = new Vector3();
            newVec.x = (1 - i) * (1 - i) * a.x + 2 * (1 - i) * i * b.x + i * i * c.x;
            newVec.y = (1 - i) * (1 - i) * a.y + 2 * (1 - i) * i * b.y + i * i * c.y;
            newVec.z = (1 - i) * (1 - i) * a.z + 2 * (1 - i) * i * b.z + i * i * c.z;
            if (list)
                interpolatedView.Add(newVec);
            else
                interpolatedLocations.Add(newVec);
        }
    }

    /*
     * Two point (linear) interpolation using bezier stuff
     */
    private void TwoPoint(bool list, Vector3 a, Vector3 b) {
        // list => looking at
        // !list => location
        for(float i = 0.0f; i <= 1.0f; i += 1/InterpolationSteps)
        {
            Vector3 newVec = new Vector3();
            newVec.x = (1 - i) * a.x + i * b.x;
            newVec.y = (1 - i) * a.y + i * b.y;
            newVec.z = (1 - i) * a.z + i * b.z;
            if (list)
                interpolatedView.Add(newVec);
            else
                interpolatedLocations.Add(newVec);
        }
    }

    private void InterpolateData(POD pod)
    {
        int sz = pod.getLocationCount();
        for(int i = 0; i < sz; i = i + 2)
        {
            if(i + 2 < sz)
            {
                ThreePoint(true,
                           pod.getLookingAtObject(i),
                           pod.getLookingAtObject(i + 1),
                           pod.getLookingAtObject(i + 2)); 
                ThreePoint(true,
                           pod.getLocationObject(i),
                           pod.getLocationObject(i + 1),
                           pod.getLocationObject(i + 2));
            }
            else
            {
                if (i + 1 < sz)
                {
                    TwoPoint(true,
                             pod.getLookingAtObject(i),
                             pod.getLookingAtObject(i + 1));
                    TwoPoint(false,
                             pod.getLookingAtObject(i),
                             pod.getLookingAtObject(i + 1));
                }
            }
        }
    }

    void Start()
    {
        i = 0;
        pod = new POD();
        CSV.ReadTimeEventData("Sight_tracker/" + fileName, pod);
        interpolatedLocations = new List<Vector3>();
        interpolatedView = new List<Vector3>();
        InterpolateData(pod);
        Debug.Log(interpolatedView.Count);
        Debug.Log(interpolatedLocations.Count);
        camControl = this.gameObject.GetComponent<Camera>();
        Debug.Log(camControl);
    }

    // Update is called once per frame
    void Update()
    {
        if (i < interpolatedLocations.Count)
        {
            T.forward = interpolatedView[i];
            T.position = interpolatedLocations[i];
        }
        i++;
    }
    
    public string fileName;
    public float InterpolationSteps;
    private POD pod;
    private int i;
    private List<Vector3> interpolatedLocations;
    private List<Vector3> interpolatedView;
    private Camera camControl;
    public Transform T;
}
