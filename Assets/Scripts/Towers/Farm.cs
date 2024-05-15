using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FarmType
{
    Points,
    Health
}

public class Farm : MonoBehaviour
{

    public FarmType type;
    public int amount; 
    public Vector2 cooldownMinMax;
    [SerializeField] private GameObject pipPrefab;
    private float cooldown;

    private void Start()
    {
        cooldown = Random.Range(cooldownMinMax.x, cooldownMinMax.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager._instance.isPlaying)
        {
            if (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
            }
            if (cooldown <= 0)
            {
                cooldown = Random.Range(cooldownMinMax.x, cooldownMinMax.y);
                Harvest();
            }
        }
    }

    private void Harvest()
    {
        switch (type)
        {
            case FarmType.Points:
                GameManager._instance.AddPoints(amount);
                break;
            case FarmType.Health:
                GameManager._instance.GainHealth(amount);
                break;
        }
        Instantiate(pipPrefab, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity).GetComponent<FarmPip>().initialize(type, amount.ToString());
            
    }

}
