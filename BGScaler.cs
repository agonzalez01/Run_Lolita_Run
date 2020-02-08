using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScaler : MonoBehaviour {


        // Use this for initialization
        void Start()
        {
            var height = Camera.main.orthographicSize * 2f;
            var width = height * Screen.width / Screen.height;

            if (gameObject.name == "Background")
            {
                transform.localScale = new Vector3(width, height, -2);
            }
            else
                transform.localScale = new Vector3(width + 3f, 5, -2);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

