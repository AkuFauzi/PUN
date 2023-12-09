using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviourPun
{
    public BukuManager BukuManager;

    bool IsNext;
    // Start is called before the first frame update
    void Start()
    {
        BukuManager = GameObject.FindObjectOfType<BukuManager>().GetComponent<BukuManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickNext()
    {
        BukuManager.Lembar[1].SetActive(false);
        BukuManager.Lembar[2].SetActive(false);
        BukuManager.Lembar[0].SetActive(true);
    }

    public void OnClickPrev()
    {
        BukuManager.Lembar[1].SetActive(true);
        BukuManager.Lembar[2].SetActive(true);
        BukuManager.Lembar[0].SetActive(false);
    }

    public void OnClickExitBook()
    {
        BukuManager.buku.SetActive(false);
        BukuManager.IsOpen = false;
    }

    //public void OnClickWInUI()
    //{
    //    BukuManager.winUI.SetActive(false);
    //}
}
