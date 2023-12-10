using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip BGM;
    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.loop = true;
        audio.PlayOneShot(BGM);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
