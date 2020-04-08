using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetMove : MonoBehaviour
{
    Vector3 pos = new Vector3();
    bool direction = true;
    public float speed = 0.05f;
    public char movementDirection = 'x';

    void Start()
    {
        pos = gameObject.transform.localPosition;
    }

    void Update()
    {
        // 180 and 44 is the coordinates in X for the "room" of the shooting range.
        // 130 and 82 is the coordinates in X for the "room" of the shooting range.
        // 330 and 161 is the coordinates in Z for the "room" of the shooting range.

        if (movementDirection == 'x')
        {
            if(direction)
            {
                pos.x = pos.x + speed;
            }
            else
            {
                pos.x = pos.x - speed;
            }
            if (pos.x > 180f)
            {
                direction = false;
            }
            if (pos.x < 44f)
            {
                direction = true;
            }
        }

        else if (movementDirection == 'y')
        {
            if (direction)
            {
                pos.y = pos.y + speed;
            }
            else
            {
                pos.y = pos.y - speed;
            }
            if (pos.y > 130f)
            {
                direction = false;
            }
            if (pos.y < 82f)
            {
                direction = true;
            }
        }

        else if (movementDirection == 'z')
        {
            if (direction)
            {
                pos.z = pos.z + speed;
            }
            else
            {
                pos.z = pos.z - speed;
            }
            if (pos.z > 330f)
            {
                direction = false;
            }
            if (pos.z < 161f)
            {
                direction = true;
            }
        }
               
        gameObject.transform.localPosition = pos; 
        
        
        
    }
}
