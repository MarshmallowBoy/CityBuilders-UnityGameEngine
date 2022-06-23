using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableEverySeconds : MonoBehaviour
{
    public GameObject RainGen;
    public float Waiting_Time;
    float nextTimeToFire;
    bool Rain = false;
    void Update()
    {
        if (Time.time > nextTimeToFire)
        {
            nextTimeToFire = Time.time + Waiting_Time;
            RainGen.SetActive(Rain);
            Rain = !Rain;
        }
    }
}
