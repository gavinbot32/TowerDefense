using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Heath Components")]
    [SerializeField] private Image healthBar;

    [Header("Points")]
    [SerializeField] private TextMeshProUGUI pointText;

    [Header("Waves")]
    public GameObject waveButton;
    public TextMeshProUGUI waveText;
    public GameObject waveContainer;

    [Header("Build")]
    public GameObject buildUI;
    public GameObject buildPostProcess;

    public void updateUI()
    {
        int health = GameManager._instance.getHealth();
        int wave = GameManager._instance.waveIndex + 1;
        int waves = GameManager._instance.waves.Length;

        healthBar.fillAmount = (float)health / 100f;
        pointText.text = GameManager._instance.points.ToString();
        waveText.text = wave + "/" + waves;

    }


    private void Start()
    {
        updateUI();
    }

    public void toggleBuildUI(bool toggle)
    {
        buildUI.SetActive(toggle);
        buildPostProcess.SetActive(toggle);
    }

}
