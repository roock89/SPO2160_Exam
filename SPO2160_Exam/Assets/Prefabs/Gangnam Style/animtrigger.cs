using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animtrigger : MonoBehaviour
{
    Animator animator;
    bool started;
    public bool beginOnStart;
    public bool Loop;
    void Start()
    {
        animator = GetComponent<Animator>();
        if(!beginOnStart)
            this.enabled = false;
        else
            animator.SetBool("animate", true);
        started = true;
        if(!Loop)
            animator.SetBool("Loop", false);
    }

    void OnValidate()
    {
        
    }

    void OnEnable()
    {
        if(started)
            animator.SetBool("animate", true);
    }

    void OnDisable()
    {
        animator.SetBool("animate", false);
    }
}
