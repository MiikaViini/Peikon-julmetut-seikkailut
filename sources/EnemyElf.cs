using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyElf : MonoBehaviour
{
    public float moveSpeed;
    public float startSpeed;
    public float health;
    
    

    public GameObject gameOver;



    Vector3 localScale;
    public bool movingRight = true;
    Rigidbody2D rb;

    public GameObject blood;
    public GameObject dieBlood;
    public Transform leftWayPoint, rightWayPoint;

    public EnemyDamage enemyDamage;

    public Animator animator;
    public float animationTimer;
    public float animationTimer2;

    private FMODUnity.StudioEventEmitter eventEmitter;


    [FMODUnity.EventRef]
    public string enemyDeathSound = "event:/Enemy/sfx_exp_odd7";

   

    


    void Start()
    {
        animationTimer = 1.5f;
        animationTimer2 = 1.5f;
        startSpeed = moveSpeed;
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        
        //leftWayPoint = GameObject.Find("LeftWayPoint").GetComponent<Transform>();
        //rightWayPoint = GameObject.Find("RightWayPoint").GetComponent<Transform>();

    }

    void Update()
    {
        animationTimer -= Time.deltaTime;
        


        if (transform.position.x > rightWayPoint.position.x)
            movingRight = false;

        if (transform.position.x < leftWayPoint.position.x)
            movingRight = true;

        if (movingRight)
            moveRight();

        else
            moveLeft();

        if (enemyDamage.dazedTime <= 0)
        {
            moveSpeed = startSpeed;
            animator.SetBool("TakingDamage", false);
        }
        else
        {
            moveSpeed = 0;
            enemyDamage.dazedTime -= Time.deltaTime;
        }

        if (enemyDamage.health <= 0)
        {
            DestroyEnemy();
        }

        if (gameOver.gameObject.activeSelf == true)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }

        if (animationTimer <= 0)
        {
            
            animator.SetBool("IsSkiing", true);
            animator.SetBool("IsSkiing2", false);
            animationTimer2 -= Time.deltaTime;
            

        }
        if(animationTimer2 <= 0)
        {
           
            animator.SetBool("IsSkiing", false);
            animator.SetBool("IsSkiing2", true);
            
        }

        

        if(animationTimer <= 0  && animationTimer2 <= 0)
        {
           
            animationTimer = 1.5f;
            animationTimer2 = 1.5f;
        }

        if(enemyDamage.dazedTime > 0 )
        {
            animator.SetBool("TakingDamage", true);
        }
        


    }

    void moveRight()
    {
        movingRight = true;
        localScale.x = 1.2f;
        transform.localScale = localScale;
        rb.velocity = new Vector2(localScale.x * moveSpeed, rb.velocity.y);
    }
    void moveLeft()
    {
        movingRight = false;
        localScale.x = -1.2f;
        transform.localScale = localScale;
        rb.velocity = new Vector2(localScale.x * moveSpeed, rb.velocity.y);
        
    }

    /*public void TakeDamage(int damage)
    {
        dazedTime = startDazedTime;
        Instantiate(blood, transform.position, Quaternion.identity);
        health -= damage;
        Debug.Log("damage Taken");
        Score.scoreValue += 5;
        CameraShake.shakeDuration = 0.1f;
    }*/

    public void DestroyEnemy()
    {
        localScale.y -= 15f * Time.deltaTime;
        if(localScale.y <= 0)
        {
            FMODUnity.RuntimeManager.PlayOneShot(enemyDeathSound, transform.position);
            Instantiate(dieBlood, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        
    }

    
    }




