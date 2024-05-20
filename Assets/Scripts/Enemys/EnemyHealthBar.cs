using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Enemy owner;
    [SerializeField] private Transform fill;
    private int maxHealth;

    private void Start()
    {
        maxHealth = owner.h_Stat.health;
    }
    private void Update()
    {
        fill.localScale = new Vector3 ((float)owner.h_Stat.health/(float)maxHealth,fill.transform.localScale.y,fill.transform.localScale.z);
    }
}
