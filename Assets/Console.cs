using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Console : MonoBehaviour
{
    public GameObject Image;
    public GameObject Helpsheet;
    public GameObject Console1;
    public InputField input1;
    public AudioSource Music;
    bool Tilde = false;
    bool HelpSheet = false;
    bool Image1 = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            Tilde = !Tilde;
            Console1.SetActive(Tilde);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HelpSheet = !HelpSheet;
            Helpsheet.SetActive(HelpSheet);
        }
    }
    public void OnEndEdit1(){
        string inText = input1.text;
        float fl = 0;
        float.TryParse(inText, out fl);
        if (float.TryParse(inText, out fl))
        {
            gameObject.GetComponent<tilemapSorter>().Money = fl;
        }
    }

    public void MusicMute()
    {
        Image1 = !Image1;
        Image.SetActive(Image1);
        Music.mute = Image1;
    }
}
