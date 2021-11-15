using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    // Life
    [SerializeField] private float _health;
    public float CurrentHealth
    {
        get { return _health; }
        set { _health = value; }
    }
    public float TotalHealth;
    public Image healthBar;
    public bool isDead;

    [Header("Combat")]
    [SerializeField] float detectPlayerRadius;
    private bool isDetectedPlayer;

    [Header("Components")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private EnemyAnim enemyAnim;

    public LayerMask playerMask;
    private Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        _health = TotalHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead && isDetectedPlayer)
        {
            Destination();

            Orientation();

            if (Vector2.Distance(transform.position, player.transform.position) <= agent.stoppingDistance)
            {
                Atack();
            }
            else
            {
                Follow();
            }
        }
    }

    private void FixedUpdate()
    {
        DetectPlayer();    
    }

    #region AI Movement 
    private void Destination()
    {
        agent.SetDestination(player.transform.position);
    }

    private void Orientation()
    {
        float enemyAndPlayerDist = player.transform.position.x - transform.position.x;


        if (enemyAndPlayerDist > 0)
        {
            //Right
            transform.eulerAngles = new Vector2(0, 0);
        }
        else
        {
            //Left
            transform.eulerAngles = new Vector2(0, 180);
        }
    }

    private void Follow()
    {
        enemyAnim.FollowAnimation();
    }
    #endregion

    #region Combat
    private void Atack()
    {
        enemyAnim.AtackingAnimation();
    }


    public void DetectPlayer()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, detectPlayerRadius, playerMask);

        if (hit != null)
        {
            isDetectedPlayer = true;
            agent.isStopped = false;
        }
        else
        {
            isDetectedPlayer = false;
            enemyAnim.IdleAnimation();
            agent.isStopped = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectPlayerRadius);
    }

    /// <summary>
    /// Detect trigger between two colliders
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sword"))
        {
            enemyAnim.OnHurting();
        }
    }
    #endregion

}
