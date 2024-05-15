using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public int stageIndex;
    public TutorialElement[] stages;

    private void Start()
    {
        stages[0].gameObject.SetActive(true);
    }

    public void AdvanceStage()
    {
        stages[stageIndex].gameObject.SetActive(false);
        stageIndex++;
        if (stageIndex >= stages.Length) { return; }
        stages[stageIndex].gameObject.SetActive(true);
        if (stages[stageIndex].advanced) {
            AdvanceStage();
        }
    }

}
