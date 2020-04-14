using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SightTracker : MonoBehaviour
{
    // Start is called before the first frame update
    public bool debugView;
    private POD collectedData;
    private GameObject cam_gameobject;
    private debugText debug_text_hud;
    private Raycast raycaster;
    private string previous_look_at = "";
    public string DebugViewButton = "None";
    KeyCode debugkey;
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
        debugkey = interpretDebugKey(DebugViewButton);
        cam_gameobject = this.gameObject;
        debug_text_hud = cam_gameobject.AddComponent<debugText>();
        collectedData = new POD();
        raycaster = this.gameObject.AddComponent<Raycast>();
    }

    // Update is called once per frame
    void Update()
    {
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

        // Store location and looking at vector every frame
        collectedData.addLookingAtVector(raycaster.getCurrentlyLookingAt());
        collectedData.addCurrentLocation(raycaster.getCurrentLocation());

        //Checks if new object was looked at
        if((raycaster.get_currently_looking_at() != previous_look_at))
        {
            collectedData.addTimedObject(raycaster.get_currently_looking_at());
            previous_look_at = raycaster.get_currently_looking_at();
        }

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
