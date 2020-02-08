using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollector : MonoBehaviour
{
   /* public GameObject obstacle1;
    public GameObject obstacle2;
    public GameObject obstacle3; */

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Bone"))
        {
            collision.gameObject.SetActive(false);
        }
    }
}
