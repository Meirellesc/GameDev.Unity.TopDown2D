using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    [Header("Items")]
    [SerializeField] Image woodBar;
    [SerializeField] Image waterBar;
    [SerializeField] Image carrotBar;
    [SerializeField] Image fishBar;

    [Header("Tools")]
    [SerializeField] Image swordUI;
    [SerializeField] Image swordIndicator;
    [SerializeField] Image axeUI;
    [SerializeField] Image axeIndicator;
    [SerializeField] Image shovelUI;
    [SerializeField] Image shovelIndicator;
    [SerializeField] Image wateringCanUI;
    [SerializeField] Image wateringCanIndicator;
    [SerializeField] Image rodUI;
    [SerializeField] Image rodIndicator;
    [SerializeField] Image hammerUI;
    [SerializeField] Image hammerIndicator;

    private Dictionary<int, Image> toolsUI = new Dictionary<int, Image>();
    private Dictionary<int, Image> toolsIndicator = new Dictionary<int, Image>();
    [SerializeField] Color selectedColor;
    [SerializeField] Color unselectedColor;


    private PlayerItems playerItems;
    private Player player;

    private void Awake()
    {
        playerItems = FindObjectOfType<PlayerItems>();
        player = playerItems.GetComponent<Player>();

        toolsUI.Add(1, swordUI);
        toolsUI.Add(2, axeUI);
        toolsUI.Add(3, shovelUI);
        toolsUI.Add(4, wateringCanUI);
        toolsUI.Add(5, rodUI);
        toolsUI.Add(6, hammerUI);

        toolsIndicator.Add(1, swordIndicator);
        toolsIndicator.Add(2, axeIndicator);
        toolsIndicator.Add(3, shovelIndicator);
        toolsIndicator.Add(4, wateringCanIndicator);
        toolsIndicator.Add(5, rodIndicator);
        toolsIndicator.Add(6, hammerIndicator);
    }

    // Start is called before the first frame update
    void Start()
    {
        woodBar.fillAmount = 0f;
        waterBar.fillAmount = 0f;
        carrotBar.fillAmount = 0f;
        fishBar.fillAmount = 0f;

        HandlingObjUI();
    }

    // Update is called once per frame
    void Update()
    {
        woodBar.fillAmount = playerItems.TotalWood / playerItems.woodMaxLimit;
        waterBar.fillAmount = playerItems.TotalWater / playerItems.waterMaxLimit;
        carrotBar.fillAmount = playerItems.TotalCarrot / playerItems.carrotMaxLimit;
        fishBar.fillAmount = playerItems.TotalFish / playerItems.fishMaxLimit;

        // Performance: Modifies the tools HUD image only when the player change the tool
        if (player.hasHandledObj)
        {
            HandlingObjUI();

            player.hasHandledObj = false;
        }
    }

    /// <summary>
    /// Function to change the tools UI alpha color accordlying which tool is equiped
    /// </summary>
    private void HandlingObjUI()
    {
        // Find into toolsUI Dictionary, which tool are equiped
        // and set the selected color
        for (int i = 1; i <= toolsUI.Count; i++)
        {
            if (i == player.HandlingObj)
            {
                toolsUI[i].color = selectedColor;
            }
            else
            {
                if (toolsUI.ContainsKey(i))
                {
                    toolsUI[i].color = unselectedColor;   
                }
            }
        }

        // Find into toolsIndicator Dictionary, which tool are equiped
        // and enable the respective indicator
        for (int i = 1; i <= toolsIndicator.Count; i++)
        {
            if (i == player.HandlingObj)
            {
                toolsIndicator[i].enabled = true;
            }
            else
            {
                if (toolsIndicator.ContainsKey(i))
                {
                    toolsIndicator[i].enabled = false;
                }
            }
        }
    }
}
