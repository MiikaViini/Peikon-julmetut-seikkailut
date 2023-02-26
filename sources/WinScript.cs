using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject boss;
    public GameObject player;

    public MusicControl musicControl;


    public float winTime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        musicControl = GameObject.Find("MusicSystem").GetComponent<MusicControl>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.Find("Boss"))
        {
            musicControl.PlayerWinMusic();
            winTime -= Time.deltaTime;
        }
        
        else
        {
            winPanel.SetActive(false);
        }

        
    }

    void LateUpdate()
    {
        if (!GameObject.Find("Boss") && winTime <= 0)
        {
            
            winPanel.SetActive(true);
            losePanel.SetActive(false);
            Destroy(player);
            winTime = 0;
            
        }
        
    }
}
