using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MusicControl : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string music = "event:/Music/Music";

    FMOD.Studio.EventInstance musicEv;
    // Start is called before the first frame update
    void Start()
    {
        musicEv = FMODUnity.RuntimeManager.CreateInstance(music);
        musicEv.start();
    }

    // Update is called once per frame
    void Update()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void PlayerDieMusic()
    {
        musicEv.setParameterByName("Death", 1f);
    }

    public void PlayerWinMusic()
    {
        musicEv.setParameterByName("Win", 1f);
    }

    public void StartMusic()
    {
        musicEv.start();
    }
    public void PlayerWinMusicReset()
    {
        musicEv.setParameterByName("Win", 0f);
    }
    public void PlayerDieMusicReset()
    {
        musicEv.setParameterByName("Death", 0f);
    }

    public void BossIsClose()
    {
        musicEv.setParameterByName("BossIsClose", 1f);
    }

    public void BossIsCloseReset()
    {
        musicEv.setParameterByName("BossIsClose", 0f);
    }
}
