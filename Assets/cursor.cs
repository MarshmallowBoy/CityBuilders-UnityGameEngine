using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursor : MonoBehaviour
{
    public float x = 0;
    public float y = 0;
    private void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        x = Input.mousePosition.x / 60;
        y = Input.mousePosition.y / 60;
        gameObject.transform.position = new Vector3(x - 16, y - 9, 1);
        Debug.Log("X: " + Input.mousePosition.x + " Y: " + Input.mousePosition.y);
    }
}
