using UnityEngine;
using System.Collections;

public class BossScript : MonoBehaviour
{
    public float moveSpeed;
    public float health;
    private float dazedTime;
    public float startDazedTime;

    public GameObject gameOver;



    Vector3 localScale;
    public bool movingRight = true;
    Rigidbody2D rb;

    public GameObject blood;
    public Transform leftWayPoint, rightWayPoint;


    void Start()
    {
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();

        //leftWayPoint = GameObject.Find("LeftWayPoint").GetComponent<Transform>();
        //rightWayPoint = GameObject.Find("RightWayPoint").GetComponent<Transform>();

    }

    void Update()
    {

        if (transform.position.x > rightWayPoint.position.x)
            movingRight = false;

        if (transform.position.x < leftWayPoint.position.x)
            movingRight = true;

        if (movingRight)
            moveRight();

        else
            moveLeft();

        if (dazedTime <= 0)
        {
            moveSpeed = moveSpeed;
        }
        else
        {
            moveSpeed = 0;
            dazedTime -= Time.deltaTime;
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }

        if (gameOver.gameObject.activeSelf == true)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }


    }

    void moveRight()
    {
        movingRight = true;
        localScale.x = 1f;
        transform.localScale = localScale;
        rb.velocity = new Vector2(localScale.x * moveSpeed, rb.velocity.y);
    }
    void moveLeft()
    {
        movingRight = false;
        localScale.x = -1f;
        transform.localScale = localScale;
        rb.velocity = new Vector2(localScale.x * moveSpeed, rb.velocity.y);

    }

    public void TakeDamage(int damage)
    {
        dazedTime = startDazedTime;
        Instantiate(blood, transform.position, Quaternion.identity);
        health -= damage;
        Debug.Log("damage Taken");
        Score.scoreValue += 5;
        CameraShake.shakeDuration = 0.1f;
    }


}


