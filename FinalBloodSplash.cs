using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBloodSplash : MonoBehaviour
{
    public GameObject finalBlood;
    public int bloodCount;
    // Start is called before the first frame update
    void Start()
    {
        bloodCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
       if (!GameObject.Find("Boss") && bloodCount <= 0)
        {
            bloodCount = 1;
            Instantiate(finalBlood, transform.position, Quaternion.identity);
        }
    }
}
