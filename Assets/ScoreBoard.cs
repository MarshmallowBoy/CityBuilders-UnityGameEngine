using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
public class ScoreBoard : NetworkBehaviour
{
    [SerializeField] Transform container;
    [SerializeField] GameObject scoreboardItemPrefab;
    

    float nexttimetofire = 0;

    private void Update()
    {
        if (nexttimetofire < Time.time)
        {
            nexttimetofire = Time.time + 5;
            ClearScoreBoard();
            Refresh();
        }
    }

    [Command]
    public void Refresh()
    {
        //ClearScoreBoard();
        foreach (var connections in NetworkServer.connections.Values)
        {
            AddScoreBoardItem(connections.identity.GetComponent<VoiceControl>().nickName);
        }
    }

    [ClientRpc]
    void AddScoreBoardItem(string vc)
    {
        scoreBoardItem item = Instantiate(scoreboardItemPrefab, container).GetComponent<scoreBoardItem>();
        item.Initialize(vc);
    }

    void ClearScoreBoard()
    {
        foreach (Transform child in container.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
