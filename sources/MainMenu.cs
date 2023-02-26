using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    private FMODUnity.StudioEventEmitter eventEmitter;
    public MusicControl musicControl;
    //Player sounds
    [FMODUnity.EventRef]
    public string menuSelect = "event:/Other/Menu_Select";


    void Start()
    {
        musicControl = GameObject.Find("MusicSystem").GetComponent<MusicControl>();
    }
    


    public void LevelToLoad(string levelToLoad)
    {
        FMODUnity.RuntimeManager.PlayOneShot(menuSelect, transform.position);
        SceneManager.LoadScene(levelToLoad);
        musicControl.PlayerDieMusicReset();
        musicControl.PlayerWinMusicReset();
        musicControl.BossIsCloseReset();
        musicControl.StartMusic();

    }

    public void Quit()
    {
        Application.Quit();
    }

   

}



