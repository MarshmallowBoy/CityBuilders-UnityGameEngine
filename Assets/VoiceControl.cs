using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
public class VoiceControl : NetworkBehaviour
{
    public GameObject VoiceTrans;
    public string nickName = "%name%";
    private void Start()
    {
        nickName = PlayerPrefs.GetString("name");
    }
    void Update()
    {
        VoiceTrans.SetActive(Input.GetKey(KeyCode.V));

        if (Input.GetKeyDown(KeyCode.V)) {
            
        }
        if (Input.GetKeyUp(KeyCode.V)){

        }
        
    }
}
