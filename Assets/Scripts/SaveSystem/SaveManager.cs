using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    public SaveData SaveData;

    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI pointText;

    [SerializeField] private TextMeshProUGUI worldText;
    [SerializeField] private TextMeshProUGUI levelText;

    [SerializeField] private Image[] stars;
    private void Start()
    {
        SavePrefs();
    }

    private void updateUI(int starsAmount)
    {
        healthText.text = SaveData.health.ToString()+"%";
        pointText.text = SaveData.points.ToString();

        worldText.text = "World-"+SaveData.world.ToString();
        levelText.text = "Level-"+SaveData.level.ToString();

        for(int i = 0; i < starsAmount; i++)
        {
            stars[i].color = Color.white;
        }
    }

    private int CalculateStars(int health, int points)
    {

        if (health >= 90)
        {
            return 3;
        }
        else if (health >= 70)
        {
            return 2;
        }
        else if (health >= 50)
        {
            return 1;
        }
        return 0;
    }

    private void SavePrefs()
    {
        int stars = CalculateStars(SaveData.health, SaveData.points);
        updateUI(stars);
        string prefix = SaveData.world.ToString()+SaveData.level.ToString();
        string starsName = prefix+"_stars".ToString();
        PlayerPrefs.SetInt(starsName, stars);
        PlayerPrefs.Save();
    }

    private int LoadStarPrefs(int world, int level)
    {
        string prefix = world.ToString()+level.ToString();
        string starsName = prefix + "_stars".ToString();
        if (PlayerPrefs.HasKey(starsName))
        {
            return PlayerPrefs.GetInt(starsName,0);
        }
        else
        {
            return 0;
        }
    }

    public void testLoad()
    {

        print(LoadStarPrefs(SaveData.world, SaveData.level));
    }
}
