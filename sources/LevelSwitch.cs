using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitch : MonoBehaviour {

    public string levelToLoad;
    
    public GameObject player;

    public PlayerMovement pMovement;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SceneManager.LoadScene(levelToLoad);
            player.transform.position = pMovement.spawnPoint.position;
            
        }
    }
}