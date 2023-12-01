using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public GameObject fishprefab;
    public int numfish = 20;
    public GameObject[] allfish;
    public Vector3 swimlimits = new Vector3(5, 5, 5);
    public Vector3 goalpos;

    [Header("setting ikan")]
    [Range(0.0f, 5.0f)]
    public float minSpeed;
    [Range(0.0f, 5.0f)]
    public float maxSpeed;
    [Range(0.0f, 10.0f)]
    public float neighbourDistance;
    [Range(0.0f, 5.0f)]
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        allfish = new GameObject[numfish];

        for (int i=0; i< numfish; i++) 
        {
        Vector3 pos = this.transform.position + new Vector3(Random.Range(-swimlimits.x,swimlimits.x),
                                                            Random.Range(-swimlimits.y, swimlimits.y),
                                                            Random.Range(-swimlimits.z, swimlimits.z));
            allfish[i] = (GameObject)Instantiate(fishprefab, pos, Quaternion.identity);
            allfish[i].GetComponent<Flock>().myManager = this;
        }
        goalpos = this.transform.position;

    }

    // Update is called once per frame
    void Update()
    { if (Random.Range(0,100)<10)
        goalpos = this.transform.position + new Vector3(Random.Range(-swimlimits.x, swimlimits.x),
                                                            Random.Range(-swimlimits.y, swimlimits.y),
                                                            Random.Range(-swimlimits.z, swimlimits.z));
    }
}
