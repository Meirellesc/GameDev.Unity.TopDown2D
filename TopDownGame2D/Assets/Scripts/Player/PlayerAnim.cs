using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask enemyLayer;

    private Player player;
    private Animator animator;
    private FishCollider fishCollider;

    private bool wasHurt;
    private float timeCount;
    private float recoveryTime = 1f;

    private readonly string transition = "transition";
    private readonly string isRoll = "isRoll";
    private readonly string isHurting = "isHurting";

    private void Awake()
    {
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
        fishCollider = FindObjectOfType<FishCollider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.DoingAction)
        {
            OnMove();
            OnRun();
        }

        OnCutting();
        OnDigging();
        OnWatering();
        OnCrafting();

        OnAttacking();
        OnRecovering();
    }

    #region Movement
    /// <summary>
    /// Walking and Idle animation
    /// </summary>
    private void OnMove()
    {
        if (player.Direction.sqrMagnitude > 0)
        {
            // Blocked the walking animation if the player is rolling 
            if (!OnRoll())
            {
                animator.SetInteger(transition, 1);
            }
        }
        else
        {
            animator.SetInteger(transition, 0);
        }

        // Identify if player is moving to right
        if (player.Direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
            player.Orientation = Orientation.RIGHT;
        }

        // Identify if player is moving to left
        if (player.Direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
            player.Orientation = Orientation.LEFT;
        }
    }

    /// <summary>
    /// Running animation
    /// </summary>
    private void OnRun()
    {
        if(player.IsRunning)
        {
            animator.SetInteger(transition, 2);
        }
    }

    /// <summary>
    /// Rolling animation
    /// PS: OnRoll movement, only happens when the player is moving
    /// </summary>
    /// <returns></returns>
    private bool OnRoll()
    {
        if(player.IsRolling)
        {
            animator.SetTrigger(isRoll);
            return true;
        }
        
        return false;
    }

    #endregion

    #region Action
    private void OnCutting()
    {
        if(player.IsCutting)
        {
            animator.SetInteger(transition, 3);
        }
    }

    private void OnDigging()
    {
        if (player.IsDigging)
        {
            animator.SetInteger(transition, 4);
        }
    }

    private void OnWatering()
    {
        if (player.IsWatering)
        {
            animator.SetInteger(transition, 5);
        }
    }

    /// <summary>
    /// This Funciton its called by Fish Class
    /// </summary>
    public void OnFishingStarted()
    {
        if (player.IsFishing)
        {
            animator.SetInteger(transition, 6);
        }
    }

    /// <summary>
    /// This Function is triggered when finish the fishing animation
    /// then, call the Fishing function into Fish Class
    /// </summary>
    public void OnFishingEnded()
    {
        fishCollider.OnFishing();
    }


    private void OnCrafting()
    {
        if (player.IsCrafting)
        {
            animator.SetInteger(transition, 7);
        }
    }
    #endregion

    #region Combat
    public void OnAttacking()
    {
        if (player.IsAttacking)
        {
            animator.SetInteger(transition, 8);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    public void OnHurting()
    {
        if (!wasHurt)
        {
            animator.SetTrigger(isHurting);
            player.CurrentHealth -= Random.Range(1, 3);

            player.healthBar.fillAmount = player.CurrentHealth / player.TotalHealth;

            wasHurt = true;
        }
    }

    private void OnRecovering()
    {
        if (wasHurt)
        {
            // Add timeCount in seconds
            timeCount += Time.deltaTime;

            if (timeCount >= recoveryTime)
            {
                wasHurt = false;
                timeCount = 0f;
            }
        }
    }
    #endregion
}
