using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private TowerData[] towers;
    [SerializeField] private GameObject towerButton;
    [SerializeField] private Transform container;

    private void Start()
    {
        foreach(TowerData tower in towers)
        {
            Instantiate(towerButton, container).GetComponent<TowerButton>().initialize(tower);
        }
    }
}
