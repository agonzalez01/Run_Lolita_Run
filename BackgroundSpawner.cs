using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawner : MonoBehaviour {

    private GameObject[] backgrounds;
    private float lastX;

    // Use this for initialization
    void Start()
    {
        GetBackgroundsAndSetLastX();
    }


    void GetBackgroundsAndSetLastX()
    {
        backgrounds = GameObject.FindGameObjectsWithTag("Background");

        lastX = backgrounds[0].transform.position.x;

        for (int i = 1; i < backgrounds.Length; ++i)
        {
            if (lastX > backgrounds[i].transform.position.x)
            {
                lastX = backgrounds[i].transform.position.x;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Background"))
        {
            collision.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.CompareTag("Background"))
        {
            if (target.transform.position.y == lastX)
            {
                Vector3 temp = target.transform.position;
                float height = ((BoxCollider2D)target).size.x;

                for (int i = 0; i < backgrounds.Length; ++i)
                {
                    if (!backgrounds[i].activeInHierarchy)
                    {
                        temp.x -= height;
                        lastX = temp.x;
                        backgrounds[i].transform.position = temp;
                        backgrounds[i].SetActive(true);
                    }
                }

            }
        }
    }

}

