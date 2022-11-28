using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Present : MonoBehaviour
{

    public GameObject presentCollected;
    public Animator animator;

    private FMODUnity.StudioEventEmitter eventEmitter;

    
    [FMODUnity.EventRef]
    public string presentCollectedSound = "event:/Collectibles/Present_Collected";



    private void Start()
    {
        animator.SetBool("PresentIdle", true);
        animator.SetBool("PresentCollection", false);
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FMODUnity.RuntimeManager.PlayOneShot(presentCollectedSound, transform.position);
            Debug.Log("Lahja kerätty");
            Score.scoreValue += 10;
            animator.SetBool("PresentCollection", true);
            InvokeRepeating("Spawn", 0.4f, 0);
            PresentCollected();
            
        }

    }

    void PresentCollected()
    {
        if (gameObject.CompareTag("Present"))
        {
            
            Destroy(gameObject, 0.5f);
            
        }
    }

    void Spawn()
    {
        Instantiate(presentCollected, transform.position, Quaternion.identity);
    }
}
