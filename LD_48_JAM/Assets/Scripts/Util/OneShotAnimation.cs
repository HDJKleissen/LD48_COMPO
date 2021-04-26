using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShotAnimation : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAnimationFinished())
        {
            Destroy(gameObject);
        }    
    }
    public bool IsAnimationFinished()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length <= animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}
