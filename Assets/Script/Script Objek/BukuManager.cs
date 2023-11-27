using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BukuManager : MonoBehaviourPun
{
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

            for(int i = 0; i < Kertas.Length; i++)
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
        }
    }

    void BukuUpdate()
    {

    }
}
