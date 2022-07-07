using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Mirror;
using System.IO;
public class tilemapSorter : NetworkBehaviour
{
    public Grid grid;
    public Tilemap default1;
    public Tilemap default2;
    public Tile[] tile;
    public GameObject[] UITile;
    public Camera cam;
    public int ActiveTile;
    int lastActiveTile = 0;
    public Text UIMoney;
    public float Money;
    public float nextTimeToFire = 10f;
    int ExistingHaus = 0;
    public bool Eraser = false;
    public float HausCost;
    public AudioSource GetCash;
    public AudioSource Purchase;
    public float ComplexCost;
    int ExistingComplex = 0;
    int tileID = 6;
    int ExistingFarms = 0;

    public GameObject cam2;

    public GameObject WantHelp;

    public GameObject Bombfire;

    private void Start()
    {
        grid = GameObject.Find("Grid").GetComponent<Grid>();
        default1 = GameObject.Find("Tilemap2").GetComponent<Tilemap>();
        default2 = GameObject.Find("Tilemap3").GetComponent<Tilemap>();
        cam2 = GameObject.Find("cam2");
        cam2.SetActive(false);
        if (!isLocalPlayer)
            cam.gameObject.SetActive(false);
        GameObject MyManager1 = GameObject.Find("NetworkManager");
        nextTimeToFire = Time.time + 10f;

        if (PlayerPrefs.GetFloat("Post") == 0)
        {
            GameObject.Find("Global Volume").SetActive(false);
        }
        if(PlayerPrefs.GetFloat("Post") == 1)
        {
            GameObject.Find("Global Volume").SetActive(true);
        }
        
    }

