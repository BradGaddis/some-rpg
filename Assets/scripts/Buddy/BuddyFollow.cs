using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuddyFollow : MonoBehaviour
{
    [SerializeField]
    GameObject wayPoint;
    Vector3 playerPos;
    [SerializeField]
    float innerRadius;
    [SerializeField]
    float followRadius;
    
    [SerializeField]
    bool followPlayer;

    [SerializeField]
    float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        if (wayPoint == null)
        {
            wayPoint = GameObject.Find("wayPoint"); // This can be used later for path finding
        } else if (wayPoint.tag == "Player"){
            followPlayer = true;
        }
    }

void  OnDrawGizmos()
{
    Gizmos.color = Color.green;
    Gizmos.DrawWireSphere(transform.position, innerRadius);
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, followRadius);
}


    // Update is called once per frame
    void Update()
    {
        playerPos = wayPoint.transform.position;
        if (followPlayer) {
            FollowPlayer();
        } 
        else {
            // Follow waypoint
            if (Vector3.Distance(transform.position, playerPos) > innerRadius)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
            }
        }
    }

    private void FollowPlayer()
    {
        // Check if Buddy is outside of follow radius
        if (Vector2.Distance(transform.position, wayPoint.transform.position) > innerRadius)
        {
            Vector2 wayPointVel = wayPoint.GetComponent<Rigidbody2D>().velocity;
            if (wayPointVel != Vector2.zero)
            {
                float xDir = Mathf.Sign(wayPointVel.x) * -1f;
                float yDir = Mathf.Sign(wayPointVel.y) * -1f;
                
                // check if position is within idle radius
                if (Vector2.Distance(transform.position, wayPoint.transform.position) <= innerRadius){
                    ProcessIdle(xDir, yDir);
                    return; 
                }
                
                // Budddy is inside follow radius, should not be moving, but panicking
                if (Vector2.Distance(transform.position, wayPoint.transform.position) < followRadius)
                {
                    // transform.position = Vector2.MoveTowards(transform.position, wayPoint.transform.position + new Vector3(xDir * followRadius, yDir * followRadius), speed * Time.deltaTime);
                    ProcessPlayerLeaving(xDir, yDir);
                    return;
                }
                else
                // Move towards the outer bouns of the follow raidus of player
                {
                    Vector2 direction = transform.position - playerPos;
                    float distance = Vector2.Distance(transform.position, playerPos);
                    direction = direction.normalized;
                    transform.position = new Vector3(playerPos.x + direction.x * followRadius, playerPos.y + direction.y * followRadius);

                }
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
            }
        }
    }

    private void ProcessPlayerLeaving(float xDir, float yDir)
    {
        Debug.Log("HELP, HE'S LEAVING ME!!");
        return; 
    }

    private void ProcessIdle(float xDir, float yDir)
    {
        Debug.Log("I'm idle");
        return;
    }
}
