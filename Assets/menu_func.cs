using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class menu_func : MonoBehaviour
{
    public NetworkManager Manager1;

    public GameObject secretobject;
    bool secretbool;

    public GameObject MainMenu;
    public GameObject PlayMenu;
    public GameObject SinglePlayerMenu;
    public GameObject MultiPlayerMenu;

    public void Start()
    {
        Manager1 = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }

    public void secret()
    {
        secretbool = !secretbool;
        if (secretbool){
            secretobject.GetComponent<RectTransform>().eulerAngles = new Vector3(180, 0, 0);
        }
        else
        {
            secretobject.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 0);
        }
        return;
    }

    public void OpenPlayMenu()
    {
        MainMenu.SetActive(false);
        PlayMenu.SetActive(true);
        SinglePlayerMenu.SetActive(false);
        MultiPlayerMenu.SetActive(false);
    }

    public void OpenMainMenu()
    {
        MainMenu.SetActive(true);
        PlayMenu.SetActive(false);
        SinglePlayerMenu.SetActive(false);
        MultiPlayerMenu.SetActive(false);
    }

    public void OpenSinglePlayerMenu()
    {
        MainMenu.SetActive(false);
        PlayMenu.SetActive(false);
        SinglePlayerMenu.SetActive(true);
        MultiPlayerMenu.SetActive(false);
    }
    public void OpenMultiPlayerMenu()
    {
        MainMenu.SetActive(false);
        PlayMenu.SetActive(false);
        SinglePlayerMenu.SetActive(false);
        MultiPlayerMenu.SetActive(true);
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetIp(string IP)
    {
        Manager1.networkAddress = IP;
    }
    public void Play_Normal()
    {
        Manager1.StartHost();
        gameObject.SetActive(false);
    }

    public void JoinGame()
    {
        Manager1.StartClient();
    }

    public void Host()
    {
        Manager1.StartHost();
    }

    public void StopEverything(){
        Manager1.StopClient();
        Manager1.StopHost();
        Manager1.StopServer();
    }

}
