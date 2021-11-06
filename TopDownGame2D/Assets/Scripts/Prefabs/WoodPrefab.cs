using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodPrefab : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float timeMove;
    [SerializeField] private AnimationCurve curve;
    private float timeCount;

    private Vector3 start;
    private Vector3 end;

    private float[] xRange = { -2f, -1.5f, -1, 1, 1.5f, 2f };
    private float[] yRange = { -1.3f, -1f, -0.5f, 0.5f, 1f, 1.3f };

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        start = transform.position;

        int xRandom = Random.Range(0, xRange.Length);
        int yRandom = Random.Range(0, yRange.Length);
        end = transform.position + new Vector3(xRange[xRandom], yRange[yRandom], 0);
        
        // Flip sprite if goes to left of tree
        if(xRange[xRandom] < 0)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;

        if(timeCount < timeMove)
        {
            Vector3 pos = Vector3.Lerp(start, end, timeCount * speed);

            pos.y += curve.Evaluate(timeCount * speed);

            transform.position = pos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerItems>().TotalWood++;

            Destroy(gameObject);
        }
    }
}
