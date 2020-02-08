using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    private float speed = 1f;
    private float acceleration = 0.1f;
    private float maxSpeed = 20f;

    [HideInInspector]
    private bool moveCamera;

	// Use this for initialization
	void Start () {
        moveCamera = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (moveCamera)
            MoveCamera();
	}

    void MoveCamera()
    {
        Vector3 temp = transform.position;

        float oldX = temp.x;
        float newX = temp.x + (speed * Time.deltaTime); //Setting new position for the camera

        temp.x = Mathf.Clamp(oldX, newX, temp.x);

        transform.position = temp;

        speed += acceleration * Time.deltaTime; //Adding speed over time

        //Setting a limit for the speed of the camera
        if (speed > maxSpeed)
            speed = maxSpeed;
    }

    public void Timer()
    {
        int i = 1;
        Debug.Log(i);
        i += 1;
    }
}
