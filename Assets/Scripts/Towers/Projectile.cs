using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Enemy target;
    private float speed;
    private int damage;
    private Tower tower;
    public float lifeTime;
    public int health;
    private bool posIndex;
    private Transform waypoint;
    private void Update()
    {
        if(lifeTime > 0)
        {
            lifeTime -= Time.deltaTime;
            if(lifeTime <= 0 ) { 
                Destroy(gameObject);
            }
        }


        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position,target.transform.position, Time.deltaTime * speed);
        transform.LookAt(target.transform.position);
    }
    public void Initialize(Enemy curTarget, float speed, int damage, Tower tower, bool posDirection)
    {
        target = curTarget;
        this.speed = speed;
        this.damage = damage;
        this.tower = tower;
        this.posIndex = posDirection;
    }

    public Enemy CheckAndChange()
    {
        Enemy exTarget = target;
        health--;
        if(health <= 0)
        {
            Destroy(gameObject);
            return exTarget;
        }
        if (posIndex)
        {
            int enemyIndex = GameManager._instance.curEnemies.IndexOf(target);
            if (enemyIndex < GameManager._instance.curEnemies.Count-1)
            {
                target = GameManager._instance.curEnemies[enemyIndex + 1];
                return exTarget;

            }
        }
        else
        {
            int enemyIndex = GameManager._instance.curEnemies.IndexOf(target);
            if (enemyIndex > 0)
            {
                target = GameManager._instance.curEnemies[enemyIndex - 1];
                return exTarget;
            }
        }
        //Destroy(gameObject);
        return exTarget;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == target.transform)
        {
            CheckAndChange().h_Stat.TakeDamage(damage);
        }
        else
        {
            if(other.gameObject.GetComponent<Enemy>() != null) {
            
                other.gameObject.GetComponent<Enemy>().h_Stat.TakeDamage(damage);
                health--;
                if (health <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
        
    }

}
