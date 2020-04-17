using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayFromCSV : MonoBehaviour
{
    // Do once
    void Start()
    {
        i = 0;
        pod = new POD();
        CSV.ReadTimeEventData("Sight_tracker/" + fileName, pod);
        T = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (i < pod.getLocationCount())
        {
            T.forward = pod.getLookingAtObject(i);
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
