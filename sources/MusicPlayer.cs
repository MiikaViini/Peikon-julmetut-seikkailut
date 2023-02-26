using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip[] audioClip;
    // Start is called before the first frame update

    private void Awake()
    {
        
    }
    private void Start()
    {
        PlaySound(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void PlaySound(int clip)
    {
        GetComponent<AudioSource>().clip = audioClip[clip];
        GetComponent<AudioSource>().Play();
    }
}
