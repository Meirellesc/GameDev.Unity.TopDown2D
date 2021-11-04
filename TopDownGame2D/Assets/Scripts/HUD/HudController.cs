using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    [SerializeField] Image woodBar;
    [SerializeField] Image waterBar;
    [SerializeField] Image carrotBar;

    private PlayerItems playerItems;

    private void Awake()
    {
        playerItems = FindObjectOfType<PlayerItems>();
    }

    // Start is called before the first frame update
    void Start()
    {
        woodBar.fillAmount = 0f;
        waterBar.fillAmount = 0f;
        carrotBar.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        woodBar.fillAmount = playerItems.TotalWood / playerItems.woodMaxLimit;
        waterBar.fillAmount = playerItems.TotalWater / playerItems.waterMaxLimit;
        carrotBar.fillAmount = playerItems.TotalCarrot / playerItems.carrotMaxLimit;
    }
}
