using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public GameObject waypointParent;
    public GameObject[] waypoints;

    [Header("Gizmos")]
    [SerializeField] private float sphereRadius;
    private void OnDrawGizmos()
    {
        foreach(Transform transform in waypointParent.GetComponentsInChildren<Transform>())
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, sphereRadius);
        
        }
        for(int i = 0; i < waypoints.Length; i++)
        {
            if (i >= waypoints.Length-1) return;
            
            Gizmos.color = Color.red;
            Gizmos.DrawLine(waypoints[i].transform.position, waypoints[i + 1].transform.position);
            
        }
    }
}
