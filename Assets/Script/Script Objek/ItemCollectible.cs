using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectible : MonoBehaviourPun
{
    public GameObject Interact;

    void Start()
    {
        Interact.SetActive(false);
    }

    void OnTriggerStay(Collider other)
    {

        if (photonView.IsMine)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Interact.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Interact.SetActive(false);
                    PhotonNetwork.Destroy(gameObject);
                    Debug.Log("p");
                }

            }
        }
    }

    void OnTriggerExit(Collider other)
    {

        if (photonView.IsMine)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Interact.SetActive(false);
            }
        }

    }
}
