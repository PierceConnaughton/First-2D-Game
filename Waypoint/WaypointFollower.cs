using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    [SerializeField] private float speed = 2f;

    // Update is called once per frame
    private void Update()
    {
        //distance between the waypoint and the platform
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < 0.1f)
        {
            currentWaypointIndex++;

            //if we are at the last waypoint go back to the original
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }

        //have the current platform move towards the next waypoint
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);


    }
}
