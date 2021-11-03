using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private bool detectPlayer;
    [SerializeField] private int waterValue;

    private PlayerItems playerItems;

    // Start is called before the first frame update
    void Start()
    {
        playerItems = FindObjectOfType<PlayerItems>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(detectPlayer && Input.GetKeyDown(KeyCode.F))
        {
            playerItems.TotalWater += waterValue;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
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
