using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class varTracker : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnChanged1))]
    public int PlayersConnected;

    [ServerCallback]
    private void Update()
    {
        PlayersConnected = NetworkManager.singleton.numPlayers;
    }

    void OnChanged1(int _Old, int _New)
    {
        Debug.Log("PlayersConnected:" + PlayersConnected);
    }
}
