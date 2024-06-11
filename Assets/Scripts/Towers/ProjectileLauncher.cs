using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum Priority
{
    First,
    Strong,
    Fast,
    Closest,
    Waypoint
}

public enum PathPosition
{
    Top,Bottom, Left, Right
}
public class ProjectileLauncher : MonoBehaviour
{
    private delegate Enemy AttackMethod();
    private Tower _tower;
    [SerializeField] private Animator animator;

    [Header("Range Variables")]
    [SerializeField] private float range;
    [SerializeField] private SphereCollider rangeCollider;

    [Header("Targeting Parameters")]


    [SerializeField] private Priority priority;
    private PathPosition pathPosition;
    private bool pathSideFound;
    private List<Enemy> curEnemiesInRange;
    [SerializeField] private Enemy curTarget;
    private Transform tarWaypoint;
    private AttackMethod attackMethod;


    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackDur;
    [SerializeField] Transform rotatingPart;
    private Vector3 defaultAngles;
    

    [Header("Projectile Variables")]
    [SerializeField] Transform muzzle;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private int projectileDamage;

    private void Awake()
    {
        _tower = GetComponent<Tower>();
        curEnemiesInRange = new List<Enemy>();
        
    }

    private void Start()
    {
        rangeCollider.radius = range;
        if (defaultAngles == null)
        {
            defaultAngles = rotatingPart.eulerAngles;
        }
        GameObject closest = GameManager._instance._waypointManager.waypoints[0];
        foreach(GameObject waypoint in GameManager._instance._waypointManager.waypoints)
        {
            if (Vector3.Distance(waypoint.transform.position, transform.position) 
                < Vector3.Distance(transform.position, closest.transform.position)){
                closest = waypoint;
            }
        }
        tarWaypoint = closest.transform;
        SetAttackMethod();
    }


    private void SetAttackMethod()
    {
        switch (priority)
        {
            case Priority.First:
                attackMethod = new AttackMethod(AttackPriorityFirst);
                break;
            case Priority.Fast:
                attackMethod = new AttackMethod(AttackPriorityFast);
                break;
            case Priority.Strong:
                attackMethod = new AttackMethod(AttackPriorityStrongest);
                break;
            case Priority.Closest:
                attackMethod = new AttackMethod(AttackPriorityClosest);
                break;
            case Priority.Waypoint:
                attackMethod = new AttackMethod(AttackPriorityWaypoint);
                break;
            default:
                attackMethod = new AttackMethod(AttackPriorityFirst);
                break;

        }
    }
    #region
    private Enemy AttackPriorityFirst()
    {
        return curEnemiesInRange[0];
    }
    private Enemy AttackPriorityFast()
    {
        Enemy ret = null;
        Enemy fastest = curEnemiesInRange[0];
        foreach (Enemy e in curEnemiesInRange)
        {
            if (e.h_Movement.speed > e.h_Movement.speed)
            {
                fastest = e;
            }
        }
        ret = fastest;
        return ret;
    }
    private Enemy AttackPriorityClosest()
    {
        Enemy ret = null;
        Enemy closest = curEnemiesInRange[0];
        foreach (Enemy e in curEnemiesInRange)
        {
            if (Vector3.Distance(transform.position, e.transform.position) < Vector3.Distance(transform.position, closest.transform.position))
            {
                closest = e;
            }
        }
        ret = closest;
        return ret;
    }
    private Enemy AttackPriorityStrongest()
    {
        Enemy ret = null;
        Enemy largestHealth = curEnemiesInRange[0];
        foreach (Enemy e in curEnemiesInRange)
        {
            if (e.h_Stat.health > largestHealth.h_Stat.health)
            {
                largestHealth = e;
            }
        }
        ret = largestHealth;
        return ret;
    }
    private Enemy AttackPriorityWaypoint()
    {
        Enemy ret = null;
        Enemy closest2 = curEnemiesInRange[0];
        foreach (Enemy e in curEnemiesInRange)
        {
            if (Vector3.Distance(transform.position, e.transform.position) < Vector3.Distance(transform.position, closest2.transform.position))
            {
                closest2 = e;
            }
        }
        ret = closest2;
        return ret;
    }
    #endregion
    private void OnValidate()
    {
        rangeCollider.radius = range;

    }

    private void Update()
    {
        CooldownDecays();
        List<Enemy> toRemove = new List<Enemy>();
        foreach(Enemy enemy in curEnemiesInRange)
        {
            if(enemy == null)
            {
                toRemove.Add(enemy);
                curTarget = null;
            }
        }

        foreach(Enemy enemy in toRemove)
        {
            curEnemiesInRange.Remove(enemy);
        }
        if(attackCooldown <= 0)
        {
            attackCooldown = attackDur;
            curTarget = GetCurrentEnemy();
            if (curTarget != null)
            {
                Attack();
            }
            else
            {
                rotatingPart.eulerAngles = defaultAngles;
            }
        }    
    }

