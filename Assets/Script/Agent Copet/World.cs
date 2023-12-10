using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    private static readonly World instance = new World();
    private static GameObject[] hidingpos;

    static World()
    {
        hidingpos = GameObject.FindGameObjectsWithTag("tempatsembunyi");
    }

    public static World Instance
    {
        get { return instance; }
    }

    public GameObject[] GetHidingPos()
    {
        return hidingpos;
    }
}
