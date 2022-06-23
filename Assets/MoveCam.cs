using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && transform.localPosition.x >= -64)
        {
            transform.Translate(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow) && transform.localPosition.x <= 3f)
        {
            transform.Translate(1, 0, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow) && transform.localPosition.y <= 15f)
        {
            transform.Translate(0, 1, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow) && transform.localPosition.y >= -50.5f)
        {
            transform.Translate(0, -1, 0);
        }
    }
}
