using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPrefab : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float timeMove;
    [SerializeField] private AnimationCurve curve;
    private float timeCount;

    private Vector3 start;
    private Vector3 end;

    private float[] xRange = { 2f, 1.5f, 1f };
    private float[] yRange = { 2f, 2.5f, 3f };

    private SpriteRenderer spriteRenderer;

    private Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        start = transform.position;

        int xRandom = Random.Range(0, xRange.Length);
        int yRandom = Random.Range(0, yRange.Length);

        end = transform.position + new Vector3(xRange[xRandom], yRange[yRandom], 0);

        // Check if player is oriented to left, then throw the fish to right
        if(player.Orientation == Orientation.LEFT)
        {
            Debug.Log("Prefab Left");
            end = transform.position + new Vector3(-xRange[xRandom], yRange[yRandom], 0);

            // Flip sprite
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;

        if (timeCount < timeMove)
        {
            Vector3 pos = Vector3.Lerp(start, end, timeCount * speed);

            pos.y += curve.Evaluate(timeCount * speed);

            transform.position = pos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerItems>().TotalFish++;

            Destroy(gameObject);
        }
    }
}
