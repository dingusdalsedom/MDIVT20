using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightTracker : MonoBehaviour
{
    // Start is called before the first frame update
    public bool debugView;
    private POD collectedData;
    private GameObject cam_gameobject;
    private debugText debug_text_hud;
    private Raycast raycaster;
    private string previous_look_at = "";
    void Start()
    {
        cam_gameobject = this.gameObject;
        debug_text_hud = cam_gameobject.AddComponent<debugText>();
        collectedData = new POD();

        raycaster = this.gameObject.AddComponent<Raycast>();
        raycaster.fpsCam = cam_gameobject.GetComponent<Camera>();


    }

    // Update is called once per frame
    void Update()
    {
        //Set HUD if debug is turned on
        if(debugView)
        {
            debug_text_hud.toggle_debug = true;
            debug_text_hud.currently_looking_at = raycaster.currently_looking_at;
            debug_text_hud.duration_looked_at = collectedData.getCurrentStopwatchTime().ToString();
            debug_text_hud.nr_objects_looked_at = collectedData.nr_of_objects_looked_at();
        }
        //Checks if new object was looked at
        if((raycaster.currently_looking_at != previous_look_at))
        {
            collectedData.addTimedObject(raycaster.currently_looking_at);
            previous_look_at = raycaster.currently_looking_at;
        }

    }
    private void OnApplicationQuit()
    {
        //Prints collected data into txt file.
        if(this.enabled)
        {
            collectedData.stopTimer();
            CSV.WriteSequential("Assets/Scenes/TestScene/Resources/CSVSequential.txt", collectedData);
            CSV.WriteSummary("Assets/Scenes/TestScene/Resources/CSVSummary.txt", collectedData);
        }
        
    }
}
