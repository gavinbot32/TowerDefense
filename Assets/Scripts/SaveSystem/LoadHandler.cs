using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadHandler : MonoBehaviour
{
    public string prefix;
    public Image[] stars;

    private void Start()
    {
        LoadStars();
    }

    public void LoadStars()
    {
        int starsAmount = 0;
        string starsName = prefix+"_stars".ToString();
        if (PlayerPrefs.HasKey(starsName)){
            starsAmount = PlayerPrefs.GetInt(starsName);
        }
        for (int i = 0; i < starsAmount; i++)
        {
            stars[i].color = Color.white;
        }
    }
}
