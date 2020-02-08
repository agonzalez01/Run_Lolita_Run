using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class Controller : MonoBehaviour
{

    const float skinWidth = 0.015f;  //Giving space between raycast and character

    public LayerMask collisionMask;
    public LayerMask Floorfloor;
    public int horizontalRaycount = 4;
    public int verticalRaycount = 4;

    float horizontalRayspacing;
    float verticalRayspacing; 

    BoxCollider2D collider;
    RaycastOrigin Raycast;
    public CollisionInfo info;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        CalculateRayspacing();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(Vector3 velocity)
    {
        UpdateRaycast();
        info.Reset();

        //Checking for movement on x and y axis
        if (velocity.x != 0)
        HorizontalCollisions(ref velocity);
        if(velocity.y !=0)
        VerticalCollisions(ref velocity);


        transform.Translate(velocity);
    }

    //Checking collision on x axis
    void HorizontalCollisions(ref Vector3 velocity)
    {
        float directionX = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;

        //Casting rays, checking for hits
        for (int i = 0; i < horizontalRaycount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? Raycast.bottomLeft : Raycast.bottomRight;
            rayOrigin += Vector2.up * (horizontalRayspacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.right * directionX* rayLength, Color.red);

            if (hit)
            {
                velocity.x = (hit.distance - skinWidth) * directionX;
                rayLength = hit.distance;

                //Getting direction of collision
                info.left = directionX == -1; 
                info.right = directionX == 1;
            }
        }
    }

    //Checking collision on y axis
    public void VerticalCollisions(ref Vector3 velocity)
    {
        Floorfloor = LayerMask.GetMask("Floorfloor");

        float directionY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;


        //Casting rays, checking for hits

        for (int i = 0; i < verticalRaycount; i++)
        {
            Vector2 rayOrigin = Raycast.bottomLeft;
            rayOrigin += Vector2.right * (verticalRayspacing * i + velocity.x);
            RaycastHit2D hit2 = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, Floorfloor);

            if (hit2)
            {
                velocity.y = (hit2.distance - skinWidth) * directionY;
                rayLength = hit2.distance;

                //Getting collision direction
                info.below = directionY == -1;
                info.above = directionY == 1;

            }

            //Casting rays when jumping or falling down
            if (velocity.y <= 0)
            {
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

                Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

                if (hit)
                {
                    velocity.y = (hit.distance - skinWidth) * directionY;
                    rayLength = hit.distance;

                    info.below = directionY == -1;
                    info.above = directionY == 1;

                }
            }
        }
    }

    //Giving space between rays depending on raycount
    void CalculateRayspacing()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);

        horizontalRaycount = Mathf.Clamp(horizontalRaycount, 2, int.MaxValue);
        verticalRaycount = Mathf.Clamp(verticalRaycount, 2, int.MaxValue);

        horizontalRayspacing = bounds.size.y / (horizontalRaycount - 1);
        verticalRayspacing = bounds.size.x / (verticalRaycount - 1);
    }

    //Getting direction of collisions
    void UpdateRaycast()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);

        Raycast.bottomLeft = new Vector2 (bounds.min.x, bounds.min.y);
        Raycast.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        Raycast.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        Raycast.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    struct RaycastOrigin
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }

    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;


        public void Reset()
        {
            above = below = false;
            left = right = false;
        }
    }
}
