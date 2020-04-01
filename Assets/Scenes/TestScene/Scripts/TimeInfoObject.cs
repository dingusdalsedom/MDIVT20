using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeInfoObject : MonoBehaviour
{
    public float timeLookedAt;      //total time looked at this object (seconds)
    public List<string> timeStamps;     //all timestamps (min:sec) when object is looked at
    public string timeStamp;        //current timestamp when the object was look at

    // Start is called before the first frame update
    void Start()
    {
        timeLookedAt = 0;
        timeStamps = new List<string>();
        timeStamp = "0:00";
    }

}
