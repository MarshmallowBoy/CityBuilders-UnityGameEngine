using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
public class Stats : NetworkBehaviour
{
    public Text ConPlayers;

    private void FixedUpdate()
    {
        ConPlayers.text = "Amount Of Connected Players: " + NetworkServer.connections.Values.Count;
    }
}
