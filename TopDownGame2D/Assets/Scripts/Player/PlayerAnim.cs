using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Player player;
    private Animator animator;

    private readonly string transition = "transition";
    private readonly string isRoll = "isRoll";

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        OnMove();
        OnRun();
        OnCutting();
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
        }

        // Identify if player is moving to left
        if (player.Direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
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

    private void OnCutting()
    {
        if(player.IsCutting)
        {
            animator.SetInteger(transition, 3);
        }
    }

    #endregion
}
