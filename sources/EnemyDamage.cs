using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    
    public float health;
    public float dazedTime;
    public float startDazedTime;

    private FMODUnity.StudioEventEmitter eventEmitter;

    
    [FMODUnity.EventRef]
    public string meleeHit = "event:/Other/Melee_Hit";



    public GameObject blood;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damage)
    {
        
        dazedTime = startDazedTime;
        Instantiate(blood, transform.position, Quaternion.identity);
        health -= damage;
        Debug.Log("damage Taken");
        Score.scoreValue += 5;
        CameraShake.shakeDuration = 0.1f;
        FMODUnity.RuntimeManager.PlayOneShot(meleeHit, transform.position);

    }
}
