using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public int damage;

    public Animator animator;

    private FMODUnity.StudioEventEmitter eventEmitter;

    //Player sounds
    [FMODUnity.EventRef]
    public string playerMeleeSwingSound = "event:/Other/Melee_Swing";

    void Update()
    {
       if(timeBtwAttack <= 0)
        {
            
            if (Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.LeftControl))
            {
                FMODUnity.RuntimeManager.PlayOneShot(playerMeleeSwingSound, transform.position);
                animator.SetBool("IsAttacking", true);
                timeBtwAttack = startTimeBtwAttack;
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<EnemyDamage>().TakeDamage(damage);
                    

                }
            }
            else
            {
                animator.SetBool("IsAttacking", false);
            }
         }

        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
      }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
