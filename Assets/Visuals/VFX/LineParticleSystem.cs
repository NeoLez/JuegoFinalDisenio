using System.Collections.Generic;
using UnityEngine;

public class LineParticleSystem : MonoBehaviour
{
    [Header("Movement")]
    public List<Transform> waypoints;
    public float speed = 2f;
    public bool loop = true;

    [Header("Set Axis")]
    public bool moveOnX = true;
    public bool moveOnY = false;
    public bool moveOnZ = true;

    private int currentWaypointIndex = 0;

    void Update()
    {
        if (waypoints == null || waypoints.Count == 0) return;

        Transform target = waypoints[currentWaypointIndex];
        Vector3 targetPos = FilterAxes(target.position);
        Vector3 currentPos = FilterAxes(transform.position);
        Vector3 direction = targetPos - currentPos;

        float step = speed * Time.deltaTime;

        if (direction.magnitude <= step)
        {
            transform.position = ApplyAxes(target.position);
            currentWaypointIndex++;

            if (currentWaypointIndex >= waypoints.Count)
            {
                currentWaypointIndex = 0;
                if (!loop)
                    enabled = false;
            }
        }
        else
        {
            transform.position = ApplyAxes(currentPos + direction.normalized * step);
        }
    }

    
    Vector3 ApplyAxes(Vector3 newPosition)
    {
        Vector3 pos = transform.position;
        if (moveOnX) pos.x = newPosition.x;
        if (moveOnY) pos.y = newPosition.y;
        if (moveOnZ) pos.z = newPosition.z;
        return pos;
    }

   
    Vector3 FilterAxes(Vector3 position)
    {
        return new Vector3(
            moveOnX ? position.x : transform.position.x,
            moveOnY ? position.y : transform.position.y,
            moveOnZ ? position.z : transform.position.z
        );
    }
    
    void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Count < 2) return;

        Gizmos.color = Color.cyan;
        for (int i = 0; i < waypoints.Count - 1; i++)
        {
            if (waypoints[i] != null && waypoints[i + 1] != null)
            {
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
                Gizmos.DrawSphere(waypoints[i].position, 0.1f);
            }
        }

        // if it is a loop it will go back to start (i guess)
        if (loop && waypoints[0] != null && waypoints[^1] != null)
        {
            Gizmos.DrawLine(waypoints[^1].position, waypoints[0].position);
            Gizmos.DrawSphere(waypoints[^1].position, 0.1f);
        }
    }
}
