using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private EnemyAnim enemyAnim;

    private Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
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

    private void Atack()
    {
        enemyAnim.AtackingAnimation();
    }
}
