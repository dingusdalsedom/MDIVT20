using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    private Camera fpsCam;
    //Default text when no target is found
    private string currently_looking_at = "Starting...";
    public string get_currently_looking_at()
    {
        return currently_looking_at;
    }

    public Vector3 getCurrentlyLookingAt()
    {
        return fpsCam.transform.forward;
    }

    public Vector3 getCurrentLocation()
    {
        return fpsCam.transform.position;
    }

    void Start()
    {
        fpsCam = this.gameObject.GetComponent<Camera>();
        Update();
    }
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        // if player is looking at something (if the casted ray hit something)
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
        {
            currently_looking_at = hit.collider.gameObject.name;
        }
        else
        {
            currently_looking_at = "no target";
        }
    }
}
