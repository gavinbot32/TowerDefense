using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Handlers")]
    public StatHandler h_Stat;
    public EnemyMovement h_Movement;

    private void Awake()
    {
        h_Stat = GetComponent<StatHandler>();
        h_Movement = GetComponent<EnemyMovement>();
    }
}
