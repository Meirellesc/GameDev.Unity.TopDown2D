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
    private Enemy enemy;

    private string transition = "transition";
    private string isHurting = "isHurting";
    private string isDead = "isDead";

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
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
        if (!enemy.isDead)
        {
            Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRadius, playerLayer);

            if (hit != null)
            {
                playerAnim.OnHurting();
            }
        }
    }

    public void OnHurting()
    {
        if (!enemy.isDead)
        {
            if (enemy.CurrentHealth <= 0)
            {
                animator.SetTrigger(isDead);
                enemy.isDead = true;

                Destroy(enemy.gameObject, 3f);
            }
            else
            {
                animator.SetTrigger(isHurting);
                enemy.CurrentHealth--;

                enemy.healthBar.fillAmount = enemy.CurrentHealth / enemy.TotalHealth;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
