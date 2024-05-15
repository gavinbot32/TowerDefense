using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerType { 
    Ranged,
    Melee,
    Support,
    }

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TowerData", order = 1)]
public class TowerData : ScriptableObject
{

    public GameObject prefab;
    public Sprite towerIcon;
    public string towerName;
    public TowerType type;
    public int cost;
    [Tooltip("Only use if is a ranged tower")]
    public float range;

}
