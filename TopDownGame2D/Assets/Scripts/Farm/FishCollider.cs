using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCollider : MonoBehaviour
{
    [SerializeField] private bool detectPlayer;

    // Percentage chance of catching a fish
    [SerializeField] private int chances;

    [SerializeField] private GameObject fishPrefab;

    private Player player;
    private PlayerAnim playerAnim;

    [SerializeField] private float iFishX;
    [SerializeField] private float iFishY;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        playerAnim = FindObjectOfType<PlayerAnim>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (detectPlayer && player.IsFishing)
        {
            playerAnim.OnFishingStarted();
        }
    }

    public void OnFishing()
    {
        int randomValue = Random.Range(1, 100);

        if(randomValue <= chances)
        {
            // Check if player is oriented to left
            if (player.Orientation == Orientation.LEFT && iFishX > 0)
            {
                Debug.Log("Change To left");
                iFishX *= -1;
            }
            else if(player.Orientation == Orientation.RIGHT && iFishX < 0)
            {
                Debug.Log("Change To right");
                iFishX *= -1;
            }
            
            // Drop the fish
            Instantiate(fishPrefab, player.transform.position + new Vector3(iFishX, iFishY, 0), transform.rotation);
        }
        else
        {
            // TODO: Feedback that not caught a fish
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            detectPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            detectPlayer = false;
        }
    }
}
