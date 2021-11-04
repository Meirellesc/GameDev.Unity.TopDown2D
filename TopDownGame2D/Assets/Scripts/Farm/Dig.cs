using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dig : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite holeSprite;
    [SerializeField] private Sprite carrotSprite;

    [Header("Settings")]
    // Quantity of digging to open a hole
    [SerializeField] private int digAmout;

    // Quantity of watering to plant a carrot
    [SerializeField] private float waterAmount;
    [SerializeField] private bool wateringDetect;

    private int initialDigAmount;
    private float currentWaterAmount;

    private bool hasHole;
    private bool hasCarrot;

    private PlayerItems playerItems;

    private void Awake()
    {
        playerItems = FindObjectOfType<PlayerItems>();
    }

    // Start is called before the first frame update
    void Start()
    {
        initialDigAmount = digAmout;
    }

    // Update is called once per frame
    void Update()
    {
        OnWatering();

        OnCollectCarrot();
    }

    private void OnDig()
    {
        // Check if still has to dig
        if (digAmout > 0)
        {
            digAmout--;
        }
        else
        {
            spriteRenderer.sprite = holeSprite;
            hasHole = true;
        }
    }

    private void OnWatering()
    {
        if (hasHole)
        {
            // Check if is watering
            if (wateringDetect)
            {
                currentWaterAmount += 0.01f;
            }

            // Check if filled the water to plant a carrot
            if (currentWaterAmount >= waterAmount)
            {
                spriteRenderer.sprite = carrotSprite;
                hasCarrot = true;
            }
        }
    }

    private void OnCollectCarrot()
    {
        if(hasCarrot)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                playerItems.TotalCarrot++;
                spriteRenderer.sprite = holeSprite;
                currentWaterAmount = 0;
                hasCarrot = false;
            }
        }
    }

    /// <summary>
    /// Detect trigger between two colliders
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shovel"))
        {
            OnDig();
        }

        if (collision.CompareTag("WateringCan"))
        {
            wateringDetect = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("WateringCan"))
        {
            wateringDetect = false;
        }
    }
}
