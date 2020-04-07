using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class debugText : MonoBehaviour
{
    private Canvas can;
    private Text debug_text;
    public bool toggle_debug = true;
    public string currently_looking_at;
    public string duration_looked_at;
    // Start is called before the first frame update
    void Start()
    {
        GameObject canvObj = new GameObject("Canvas");
        canvObj.transform.SetParent(this.transform);
        can = canvObj.AddComponent<Canvas>();
        RectTransform canvTransform = can.GetComponent<RectTransform>();
        can.renderMode = RenderMode.ScreenSpaceOverlay;

        GameObject txtObj = new GameObject("Text");
        debug_text = txtObj.AddComponent<Text>();
        debug_text.rectTransform.sizeDelta = new Vector2(canvTransform.rect.width*0.99f, canvTransform.rect.height*0.99f);
        txtObj.transform.SetParent(can.transform, false);
        debug_text.font = Font.CreateDynamicFontFromOSFont("Arial", 4);
        debug_text.alignment = TextAnchor.UpperLeft;
        debug_text.color = Color.black;
        debug_text.text = "";

    }

    // Update is called once per frame
    void Update()
    {
        if(toggle_debug)
        {
            debug_text.text = "Sight tracker debug information: \n" +
            "Currently looking at: " + currently_looking_at + "\n" +
            "Duration looked at: " + duration_looked_at + "\n";
        }
        else
        {
            debug_text.text = "";
        }
        
    }
}
