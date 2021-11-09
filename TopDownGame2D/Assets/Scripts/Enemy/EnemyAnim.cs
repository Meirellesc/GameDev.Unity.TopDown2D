using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
    private Animator animator;

    private string transition = "transition";

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void IdleAnimation()
    {
        animator.SetInteger(transition, 0);
    }

    public void FollowAnimation()
    {
        animator.SetInteger(transition, 1);
    }

    public void AtackingAnimation()
    {
        animator.SetInteger(transition,2);
    }
}
