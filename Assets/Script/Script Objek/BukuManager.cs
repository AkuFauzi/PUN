using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BukuManager : MonoBehaviourPun
{
    public static BukuManager Instance;

    public GameObject buku;
    public GameObject[] Item;
    public GameObject[] Kertas;
    public GameObject[] Lembar;


    bool IsOpen;

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            buku = GameObject.FindGameObjectWithTag("Buku");
            Item = GameObject.FindGameObjectsWithTag("Item");
            Kertas = GameObject.FindGameObjectsWithTag("Kertas");
            Lembar = GameObject.FindGameObjectsWithTag("Lembaran");

            buku.SetActive(false);

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
}
