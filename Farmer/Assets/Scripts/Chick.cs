using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chick : Character
{
    public List<Transform> waypoints;

    private List<int> facingRight = new List<int>() {
        1, 2, 3, 4, 7
    };
    private int waypointIndex;
    public float moveSpeed = 0.1f;
    private bool moving = true;

    private void Update()
    {
        if (moving)
        {
            Move();
        }

    }

    private IEnumerator ReachedWaypoint()
    {
        yield return new WaitForSeconds(2);

        // Increment waypoint index
        waypointIndex++;
        if (waypointIndex >= waypoints.Count)
        {
            waypointIndex = 0;
        }

        // Update sprite direction
        if (facingRight.Contains(waypointIndex))
        {
            transform.localScale = Vector2.one;
        }
        else
        {
            transform.localScale = new Vector2(-1, 1);
        }

        moving = true;
    }

    private void Move()
    {
        if (waypointIndex < waypoints.Count)
        {
            Vector2 movingg = Vector2.MoveTowards(transform.position,
               waypoints[waypointIndex].transform.position,
               moveSpeed * Time.deltaTime);

            transform.position = movingg;

            animator.SetFloat("Speed", moveSpeed);

            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                moving = false;
                animator.SetFloat("Speed", 0);
                StartCoroutine(ReachedWaypoint());
            }
        }
    }
    protected override void Start()
    {
        base.Start();
        transform.position = waypoints[waypointIndex].transform.position;
    }
}
