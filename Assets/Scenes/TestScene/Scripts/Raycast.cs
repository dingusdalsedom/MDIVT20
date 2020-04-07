using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public Camera fpsCam;
    public string prevName;     // name of previous looked at object

    void Start()
    {
        prevName = "";
    }
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        // if player is looking at something (if the casted ray hit something)
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
        {
            var selection = hit.transform;
            var selectionInfo = selection.GetComponent<TimeInfoObject>();   // access the script component of object 

            if (selectionInfo != null)
            {
                selectionInfo.timeLookedAt += Time.deltaTime;   // add the time 

                // check if new object 
                if (prevName != hit.transform.name)     
                {
                    selectionInfo.timeStamps.Add(timeToText(Time.time));
                    selectionInfo.timeStamp = timeToText(Time.time);
                }
            }
            prevName = hit.transform.name;
            Debug.Log(hit.transform.name);     // print name of object in debug 
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
