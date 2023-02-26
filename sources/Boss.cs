using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{   //Moving

    public float attackTime;
    
    public GameObject gameOver;

    Vector3 localScale;
    Rigidbody2D rb;

    public GameObject blood;
    public GameObject dieBlood;

    public Animator animator;

    

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public int damage;

    public bool isAttacking;
    
    public bool facingRight = true;

    public EnemyDamage enemyDamage;
    public GameObject player;

    public bool playerIsClose;
    public float chargeTime;
    public float startChargeTime;

    public bool hitPlayer;

    private FMODUnity.StudioEventEmitter eventEmitter;

    public MusicControl musicControl;

    


    [FMODUnity.EventRef]
    public string winSound = "event:/Other/WinSound";




    void Start()
    {
        startChargeTime = 1.35f;
        localScale = transform.localScale;
        localScale.x = -1.0f;
        transform.localScale = localScale;
        rb = GetComponent<Rigidbody2D>();
        gameObject.GetComponent<EnemyDamage>();
        musicControl = GameObject.Find("MusicSystem").GetComponent<MusicControl>();
        player = GameObject.FindWithTag("Player");
        

    }

     void Update()
    {
        
        if (enemyDamage.health <= 0)
        {
            Die();
        }

        if (gameOver.gameObject.activeSelf == true)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }

        if (chargeTime <= 0 && isAttacking)
        {
            Attack();
            chargeTime = startChargeTime;
            
        }

        if (GameObject.FindWithTag("Player"))
        {
            if (gameObject.transform.position.x - player.transform.position.x <= 1.5)
            {
                isAttacking = true;
                playerIsClose = true;
                chargeTime -= Time.deltaTime;
                animator.SetBool("IsHitting", true);
                animator.SetBool("IsMoving", false);
        
            }

            

            if (gameObject.transform.position.x - player.transform.position.x >= 1.5)
            {
                isAttacking = false;
                playerIsClose = false;
                chargeTime = startChargeTime;
                animator.SetBool("IsHitting", false);
                animator.SetBool("IsMoving", true);
                chargeTime = startChargeTime;
                hitPlayer = false;
            }

            if (gameObject.transform.position.x - player.transform.position.x <= 9.5)
            {
                musicControl.BossIsClose();
                //musicControl.BossIsCloseReset();
            }

            if (gameObject.transform.position.x - player.transform.position.x <= 9.4)
            {               
                musicControl.BossIsCloseReset();
            }
        }
        else
        {
            animator.SetBool("IsHitting", false);
            animator.SetBool("IsMoving", true);
        }
    }



    public void Die()
    {
        Instantiate(dieBlood, transform.position, Quaternion.identity);
        Destroy(gameObject);
        FMODUnity.RuntimeManager.PlayOneShot(winSound, transform.position);
        

    }


    void Attack()
    {
        
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<PlayerMovement>().currentHealth -= 1;
                Debug.Log("Damage tehtiin");
                hitPlayer = true;

            }
        

    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }


}




