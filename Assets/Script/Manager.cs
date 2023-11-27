using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using TMPro;

public class Manager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI statusKoneksi;

    public TMP_InputField namaPlayer;
    public GameObject panelLogin;

    private void Update()
    {
        statusKoneksi.text = "Status Koneksi : " + PhotonNetwork.NetworkClientState;
    }

    public void OnLoginButtonCliked()
    {
        string playerName = namaPlayer.text;
        if (!string.IsNullOrEmpty(playerName))
        {
            PhotonNetwork.LocalPlayer.NickName = playerName;
            PhotonNetwork.ConnectUsingSettings();

        }
        else
        {
            Debug.Log("Nama Player Tidak Valid!");
        }
    }

    public override void OnConnected()
    {
        Debug.Log("Terhubung ke Internet");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " Terhubung ke Photon");
    }
}