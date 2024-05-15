using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform tm_target;
    private StatHandler h_stats;
    [field: Header("Movement")]
    [field:SerializeField] public float speed { get; private set; }

    [Header("Waypoints")]
    private WaypointManager mn_waypoint;
    public int waypointIndex;

    private void Awake()
    {
        h_stats = GetComponent<StatHandler>();
    }

    private void Start()
    {
        mn_waypoint = FindObjectOfType<WaypointManager>();
        waypointIndex = 0;
        tm_target = mn_waypoint.waypoints[0].transform;
        Vector3 targetPos = new Vector3(tm_target.position.x, tm_target.position.y + transform.localScale.y, tm_target.position.z);

        transform.position = targetPos;
    }
    private void Update()
    {
        tm_target = mn_waypoint.waypoints[waypointIndex].transform;
        Vector3 targetPos = new Vector3(tm_target.position.x, tm_target.position.y+transform.localScale.y, tm_target.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);

        if(transform.position == targetPos)
        {
            if (waypointIndex == mn_waypoint.waypoints.Length - 1)
            {
                waypointIndex = 0;
                GameManager._instance.TakeDamage(h_stats.damage);
                GameManager._instance.curEnemies.Remove(GetComponent<Enemy>());
                Destroy(gameObject);
            }
            waypointIndex++;
        }
    }
}
