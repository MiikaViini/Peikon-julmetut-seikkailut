using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour {

    [SerializeField]
    private float xMax;
    [SerializeField]
    private float yMax;
    [SerializeField]
    private float xMin;
    [SerializeField]
    private float yMin;

    public Transform target;

    public GameObject youDie;
    public GameObject backGroundPanel;
    public GameObject playerBody;
    public GameObject winPanel;

    public PlayerMovement player;

    public Vector3 playerSpawn;

    void Awake()
    {
        if (!GameObject.Find("Player"))
        {
            Instantiate(playerBody, playerSpawn, Quaternion.identity);
        } 
    }


    void Start()
    {
        PlayerMovement player = gameObject.GetComponent<PlayerMovement>() as PlayerMovement;
        target = GameObject.FindWithTag("Player").transform;
        
       
    }

     void LateUpdate()
    {
       
        if (target)
        transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin, xMax), Mathf.Clamp(target.position.y, yMin, yMax), transform.position.z);
        else 
        {
            youDie.SetActive(true);
            backGroundPanel.SetActive(true);
            transform.position = transform.position;
 
        }
    }
   
}
