using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SightTracker : MonoBehaviour
{
    // Public variables (can be set through Unity editor)
    public bool debugView;
    public string DebugViewButton = "None";

    // Private member functions
    private POD collectedData;
    private GameObject cam_gameobject;
    private debugText debug_text_hud;
    private Raycast raycaster;
    private string previous_look_at = "";
    private double currentSampleTime = 0;
    private uint counter = 0;
    private int rate = 1;
    KeyCode debugkey;


    //Enum for selection of record speed
    public enum Speeds
    { Full = 1, Half = 2, Quarter = 4 };

    //Default speed is full speed
    public Speeds RecordRate = Speeds.Full;
    
    /*
     * Function that sanitizes user input and attempts to map
     * it to the corresponding Unity KeyCode.
     * 
     * DISCLAIMER: User *MUST* supply Unity KeyCode for this to work.
     */
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

    // Start runs once
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

    // Grabs position and oriantation data from camera and accumulated frametime and adds it to POD
    private void grab_pos_data()
    {
        currentSampleTime += Time.deltaTime;
        if ((counter%rate)==0)
        {
            collectedData.addLookingAtVector(raycaster.getCurrentDirection());
            collectedData.addCurrentLocation(raycaster.getCurrentLocation());
            collectedData.addFrameTime(currentSampleTime);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        // Used to toggle Debug HUD
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
