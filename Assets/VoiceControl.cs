using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
public class VoiceControl : NetworkBehaviour
{
    public GameObject VoiceTrans;

    [SyncVar] public string nickName = "%name%";

    public override void OnStartClient()
    {
        SomeCommandFromClientToServer(PlayerPrefs.GetString("name"));
        base.OnStartClient();
    }

    [Command]
    private void SomeCommandFromClientToServer(string data)
    {
        nickName = data;
    }

    void Update()
    {
        //VoiceTrans.SetActive(Input.GetKey(KeyCode.V));

        if (Input.GetKeyDown(KeyCode.V)) {
            
        }
        if (Input.GetKeyUp(KeyCode.V)){

        }
        
    }
}
