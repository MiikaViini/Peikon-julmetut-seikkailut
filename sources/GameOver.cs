using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{

    public GameObject gameOver;
    public GameObject backGroundPanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            Debug.Log("Osuttiin maaliin");
            gameOver.SetActive(true);
            backGroundPanel.SetActive(true);
            PlayerMovement moveScript = collision.GetComponent<PlayerMovement>();
            moveScript.canMove = false;


        }

    }
}
