using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class menu_func : MonoBehaviour
{
    public NetworkManager Manager1;

    public GameObject Check;
    public GameObject secretobject;
    public GameObject LoadSign;
    public Slider Post1;
    bool secretbool;
    bool Clicked = false;
    public float value1;

    public GameObject MainMenu;
    public GameObject PlayMenu;
    public GameObject SinglePlayerMenu;
    public GameObject MultiPlayerMenu;
    public GameObject SettingsMenu;
    public GameObject HostingMenu;

    public void Start()
    {
        Manager1 = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        if (Post1 != null)
        {
            PlayerPrefs.SetInt("sandbox", 0);
        }
        
        if (PlayerPrefs.GetInt("LoadLastSave") == 1)
        {
            Clicked = true;
            if(Check != null)
            Check.SetActive(true);
        }

        if (LoadSign == null)
        {
            return;
        }
        if (PlayerPrefs.GetInt("Multiplayer") == 1)
        {
            LoadSign.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Multiplayer") == 0)
        {
            LoadSign.SetActive(true);
        }
    }

    private void Update()
    {
        if (Post1 == null)
        {
            return;
        }
        value1 = Post1.value;
        if (PlayerPrefs.GetFloat("Post") == 1)
        {
            Post1.value = 1;
        }
        if (PlayerPrefs.GetFloat("Post") == 0)
        {
            Post1.value = 0;
        }
        if (value1 == 1){
            PlayerPrefs.SetFloat("Post", 1);
        }
        else
        {
            PlayerPrefs.SetFloat("Post", 0);
        }
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
        SettingsMenu.SetActive(false);
        HostingMenu.SetActive(false);
    }

    public void OpenMainMenu()
    {
        MainMenu.SetActive(true);
        PlayMenu.SetActive(false);
        SinglePlayerMenu.SetActive(false);
        MultiPlayerMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        HostingMenu.SetActive(false);
    }

    public void OpenSinglePlayerMenu()
    {
        MainMenu.SetActive(false);
        PlayMenu.SetActive(false);
        SinglePlayerMenu.SetActive(true);
        MultiPlayerMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        HostingMenu.SetActive(false);
    }
    public void OpenMultiPlayerMenu()
    {
        MainMenu.SetActive(false);
        PlayMenu.SetActive(false);
        SinglePlayerMenu.SetActive(false);
        MultiPlayerMenu.SetActive(true);
        SettingsMenu.SetActive(false);
        HostingMenu.SetActive(false);
    }

    public void OpenSettingsMenu()
    {
        MainMenu.SetActive(false);
        PlayMenu.SetActive(false);
        SinglePlayerMenu.SetActive(false);
        MultiPlayerMenu.SetActive(false);
        SettingsMenu.SetActive(true);
        HostingMenu.SetActive(false);
    }
    public void OpenHostingMenu()
    {
        MainMenu.SetActive(false);
        PlayMenu.SetActive(false);
        SinglePlayerMenu.SetActive(false);
        MultiPlayerMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        HostingMenu.SetActive(true);
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
        PlayerPrefs.SetInt("Multiplayer", 0);
        Manager1.StartHost();
        gameObject.SetActive(false);
    }

    public void JoinGame()
    {
        PlayerPrefs.SetInt("Multiplayer", 1);
        gameObject.SetActive(false);
        Manager1.StartClient();
    }

    public void Host()
    {
        OpenHostingMenu();
        Manager1.StartServer();
    }

    public void StopEverything(){
        if (NetworkServer.active && NetworkClient.isConnected)
        {
            Manager1.StopHost();
        }
        // stop client if client-only
        else if (NetworkClient.isConnected)
        {
            Manager1.StopClient();
            SceneManager.LoadScene(0);
        }
        // stop server if server-only
        else if (NetworkServer.active)
        {
            Manager1.StopServer();
        }
    }

    public void LoadLastSave()
    {
        Clicked = !Clicked;
        if (Clicked){
            PlayerPrefs.SetInt("LoadLastSave", 1);
            Check.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("LoadLastSave", 0);
            Check.SetActive(false);
        }
    }

    public void EnableSandbox()
    {
        PlayerPrefs.SetInt("sandbox", 1);
    }
    public void DisableSandbox()
    {
        PlayerPrefs.SetInt("sandbox", 0);
    }

    public void SetPost(int value)
    {
        PlayerPrefs.SetInt("Post", value);
    }

    public void SetName(string Name)
    {
        PlayerPrefs.SetString("name", Name);
    }
}
