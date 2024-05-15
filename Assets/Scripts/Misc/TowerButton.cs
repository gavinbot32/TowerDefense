using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class TowerButton : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI towerName;
    public TextMeshProUGUI costText;
    [SerializeField] private BuildHandler buildHandler;
    [SerializeField] private TowerData towerData;
    [SerializeField] private GameObject farmInfo;
    [SerializeField] private Image farmIcon;
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private TextMeshProUGUI secText;
    [SerializeField] private Sprite healthIcon;
    [SerializeField] private Sprite pointsIcon;
    [SerializeField] private Color healthColor;
    [SerializeField] private Color pointColor;
    public void initialize(TowerData tower)
    {
        towerData = tower;
        buildHandler = FindObjectOfType<BuildHandler>();
        icon.sprite = towerData.towerIcon;
        towerName.text = towerData.towerName;
        costText.text = "$" + towerData.cost.ToString();
        farmInfo.SetActive(false);
        if(tower.prefab.GetComponent<Farm>() != null)
        {
            Farm farm = tower.prefab.GetComponent<Farm>();
            farmInfo.SetActive(true);
            switch (farm.type)
            {
                case FarmType.Health:
                    farmIcon.sprite = healthIcon;
                    secText.color = healthColor;
                    amountText.color = healthColor;
                    break;
                case FarmType.Points:
                    farmIcon.sprite = pointsIcon;
                    secText.color = pointColor;
                    amountText.color = pointColor;
                    break;
                default:
                    farmIcon.sprite = pointsIcon;
                    secText.color = pointColor;
                    amountText.color = pointColor;
                    break;
            }
            amountText.text = MathF.Round((float)farm.amount/farm.cooldownMinMax.y,1).ToString() +"-" + MathF.Round((float)farm.amount / farm.cooldownMinMax.x,1).ToString();
        }

    }

    public void startBuilding()
    {
        buildHandler.startBuilding(towerData);
    }

}
