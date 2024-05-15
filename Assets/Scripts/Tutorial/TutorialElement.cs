using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialElement : MonoBehaviour
{
    public bool isTimer;

    public float timer;
    private bool startTimer;
    private TutorialManager tutorial;

    public bool advanced = false;

    private void Awake()
    {
        tutorial = FindObjectOfType<TutorialManager>();
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (isTimer) { 
        startTimer = true;
        }
    }

    void Update()
    {
        if(isTimer && startTimer)
        {
            timer -= Time.deltaTime;
        }
        if(isTimer && timer <= 0)
        {
            AdvanceStage();
        }
    }


    public void AdvanceStage()
    {
        if (!advanced)
        {
            advanced = true;
            tutorial.AdvanceStage();
            this.enabled = false;
        }
    }

}
