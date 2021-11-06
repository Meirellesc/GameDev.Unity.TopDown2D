using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    [SerializeField] public float woodMaxLimit { get; private set; } = 25;
    [SerializeField] public float waterMaxLimit { get; private set; } = 50;
    [SerializeField] public float carrotMaxLimit { get; private set; } = 10;
    [SerializeField] public float fishMaxLimit { get; private set; } = 10;

    // Wood
    [SerializeField] private int _totalWood;
    public int TotalWood
    {
        get => _totalWood;
        set => _totalWood = CheckLimitItem(value, woodMaxLimit);
    }

    // Water
    [SerializeField] private float _totalWater;
    public float TotalWater
    {
        get => _totalWater;
        set => _totalWater = CheckItemLimit(value, waterMaxLimit);
    }

    // Carrot
    [SerializeField] private int _totalCarrot;
    public int TotalCarrot
    {
        get => _totalCarrot;
        set => _totalCarrot = CheckLimitItem(value, carrotMaxLimit);
    }

    // Fish
    [SerializeField] private int _totalFish;
    public int TotalFish
    {
        get => _totalFish;
        set => _totalFish = CheckLimitItem(value, fishMaxLimit);
    }

    private int CheckLimitItem(int value, float maxLimit)
    {
        if (value > maxLimit)
        {
            return (int)maxLimit;
        }

        if(value < 0)
        {
            return 0;
        }

        return value;
    }

    private float CheckItemLimit(float value, float maxLimit)
    {
        if (value > maxLimit)
        {
            return maxLimit;
        }

        if (value < 0)
        {
            return 0;
        }

        return value;
    }
}
