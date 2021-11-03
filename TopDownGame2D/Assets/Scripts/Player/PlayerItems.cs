using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    [SerializeField] private int LIMIT_ITEM = 50;

    // Wood
    [SerializeField] private int _totalWood;
    public int TotalWood
    {
        get => _totalWood;
        set => _totalWood = CheckLimitItem(value);
    }

    // Water
    [SerializeField] private float _totalWater;
    public float TotalWater
    {
        get => _totalWater;
        set => _totalWater = CheckLimitItem(value);
    }

    // Carrot
    [SerializeField] private int _totalCarrot;
    public int TotalCarrot
    {
        get => _totalCarrot;
        set => _totalCarrot = CheckLimitItem(value);
    }

    private int CheckLimitItem(int value)
    { 
        if (value > LIMIT_ITEM)
        {
            return LIMIT_ITEM;
        }

        if(value < 0)
        {
            return 0;
        }

        return value;
    }

    private float CheckLimitItem(float value)
    {
        if (value > LIMIT_ITEM)
        {
            return LIMIT_ITEM;
        }

        if (value < 0)
        {
            return 0;
        }

        return value;
    }
}
