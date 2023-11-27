using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectible : MonoBehaviourPun
{
    public GameObject Interact;

    private void Start()
    {
        if (photonView.IsMine)
        {
            Interact = GameObject.FindGameObjectWithTag("Interact");
            Interact.SetActive(false);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (photonView.IsMine)
        {
            if (other.gameObject.tag == "Player")
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

    private void OnTriggerExit(Collider other)
    {
        if (photonView.IsMine)
        {
            Interact.SetActive(false);
        }
    }
}
