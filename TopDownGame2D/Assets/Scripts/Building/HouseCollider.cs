using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseCollider : MonoBehaviour
{
    [SerializeField] private bool detectPlayer;
    [SerializeField] private int buildingHitCost;
    [SerializeField] private int buildingItemCost;
    private int buildingHit;

    [SerializeField] private SpriteRenderer houseBackSprite;
    [SerializeField] private SpriteRenderer houseFrontSprite;

    [SerializeField] private Color showBuildingColor;
    [SerializeField] private Color hideBuildingColor;

    [SerializeField] private Color firstHitColor;
    [SerializeField] private Color secondHitColor;
    [SerializeField] private Color thirdHitColor;

    private PlayerItems playerItems;
    private Player player;

    private void Awake()
    {
        playerItems = FindObjectOfType<PlayerItems>();
        player = FindObjectOfType<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (buildingHit == 0)
        {
            OnShowingBuilding();
        }
    }

    private void OnShowingBuilding()
    {
        if(detectPlayer)
        {
            houseBackSprite.color = showBuildingColor;
            houseFrontSprite.color = showBuildingColor;
        }
        else
        {
            houseBackSprite.color = hideBuildingColor;
            houseFrontSprite.color = hideBuildingColor;
        }
    }

    private void OnBuildingHit()
    {
        if (buildingHit < buildingHitCost && playerItems.TotalWood >= buildingItemCost)
        {
            buildingHit++;

            playerItems.TotalWood -= buildingItemCost;

            BuildingAlphaColor();
        }
    }

    private void BuildingAlphaColor()
    {
        if(buildingHit == 1)
        {
            houseBackSprite.color = firstHitColor;
            houseFrontSprite.color = firstHitColor;
        }
        else if(buildingHit == 2)
        {
            houseBackSprite.color = secondHitColor;
            houseFrontSprite.color = secondHitColor;
        }
        else if(buildingHit == 3)
        {
            houseBackSprite.color = thirdHitColor;
            houseFrontSprite.color = thirdHitColor;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            detectPlayer = true;
        }

        if (collision.CompareTag("Hammer"))
        {
            OnBuildingHit();
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