    void Update()
    {
        if (!isLocalPlayer)
            cam.gameObject.SetActive(false);

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (ActiveTile == tile.Length - 1)
            {
                ActiveTile = 0;
            }
            else
            {
                ActiveTile++;
            }

        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (ActiveTile == 0)
            {
                ActiveTile = tile.Length - 1;
            }
            else
            {
                ActiveTile--;
            }
        }
        UIMoney.text = "" + Money;
        if (Time.time > nextTimeToFire)
        {
            nextTimeToFire = Time.time + 10f;
            Money += ExistingHaus * 100;
            Money += ExistingComplex * 200;
            Refresh();
        }
        Vector3Int mousePos = GetMousePos();
        CheckTileSelection();
        if (Input.GetMouseButtonDown(0))
        {

            WantHelp.SetActive(false);
            if (Eraser == true)
            {
                if (HausCheck(mousePos, 0) == true) { return; }
                if (HausCheck(mousePos, 5) == true) { return; }

                if (default1.GetTile(mousePos) == tile[0]) { ExistingHaus--; }
                if (default1.GetTile(mousePos) == tile[5]) { ExistingComplex--; }
                if (default1.GetTile(mousePos) == tile[7]) { ExistingFarms--; }

                if (default1.GetTile(mousePos) != tile[6]) { Money += 100; }
                SendData(6, mousePos, 0);
                return;
            }

            if (default1.GetTile(mousePos) != tile[6]) { return; }

            if (PlayerPrefs.GetInt("sandbox") == 1)
            {
                if (ActiveTile == 0)
                {
                    ExistingHaus++;
                }
                if (ActiveTile == 5)
                {
                    ExistingComplex++;
                }
                if (ActiveTile == 7)
                {
                    ExistingFarms++;
                }
                if (ActiveTile == 8)
                {
                    StartCoroutine(SpawnFire(mousePos));
                }
                SendData(ActiveTile, mousePos, 0);
                return;
            }

            if (ActiveTile == 0)
                if (CheckHaus(mousePos) == false || Money < HausCost || ExistingFarms/2 < ExistingHaus + 1) { return; } else {
                    ExistingHaus++;
                    Money -= HausCost;
                    Purchase.PlayOneShot(Purchase.clip);
                    SendData(0, mousePos, 0);
                    return;
                }

            if (ActiveTile == 5)
                if (CheckHaus(mousePos) == false || Money < ComplexCost) { return; } else
                {
                    ExistingComplex++;
                    Money -= ComplexCost;
                    Purchase.PlayOneShot(Purchase.clip);
                    SendData(5, mousePos, 0);
                    return;
                }

            if (Money < 100 || ActiveTile == 0 || ActiveTile == 6)
                return;


            if (ActiveTile == 7) {
                if (Scan(mousePos, 4, 1) == true && Money >= 300)
                {
                    SendData(7, mousePos, 0);
                    ExistingFarms++;
                    Money -= 300;
                    return;
                }
                else
                {
                    return;
                }
            }

            if (ActiveTile == 8)
            {
                if (Money >= 300000)
                {
                    SendData(8, mousePos, 0);
                    StartCoroutine(SpawnFire(mousePos));
                    Money -= 300000;
                    return;
                }
                else
                {
                    return;
                }
            }

            Money -= 100;
            

            SendData(ActiveTile, mousePos, 0);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            WantHelp.SetActive(false);
        }
    }

    [Command]
    void Refresh()
    {
        if (!isServer)
        {
            return;
        }
        for (int k = -24; k < 25; k++)
        {
            for (int i = -31; i < 32; i++)
            {
                for (int l = 0; l < 9; l++)
                {
                    if (default1.GetTile(new Vector3Int(i, k, 0)) == tile[l])
                    {
                        tileID = l;
                    }
                }
                Vector3Int vect = new Vector3Int(i, k, 0);
                rpcsend(vect, tileID);
            }
        }
    }

    bool Scan(Vector3Int Position, int Radius, int tileID, bool Placing = false, bool Erasing = false)
    {
        bool Near = false;
        for (int k = -Radius; k < Radius + 1; k++)
        {
            for (int i = -Radius; i < Radius + 1; i++)
            {
                if (default1.GetTile(Position + new Vector3Int(i, k, 0)) == tile[tileID])
                {
                    Near = true;
                }
                if (Placing)
                {
                    SendData(tileID, Position + new Vector3Int(i, k, 0), 0);
                }
                if (Erasing)
                {
                    if (default1.GetTile(Position + new Vector3Int(i, k, 0)) == tile[0])
                    {
                        ExistingHaus--;
                    }
                    if (default1.GetTile(Position + new Vector3Int(i, k, 0)) == tile[5])
                    {
                        ExistingComplex--;
                    }
                }
            }
        }
        return Near;
    }


    bool CheckHaus(Vector3Int mousePos)
    {
        bool returnBool = false;
        for (int i = 2; i < 5; i++)
        {
            if (default1.GetTile(mousePos + new Vector3Int(1, 0, 0)) == tile[i]) { returnBool = true; }
            if (default1.GetTile(mousePos + new Vector3Int(-1, 0, 0)) == tile[i]) { returnBool = true; }
            if (default1.GetTile(mousePos + new Vector3Int(0, 1, 0)) == tile[i]) { returnBool = true; }
            if (default1.GetTile(mousePos + new Vector3Int(0, -1, 0)) == tile[i]) { returnBool = true; }
        }
        return returnBool;
    }

    bool HausCheck(Vector3Int mousePos, int tileignore)
    {
        bool returnBool = false;
        for (int i = 2; i < 5; i++)
        {
            if (default1.GetTile(mousePos) == tile[i] && default1.GetTile(mousePos + new Vector3Int(1, 0, 0)) == tile[tileignore]) { returnBool = true; }
            if (default1.GetTile(mousePos) == tile[i] && default1.GetTile(mousePos + new Vector3Int(-1, 0, 0)) == tile[tileignore]) { returnBool = true; }
            if (default1.GetTile(mousePos) == tile[i] && default1.GetTile(mousePos + new Vector3Int(0, 1, 0)) == tile[tileignore]) { returnBool = true; }
            if (default1.GetTile(mousePos) == tile[i] && default1.GetTile(mousePos + new Vector3Int(0, -1, 0)) == tile[tileignore]) { returnBool = true; }
        }
        return returnBool;
    }

    [Command]
    public void Save(){
        if (!isServer)
        {
            return;
        }
        StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/save.txt");
        sw.AutoFlush = true;
        for (int k = -24; k < 25; k++)
        {
            for (int i = -31; i < 32; i++)
            {
                for (int l = 0; l < 9; l++)
                {
                    if (default1.GetTile(new Vector3Int(i, k, 0)) == tile[l])
                    {
                        tileID = l;
                    }
                }
                Vector3Int vect = new Vector3Int(i, k, 0);
                sw.WriteLine(tileID);
            }
        }
    }

    [Command]
    public void Load()
    {
        StreamReader sr = new StreamReader(Application.persistentDataPath + "/save.txt");
        for (int k = -24; k < 25; k++)
        {
            for (int i = -31; i < 32; i++)
            {
                Vector3Int vect = new Vector3Int(i, k, 0);
                tileID = int.Parse(sr.ReadLine());
                rpcsend(vect, tileID);
            }
        }

    }

    public static Vector3Int StringToVector3(string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3
        Vector3Int result = new Vector3Int(
            int.Parse(sArray[0]),
            int.Parse(sArray[1]),
            int.Parse(sArray[2]));

        return result;
    }

    [ClientRpc]
    void rpcsend(Vector3Int pos, int id)
    {
        default1.SetTile(pos, tile[id]);
        if (!isLocalPlayer)
        {
            UIMoney.gameObject.SetActive(false);
        }
    }

    void CheckTileSelection()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //Haus
            ActiveTile = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //Lake
            ActiveTile = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //Intersection
            ActiveTile = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            //Vertical Road
            ActiveTile = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            //Horizontal Road
            ActiveTile = 4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            //Building Complex
            ActiveTile = 5;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Eraser = !Eraser;
            if (Eraser) { lastActiveTile = ActiveTile; }
            if (Eraser == false)
            {
                ActiveTile = lastActiveTile;
                return;
            }
            ActiveTile = 6;
        }
        if (ActiveTile != 6)
        {
            Eraser = false;
        }
        else
        {
            Eraser = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            //Farm
            ActiveTile = 7;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            //Bomb
            ActiveTile = 8;
        }
        DisableAll();
        UITile[ActiveTile].SetActive(true);
    }

    void DisableAll()
    {
        for (int i = 0; i < UITile.Length; i++)
        {
            UITile[i].SetActive(false);
        }
    }

    Vector3Int GetMousePos()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return grid.WorldToCell(mouseWorldPos);
    }

    [Command]
    void SendData(int ActiveTile, Vector3Int mousePos, int default5)
    {
        if (default5 == 0)
            default1.SetTile(mousePos, tile[ActiveTile]);

        if (default5 == 1)
            default2.SetTile(mousePos, tile[ActiveTile]);
        ReceiveData(ActiveTile, mousePos, default5);
    }

    [ClientRpc]
    void ReceiveData(int ActiveTile, Vector3Int mousePos, int default5)
    {
        if (default5 == 0)
        default1.SetTile(mousePos, tile[ActiveTile]);

        if (default5 == 1)
        default2.SetTile(mousePos, tile[ActiveTile]);
    }

    IEnumerator SpawnFire(Vector3Int mousePos)
    {
        yield return new WaitForSecondsRealtime(5);
        Scan(mousePos, 10, 6, true, true);
        GameObject Fire = Instantiate(Bombfire, mousePos + new Vector3(0.5f, 0.5f, 0), Quaternion.Euler(0, 0, 0));
        Fire.transform.localScale = new Vector3(5, 5, 5);
        NetworkServer.Spawn(Fire);
    }
}
