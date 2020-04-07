using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetMove : MonoBehaviour
{
    Vector3 pos = new Vector3();
    bool direction = true;
    public float speed = 0.05f;

    void Start()
    {
        pos = gameObject.transform.localPosition;
    }

    void Update()
    {
        // 44 to 180
        //Vector3 pos = gameObject.transform.position;
        
        if(direction)
        {
            pos.x = pos.x + speed;
        }
        else
        {
            pos.x = pos.x - speed;
        }

        gameObject.transform.localPosition = pos; 
        
        if(pos.x > 180f)
        {
            direction = false;
        }
        if(pos.x < 44)
        {
            direction = true;
        }

    }
}
