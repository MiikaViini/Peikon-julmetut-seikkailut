using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    public GameObject player;

    private FMODUnity.StudioEventEmitter eventEmitter;

    //Player sounds
    [FMODUnity.EventRef]
    public string healCollectedSound = "event:/Collectibles/Heart_Collected";


    // Start is called before the first frame update
    void Awake()
    {
       
        
   
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        
        if (collision.gameObject.CompareTag("Player"))
        {
            FMODUnity.RuntimeManager.PlayOneShot(healCollectedSound, transform.position);
            if(player.GetComponent<PlayerMovement>().currentHealth >= 5)
            {
                Score.scoreValue += 10;
            }

            else
            {
                player.GetComponent<PlayerMovement>().currentHealth += 1;
            }
            Destroy(gameObject);
        }
    }
}