    private void CooldownDecays()
    {
        if(attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    private Enemy GetCurrentEnemy()
    {
        Enemy ret = null;
        if (curEnemiesInRange.Count <= 0) return null;
      /* switch (priority)
        {
            case Priority.First:
                ret = curEnemiesInRange[0];
                return ret;
            case Priority.Strong:
                Enemy largestHealth = curEnemiesInRange[0];
                foreach (Enemy e in curEnemiesInRange)
                {
                    if(e.h_Stat.health > largestHealth.h_Stat.health)
                    {
                        largestHealth =  e; 
                    }
                }
                ret = largestHealth;
                return ret;
            case Priority.Fast:
                Enemy fastest = curEnemiesInRange[0];
                foreach (Enemy e in curEnemiesInRange)
                {
                    if (e.h_Movement.speed > e.h_Movement.speed)
                    {
                        fastest = e;
                    }
                }
                ret = fastest;
                return ret;
            case Priority.Closest:
                Enemy closest = curEnemiesInRange[0];
                foreach(Enemy e in curEnemiesInRange)
                {
                    if(Vector3.Distance(transform.position,e.transform.position) < Vector3.Distance(transform.position, closest.transform.position))
                    {
                        closest = e;
                    }
                }
                ret = closest;
                return ret;
            case Priority.Waypoint:
                Enemy closest2 = curEnemiesInRange[0];
                foreach (Enemy e in curEnemiesInRange)
                {
                    if (Vector3.Distance(transform.position, e.transform.position) < Vector3.Distance(transform.position, closest2.transform.position))
                    {
                        closest2 = e;
                    }
                }
                ret = closest2;
                return ret;
        }*/
        ret = attackMethod.Invoke();
        return ret;
    }

    public void SetDefaultAngles(Vector3 angles)
    {
        defaultAngles = angles;
    }

    private void Attack()
    {
        if(animator != null)
        {
            animator.SetTrigger("Attack");
            animator.SetFloat("SpeedMultiplier", 1 / attackDur);
        }
        rotatingPart.LookAt(curTarget.transform);
        rotatingPart.eulerAngles = new Vector3(0,rotatingPart.eulerAngles.y,0);

        bool posDir = false;
        Vector3 cannonDir = Vector3.Normalize(transform.position - curTarget.transform.position);
        /*
                switch (pathPosition)
                {
                    case PathPosition.Left:
                        if (cannonDir.z > 0) posDir = true;
                        break;
                    case PathPosition.Right:
                        if(cannonDir.z < 0) posDir = true;
                        break;
                    case PathPosition.Top:
                        if(cannonDir.x < 0 )posDir = true;
                        break;
                    case PathPosition.Bottom:
                        if(cannonDir.x > 0) posDir = true;
                        break;
                }*/

        if(_tower.triggerSounds.Length > 0 ) _tower.audioSource.PlayOneShot(_tower.RandomSound(_tower.triggerSounds));


        Instantiate(projectilePrefab, muzzle.position, Quaternion.identity)
        .GetComponent<Projectile>().Initialize(curTarget, projectileSpeed, projectileDamage, GetComponent<Tower>(),posDir);
        
    }

   /* private PathPosition FindSide(Transform targetTransform,Transform secondaryTransform)
    {
        PathPosition ret = PathPosition.Top;
        Vector3 cannonDir = Vector3.Normalize(transform.position - targetTransform.transform.position);
        Vector3 cannonDir2 = Vector3.Normalize(transform.position - secondaryTransform.transform.position);
        print("Dir1: "+cannonDir);
        print("Dir2: "+cannonDir2);

        if (cannonDir.x < 0 && cannonDir2.x < 0) ret = PathPosition.Right;
        else if (cannonDir.x > 0 && cannonDir2.x > 0) ret = PathPosition.Left;
        else if (cannonDir.z < 0 && cannonDir2.z < 0) ret = PathPosition.Top;
        else if (cannonDir.z > 0 && cannonDir2.z > 0) ret = PathPosition.Bottom;


        return ret;
    }*/
    //Left+X-Z

    //Right side of path: -X  Z Changes based on target
    //Top Of Path: -Z X Changes based on target

    //Left Side: +X Z Changes based on target +Z positive Dir
    //Bottom of path: +Z X Changes based on target


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            curEnemiesInRange.Add(other.GetComponent<Enemy>());
           // int wayPointIndex = other.GetComponent<Enemy>().h_Movement.waypointIndex;

            /*  pathPosition = FindSide(other.transform, GameManager._instance._waypointManager.waypoints[wayPointIndex+1].transform);
            pathSideFound = true;
            print(pathPosition);*/
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            curEnemiesInRange.Remove(other.GetComponent<Enemy>());
            curTarget = GetCurrentEnemy();
        }   
    }

    

}
