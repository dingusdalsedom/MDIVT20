using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayFromCSV : MonoBehaviour
{
    // Do once
    void Start()
    {
        i = 0;
        // Create new POD for data storage
        pod = new POD();
        // Read Time Event Data from Filename
        CSV.ReadTimeEventData("Sight_tracker/" + fileName, pod);
        // Set the transform to the *camera* transform (since we record camera position and direction)
        T = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Iterate though the read lists
        if (i < pod.getLocationCount())
        {
            // Set the forward vector (or view)
            T.forward = pod.getLookingAtObject(i);
            // Set the current position of the camera (MUST be camera)
            T.position = pod.getLocationObject(i);
        }
        i++;
    }
    
    // Used to specify which CSV to load
    public string fileName;
    private POD pod;
    private int i;
    private Transform T;
}
