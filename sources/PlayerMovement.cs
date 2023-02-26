using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D playerRB;

    public float moveSpeed;
    public float jumpForce;
    public float maxHealth = 5f;
    public int currentHealth = 5;

    public float knockbackLenght;
    public float knockbackCount;
    public float knockback;
    public float knockbackup;
    public bool knockFromRight;

    

    public float timer = 3f;

    bool facingRight = true;

    public bool grounded = false;
    public bool canMove;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    public float defaultGravity;

    public GameObject blood;
    public GameObject player;

    public Animator animator;

    public Transform spawnPoint;

    public Boss boss;
    public GameObject bossBody;
    public CameraScript cameraS;
    

    public bool dead;

    public GameObject canvas;
    public GameObject win;
    
    public float winTime = 5f;

    private FMODUnity.StudioEventEmitter eventEmitter;
    public MusicControl musicControl;
    public GameObject musicSystem;

    //Player sounds
    [FMODUnity.EventRef]
    public string playerHurtSound = "event:/Player/Taking_Damage";

    [FMODUnity.EventRef]
    public string jumpSound = "event:/Player/Jump";

    [FMODUnity.EventRef]
    public string walkSound = "event:/Player/Walk";

    [FMODUnity.EventRef]
    public string landingSound = "event:/Player/Landing";

    [FMODUnity.EventRef]
    public string playerDie = "event:/Other/Die";






    // Use this for initialization
    void Start()
    {
        canMove = true;
        dead = false;
        playerRB = GetComponent<Rigidbody2D>();
        canvas = GameObject.Find("Canvas");
        Score.scoreValue = 0;
        musicControl = GameObject.Find("MusicSystem").GetComponent<MusicControl>();
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround); //Onko pelaaja maassa

        float move = Input.GetAxisRaw("Horizontal");//Liikkuminen horisontaalisesti


        if (canMove == true && knockbackCount <= 0)
        {
            playerRB.velocity = new Vector2(move * moveSpeed, playerRB.velocity.y);
        }


        else
        {
            if (knockFromRight)
            {
                playerRB.velocity = new Vector2(-knockback, knockbackup);
            }

            if (!knockFromRight)
            {
                playerRB.velocity = new Vector2(knockback, knockbackup);
            }

            knockbackCount -= Time.deltaTime;
            /*playerRB.velocity = new Vector2(0, playerRB.velocity.x);*/
            /*playerRB.constraints = RigidbodyConstraints2D.FreezePositionY;*/
        }


        if (canMove == true && move > 0 && !facingRight)
            Flip();
        else if (canMove == true && move < 0 && facingRight)
            Flip();






        if (Input.GetAxisRaw("Horizontal") !=0)// Pelaajan liikkuminen, jos GetAxisRaw on jotain muuta kuin nolla niin, kävelyanimaatio päälle ja pelaaja liikkuu.
        {
            
            animator.SetBool("IsWalking", true);
            animator.SetBool("IsIdling", false);
            transform.Translate(new Vector3(moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0, 0));

        }
        if (Input.GetAxisRaw("Horizontal") != 0 && grounded)
        {
            FMODUnity.RuntimeManager.PlayOneShot(walkSound, transform.position);
        }


        if (Input.GetAxisRaw("Horizontal") >= 0.5)
        {
            
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            
            transform.localScale = new Vector3(-1f, 1f, 1f);

        }
        if(Input.GetAxisRaw("Horizontal") == 0)// jos GetAxisRaw on nolla, niin Idle animaatio käynnistuu ja kävelyanimaatio menee pois päältä.
        {

            animator.SetBool("IsWalking", false);
            animator.SetBool("IsIdling", true);
        }
    }


    void Update()

    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Level2" && GameObject.Find("Boss"))
        {
            bossBody = GameObject.Find("Boss");
            boss = bossBody.GetComponent<Boss>();

        }
        if (sceneName == "Level2" && !GameObject.Find("Boss"))
        {
            canMove = false;
            playerRB.constraints = RigidbodyConstraints2D.FreezeAll;


        }
        //win = GameObject.Find("WinGameOver");

        DontDestroyOnLoad(gameObject);
        
        if (grounded && Input.GetButtonDown("Jump"))//Pelaajan hyppy
        {
            FMODUnity.RuntimeManager.PlayOneShot(jumpSound, transform.position);
            playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            Debug.Log("Hypättiin");           
        }
        
        if (!grounded)
        {
            animator.SetBool("IsJumping", true);//Pelaajan hypyn animaatiot
            animator.SetBool("IsWalking", false);//Pelaajan hypyn animaatiot
            

        }
        else
        {
            animator.SetBool("IsJumping", false);//Pelaajan hypyn animaatiot
        }
     
        if (transform.position.y < -3 || currentHealth <= 0)//jos pelaaja tippuu rotkoon, niin Die metodi astuu voimaan.
        {
            Die();
        }

        if(boss.hitPlayer == true)
        {
            knockFromRight = true;
            KnockBack();
            Debug.Log("bossi löi pelaajaa");

        }
            

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
            Destroy(gameObject);
        }

    }


    void Flip()// kääntää pelaajan

    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && other.transform.position.x > transform.position.x )
        {
            KnockBack();
            currentHealth -= 1;
            knockFromRight = true;            
        }

        if (other.gameObject.CompareTag("Enemy") && other.transform.position.x < transform.position.x)
        {
            KnockBack();
            currentHealth -= 1;
            knockFromRight = false;
        }

    }

    




    void Die()

    {
        FMODUnity.RuntimeManager.PlayOneShot(playerDie, transform.position);
        Score.scoreValue = 0;
        Destroy(gameObject);
        currentHealth = 0;
        musicControl.PlayerDieMusic();
        

    }

        void KnockBack()// pelajaa lentää vihollisen osumasta taaksepäin.
        {
        FMODUnity.RuntimeManager.PlayOneShot(playerHurtSound, transform.position);
        knockbackCount = knockbackLenght;
            Instantiate(blood, transform.position, Quaternion.identity); 
            Debug.Log("Osuttiin viholliseen");   
            
        



    }
    }

    





