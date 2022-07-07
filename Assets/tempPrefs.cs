using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class tempPrefs : MonoBehaviour
{
    public GameObject canvas;

    public bool sandbox;
    
    public void EnableSandbox()
    {
        sandbox = true;
        canvas.SetActive(false);
        gameObject.GetComponent<NetworkManager>().StartHost();
    }
}
