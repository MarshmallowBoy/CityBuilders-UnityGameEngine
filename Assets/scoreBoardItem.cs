using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
public class scoreBoardItem : MonoBehaviour
{
    public Text UsernameText;
    public void Initialize(string vc)
    {
        UsernameText.text = vc;
    }
}
