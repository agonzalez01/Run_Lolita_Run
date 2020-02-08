using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Controller))]
public class PlayerMovement : MonoBehaviour
{
    public GameObject DeathMenu;

    public Text distance;
    public Text bones;
    public int distanceMoved = 1;
    float acceleration = 0.1f;
    float maxSpeed = 20f;
    float jumpHeight = 1.1f;
    float timeToJumpApex = 0.6f;

    float jumpVelocity;
    float gravity;

    float Restart = 25;

    int bonesCollected = 0;
    int bonesForJump = 0;
    Vector3 velocity;

    public Button jumpBtn;
    public Button UpBtn;
    public Button DownBtn;
    public Button BigJump;

    bool iCanBigJump = true;
    
    HighScores scoreUpdate = new HighScores();
    



    Controller controller = new Controller();
    // Start is called before the first frame update
    void Start()
    {
        velocity.x = 1f;
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity * timeToJumpApex);
        controller = GetComponent<Controller>();

        jumpBtn.onClick.AddListener(() => justJump());
        UpBtn.onClick.AddListener(() => laneUp());
        DownBtn.onClick.AddListener(() => laneDown());
        BigJump.onClick.AddListener(() => bigJump());


        InvokeRepeating("Distance", 1, velocity.x);

        
    }

    // Update is called once per frame
    void Update()
    {

        //Updating jump force depending on movement
        if (this.transform.position.x > Restart)
        {
            Restart += 25;
            timeToJumpApex -= 0.02f;
            UpdateJump();
        }

        //Setting input for PC testing. Movement between lanes

        if(Input.GetKeyDown("s")  && transform.position.y > -3 && controller.info.below)
        {
            if(transform.position.y > 0)
                transform.position = new Vector2(transform.position.x, -1.615f);

            else if(transform.position.y > -2 && transform.position.y <-1)
                transform.position = new Vector2(transform.position.x, -4.165f);
        }



        if(Input.GetKeyDown("w") && transform.position.y < 0 && controller.info.below)
        {
            if (transform.position.y > -2 && transform.position.y < -1)
                transform.position = new Vector2(transform.position.x, 0.96f);
            else if(transform.position.y < -2)
            {
                transform.position = new Vector2(transform.position.x, -1.615f);
            }
        }

        //Checking for big jump availability to turn on big jump button in game
        if (iCanBigJump && transform.position.y > 0)
        {
            BigJump.gameObject.SetActive(true);
        }
        else if(transform.position.y < 0)
        {
            BigJump.gameObject.SetActive(false);
        }

        //Setting big jump depending on bones collected
        if (bonesCollected > 6)
        {
            iCanBigJump = true;
        }

        velocity.x += acceleration* Time.deltaTime;

        //Setting a limit for max speed
        if(velocity.x > maxSpeed)
        {
            velocity.x = maxSpeed;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

    public void UpdateJump()
    {
        //Updating jump force
        jumpVelocity = Mathf.Abs(gravity * timeToJumpApex);
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);

        if(timeToJumpApex < 0.25)
        {
            timeToJumpApex = 0.25f;
        }
    }

    public void justJump()
    {

        //Chaging y velocity to jump 
        if (controller.info.below)
        {
            velocity.y = jumpVelocity;
        }
        
        

    }

    public void bigJump()
    {
        if(controller.info.below && transform.position.y > 0)
        {
            if (timeToJumpApex > 0.5)
                velocity.y = 6.5f;
            else if (timeToJumpApex > 0.4)
                velocity.y = 8f;
            else if (timeToJumpApex > 0.3 )
                velocity.y = 10f;
            else if (timeToJumpApex > 0.25)
                velocity.y = 12f;
            else if (timeToJumpApex <= 0.25)
                velocity.y = 13f;

            BigJump.gameObject.SetActive(false);
            iCanBigJump = false;
        }

        if(bonesCollected > 7)
        {
            bonesCollected -= 7;
        }

        bones.text = "X " + bonesCollected.ToString();


    }

    public void laneUp()
    {
        if (controller.info.below)
        {
            if (transform.position.y > -2 && transform.position.y < -1)
                transform.position = new Vector2(transform.position.x, 0.96f);
            else if (transform.position.y < -2)
            {
                transform.position = new Vector2(transform.position.x, -1.615f);
            }
        }
    }

    public void laneDown()
    {
        if (controller.info.below)
        {
            if (transform.position.y > 0)
                transform.position = new Vector2(transform.position.x, -1.615f);

            else if (transform.position.y > -2 && transform.position.y < -1)
                transform.position = new Vector2(transform.position.x, -4.165f);
        }
    }

    void Distance()
    {
        
        distanceMoved += 1;
        distance.text = distanceMoved.ToString ();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            Time.timeScale = 0;
            DeathMenu.SetActive(true);
            UpBtn.gameObject.SetActive(false);
            DownBtn.gameObject.SetActive(false);
            BigJump.gameObject.SetActive(false);

            scoreUpdate.highScore(distanceMoved);
        }
        
        if(collision.gameObject.CompareTag("Bone"))
        {
            bonesCollected += 1;
            bonesForJump += 1;
            collision.gameObject.SetActive(false);

            bones.text = "X " + bonesCollected.ToString();
        }
    }

    IEnumerator ResetSpeed()
    {
        yield return new WaitForSecondsRealtime(0.7f);
        velocity.x -= 0.08f;
    }
}
