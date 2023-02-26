using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Sprite[] heartSprites;
    public Image HeartUI;
    public PlayerMovement player;



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        HeartUI.sprite = heartSprites[player.currentHealth];
    }
}
