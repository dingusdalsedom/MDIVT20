using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public Camera fpsCam;
    public GameObject looking_at;
    public float time_looked_at;
    public string currently_looking_at = "Starting...";

    void Start()
    {
        Update();
        //prevName = "";
    }
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        // if player is looking at something (if the casted ray hit something)
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
        {
            currently_looking_at = hit.collider.gameObject.name;
            looking_at = hit.collider.gameObject;
            currently_looking_at = looking_at.name;

            //Debug.Log(hit.transform.name);     // print name of object in debug 
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
