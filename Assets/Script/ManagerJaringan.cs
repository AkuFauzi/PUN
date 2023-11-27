using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;

public class ManagerJaringan : MonoBehaviourPunCallbacks
{
    //Variabel koneksi
    [Header("connectio status")]
    public TextMeshProUGUI statusKoneksi;


    //Variabel login
    [Header("login UI Panel")]
    public TMP_InputField NamaPlayer;
    public GameObject panelLogin;

    [Header("Room Panel")]
    public GameObject RoomPanel;

    [Header("Membuat Room Panel")]
    public GameObject BuatRoomPanel;
    public TMP_InputField NamaRoomInputField;
    public TMP_InputField maxPlayerInputField;

    [Header("Game Panel")]
    public GameObject GamePanel;
    public TextMeshProUGUI roomInfoText;
    public GameObject TombolMulaiGame;

    [Header("Daftar Room Panel")]
    public GameObject DaftarRoomPanel;
    public GameObject daftarEntriRoomPrefab;
    public GameObject daftarRoomUtamaGameobject;

    [Header("Gabung Room Acak")]
    public GameObject RoomAcakPanel;

    private Dictionary<string, RoomInfo> cacheDaftarRoom;
    private Dictionary<string, GameObject> daftarRoomGameObject;


    // Start is called before the first frame update
    void Start()
    {
        ActivatePanel(panelLogin.name);
        cacheDaftarRoom = new Dictionary<string, RoomInfo>();
        daftarRoomGameObject = new Dictionary<string, GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        statusKoneksi.text = " Status Koneksi :" + PhotonNetwork.NetworkClientState;
    }

    public void OnloginButtonClicked()
    {
        string playerName = NamaPlayer.text;
        if (!string.IsNullOrEmpty(playerName))
        {
            PhotonNetwork.LocalPlayer.NickName = playerName;
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            print("Nama Player Tidak Valid");
        }
    }

    public void OnRoomCreateButtonClicked()
    {
        string NamaRoom = NamaRoomInputField.text;
        if (string.IsNullOrEmpty(NamaRoom))
        {
            NamaRoom = "Room " + Random.Range(1000, 10000);
        }
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)int.Parse(maxPlayerInputField.text);
        PhotonNetwork.CreateRoom(NamaRoom, roomOptions);
    }
    public void OnCancleButtonClicked()
    {
        ActivatePanel(RoomPanel.name);
    }

    public override void OnConnected()
    {
        print("Terhubung ke Interet");
    }

    public override void OnConnectedToMaster()
    {
        print(PhotonNetwork.LocalPlayer.NickName + " Terhubung Ke photon");
        ActivatePanel(RoomPanel.name);
    }

    public override void OnCreatedRoom()
    {
        print(PhotonNetwork.CurrentRoom.Name + "Berhasil Membuat Room");
    }
    public override void OnJoinedRoom()
    {
        print(PhotonNetwork.LocalPlayer.NickName + "bergabung Dengan" + PhotonNetwork.CurrentRoom.Name);
        ActivatePanel(GamePanel.name);

        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            TombolMulaiGame.SetActive(true);
        }
        else
        {
            TombolMulaiGame.SetActive(false);
        }

        roomInfoText.text = "Nama Room: " + PhotonNetwork.CurrentRoom.Name + " " + "Maks.Players :" + PhotonNetwork.CurrentRoom.PlayerCount + "/" + PhotonNetwork.CurrentRoom.MaxPlayers;

    }

    public override void OnRoomListUpdate(List<RoomInfo> daftarRoom)
    {
        ClearRoomListView();

        foreach (RoomInfo room in daftarRoom)
        {
            print(room.Name);
            if(!room.IsOpen||!room.IsVisible||room.RemovedFromList)
            {
                if(cacheDaftarRoom.ContainsKey(room.Name))
                {
                    cacheDaftarRoom.Remove(room.Name); 
                }
            }
            else
            {
                if (cacheDaftarRoom.ContainsKey(room.Name))
                {
                    cacheDaftarRoom[room.Name] = room;
                }
                else
                {
                    cacheDaftarRoom.Add(room.Name, room);
                }
            }
        }

        foreach(RoomInfo room in cacheDaftarRoom.Values)
        {

            GameObject daftarEntriRoomGameObject = Instantiate(daftarEntriRoomPrefab);
            daftarEntriRoomGameObject.transform.SetParent(daftarRoomUtamaGameobject.transform);
            daftarEntriRoomGameObject.transform.localScale = Vector3.one;

            daftarEntriRoomGameObject.transform.Find("NamaRoomText").GetComponent<Text>().text = room.Name;
            daftarEntriRoomGameObject.transform.Find("MaksPlayerText").GetComponent<Text>().text = room.PlayerCount + " / " + room.MaxPlayers;
            daftarEntriRoomGameObject.transform.Find("BuittongabungRoom").GetComponent<Button>().onClick.AddListener(()=>OnJoinRoomButtonClicked(room.Name));

            daftarRoomGameObject.Add(room.Name, daftarEntriRoomGameObject);
        }
    }

    public void OnShowRoomListButtonClicked()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    void OnJoinRoomButtonClicked(string _namaroom)
    {
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
        }

        PhotonNetwork.JoinRoom(_namaroom);
    }

    void ClearRoomListView()
    {
        foreach(var daftarRoomGameObject in daftarRoomGameObject.Values)
        {
            Destroy(daftarRoomGameObject);  
        }

        daftarRoomGameObject.Clear();
    }

    public void ActivatePanel(string panelToBeActivated)
    {
        panelLogin.SetActive(panelToBeActivated.Equals(panelLogin.name));
        RoomPanel.SetActive(panelToBeActivated.Equals(RoomPanel.name));
        BuatRoomPanel.SetActive(panelToBeActivated.Equals(BuatRoomPanel.name));
        GamePanel.SetActive(panelToBeActivated.Equals(GamePanel.name));
        DaftarRoomPanel.SetActive(panelToBeActivated.Equals(DaftarRoomPanel.name));
        RoomAcakPanel.SetActive(panelToBeActivated.Equals(RoomAcakPanel.name));
    }
}
