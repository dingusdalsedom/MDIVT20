using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SightTracker : MonoBehaviour
{
    // Start is called before the first frame update
    public bool debugView;
    private POD collectedData;
    private float timeElapsed;
    private GameObject cam_gameobject;
    private debugText debug_text_hud;
    private Raycast raycaster;
    private string previous_look_at = "";
    public string DebugViewButton = "None";
    private double currentSampleTime = 0;
    KeyCode debugkey;
    public enum Speeds
    { Full = 1, Half = 2, Quarter = 4 };
    public Speeds RecordRate;
    // Object polling things
    private uint counter = 0;
    private int rate = 1;
    //public bool check = (RecordRate == Speeds.Full);
    
    private KeyCode interpretDebugKey(string buttonName)
    {
        try
        {
            char[] tempCharArray = buttonName.ToCharArray();
            tempCharArray[0] = char.ToUpper(tempCharArray[0]);
            for (int i = 1; i < buttonName.Length; i++)
            {
                tempCharArray[i] = char.ToLower(tempCharArray[i]);
            }
            string finalButtonName = new string(tempCharArray);
            DebugViewButton = finalButtonName;
            KeyCode finalKeycode = (KeyCode)System.Enum.Parse(typeof(KeyCode), finalButtonName);
            return finalKeycode;
        }
        catch
        {
            Debug.LogError("Unable to define Debug View Button, Please use Unity KeyCodes for button specification.");
            return KeyCode.None;
        }
        
    }
    void Start()
    {
        rate = (int)RecordRate;
        //Debug.Log(rate);
        debugkey = interpretDebugKey(DebugViewButton);
        cam_gameobject = this.gameObject;
        debug_text_hud = cam_gameobject.AddComponent<debugText>();
        collectedData = new POD();
        raycaster = this.gameObject.AddComponent<Raycast>();
    }

    /*
     * Function for adding an object to the POD based on interval specified by the user in MS
     * 
     * DISCLAIMER: This does NOT work for incredibly low ms values since Time.deltaTime depends on the users computer
     */
    //private void AddObject()
    //{
    //    if(IntervalInMS == 0.0f)
    //    {
    //        collectedData.addLookingAtVector(raycaster.getCurrentlyLookingAt());
    //        collectedData.addCurrentLocation(raycaster.getCurrentLocation());
    //    }
    //    else
    //    {
    //        if(this.doOnce == false) // First iteration just set the upperbound based on user selected Interval specified in MS 
    //        {
    //            doOnce = true;
    //            upperBound += IntervalInMS / 1000;
    //        }
    //        else
    //        {
    //            // Grab the first object in any given time span and increase the interval to select from
    //            if (timeElapsed >= lowerBound && timeElapsed <= upperBound)
    //            {
    //                // Add what the user is looking at
    //                collectedData.addLookingAtVector(raycaster.getCurrentlyLookingAt());
    //                // Add the users current location
    //                collectedData.addCurrentLocation(raycaster.getCurrentLocation());
    //                // Increase lower and upperbound
    //                lowerBound += IntervalInMS / 1000;
    //                upperBound += IntervalInMS / 1000;
    //            }
    //        }
    //    }
    //}
    private void grab_pos_data()
    {
        if((counter%rate)==0)
        {
            collectedData.addLookingAtVector(raycaster.getCurrentlyLookingAt());
            collectedData.addCurrentLocation(raycaster.getCurrentLocation());
            collectedData.addFrameTime(currentSampleTime + Time.deltaTime);
            //currentSampleTime = 0;
        }
        else
        {
            currentSampleTime += Time.deltaTime;
        }
    }
    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (Input.GetKeyDown(debugkey))
        {
            debugView = !debugView;
        }
        //Set HUD if debug is turned on
        debug_text_hud.set_debug_mode(debugView);
        if (debugView)
        {
            debug_text_hud.set_currently_looking_at(raycaster.get_currently_looking_at());
            debug_text_hud.set_duration_looked_at(collectedData.getCurrentStopwatchTime().ToString());
            debug_text_hud.set_nr_objects_looked_at(collectedData.nr_of_objects_looked_at());
        }

        // Adds object to list, see separate function
        //AddObject();
        grab_pos_data();

        //Checks if new object was looked at
        if((raycaster.get_currently_looking_at() != previous_look_at))
        {
            collectedData.addTimedObject(raycaster.get_currently_looking_at());
            previous_look_at = raycaster.get_currently_looking_at();
        }
        counter++;
    }
    private void OnApplicationQuit()
    {
        //Prints collected data into txt file.
        if(this.enabled)
        {
            collectedData.stopTimer();
            var folder = Directory.CreateDirectory("Sight_tracker");
            CSV.WriteSequential("Sight_tracker/CSVSequential.txt", collectedData);
            CSV.WriteSummary("Sight_tracker/CSVSummary.txt", collectedData);
            CSV.WriteTimeEventData("Sight_tracker/CSVTimeData.txt", collectedData);
        }
        
    }
}
