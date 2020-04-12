using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    private Camera fpsCam;
    private string currently_looking_at = "Starting...";
    public string get_currently_looking_at()
    {
        return currently_looking_at;
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

    // formating seconds ellapsed to a string time stamp (min:sec)
    string timeToText(float t)
    {
        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");

        return minutes + ":" + seconds;
    }
}
