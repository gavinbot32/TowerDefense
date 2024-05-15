using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public Animator anim;

    public bool toggle;
    public void AnimationTrigger(string trigger)
    {
        anim.SetTrigger(trigger);
        
    }

    public void AnimationBool(string trigger)
    {
        toggle = !toggle;
        anim.SetBool(trigger,toggle);
    }
}
