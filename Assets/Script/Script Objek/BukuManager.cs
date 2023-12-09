using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BukuManager : MonoBehaviourPun
{
    public static BukuManager Instance;

    public GameObject buku;
    public GameObject[] Item;
    public GameObject[] Kertas;
    public GameObject[] Lembar;
    public GameObject[] colliderQuest;
    //public GameObject winUI;
    public TextMeshProUGUI textMeshProUGUI;

    public bool IsOpen;

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            buku = GameObject.FindGameObjectWithTag("Buku");
            Item = GameObject.FindGameObjectsWithTag("Item");
            Kertas = GameObject.FindGameObjectsWithTag("Kertas");
            Lembar = GameObject.FindGameObjectsWithTag("Lembaran");
            colliderQuest = GameObject.FindGameObjectsWithTag("ColliderQuest");
            //winUI = GameObject.FindGameObjectWithTag("WinUI");
            textMeshProUGUI = GameObject.FindGameObjectWithTag("TextMisi").GetComponent<TextMeshProUGUI>();

            //winUI.SetActive(false);
            buku.SetActive(false);
            Kertas[0].SetActive(false);
            IsOpen = false;

            for(int i = 0; i < Item.Length; i++)
            {
                Kertas[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(photonView.IsMine)
        {
            if(Input.GetKeyDown(KeyCode.E) && !IsOpen) 
            {
                buku.SetActive(true);
                IsOpen = true;
            }
            else if (Input.GetKeyDown(KeyCode.E) && IsOpen)
            {
                buku.SetActive(false);
                IsOpen= false;
            }

            BukuUpdate();
            MisiUpdate();
        }
    }

    void BukuUpdate()
    {
        if (Item[0]==null)
        {
            Kertas[0].SetActive(true);
        }
        if (Item[1] == null)
        {
            Kertas[1].SetActive(true);
        }
        if (Item[2] == null)
        {
            Kertas[2].SetActive(true);
        }
        if (Item[3] == null)
        {
            Kertas[3].SetActive(true);
        }
        if (Item[4] == null)
        {
            Kertas[4].SetActive(true);
        }
        if (Item[5] == null)
        {
            Kertas[5].SetActive(true);
        }
    }

    void MisiUpdate()
    {
        if (Item[3] != null)
        {
            textMeshProUGUI.text = "Temukan satu lembar sejarah dan naik ke tingkat selanjutnya";
            
        }
        else
        {
            textMeshProUGUI.text = "Lembar sejarah telah diambil Tekan E untuk membuka buku";
        }

        if (Item[2] || Item[4] || Item[5]!=null)
        {
            textMeshProUGUI.text = "Temukan tiga lembar sejarah dan naik ke tingkat selanjutnya";
        }
        else
        {
            textMeshProUGUI.text = "Lembar sejarah telah diambil Tekan E untuk membuka buku";
        }

        //if (Item[0] || Item[1] || Item[2] || Item[3] || Item[4] || Item[5] == null)
        //{
        //    winUI.SetActive(true);
        //}

    }
}
