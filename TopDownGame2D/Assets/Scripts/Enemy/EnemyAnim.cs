using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask playerLayer;

    private PlayerAnim playerAnim;

    private Animator animator;

    private string transition = "transition";

    private void Awake()
    {
        playerAnim = FindObjectOfType<PlayerAnim>();
    }

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

    public void AtackAction()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRadius, playerLayer);

        if(hit != null)
        {
            playerAnim.OnHurting();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
