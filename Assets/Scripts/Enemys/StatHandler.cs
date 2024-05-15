using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    [field: SerializeField] public int health { get; private set; }

    [field: SerializeField] public int bounty { get; private set; }
    [field: SerializeField] public int damage { get; private set; }
    public void TakeDamage(int damage)
    {
        health -= damage;
        DeathCheck();
    }

    private void DeathCheck()
    {
        if(health <= 0)
        {
            GameManager._instance.curEnemies.Remove(GetComponent<Enemy>());
            GameManager._instance.AddPoints(bounty);
            Destroy(gameObject);
        }
    }
}
