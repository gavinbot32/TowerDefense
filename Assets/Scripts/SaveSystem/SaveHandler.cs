using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveHandler : MonoBehaviour
{
    public SaveData SaveData;
    public int world;
    public int level;

    public void PopulateSave()
    {
        int health = GameManager._instance.getHealth();
        int points = GameManager._instance.points;
        SaveData.health = health;
        SaveData.points = points;
        SaveData.world = world;
        SaveData.level = level;
    }
}
