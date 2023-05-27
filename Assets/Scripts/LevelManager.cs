using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public DimensionSwitch[] LevelObjects;

    void OnEnable()
    {
        PlayerMain.DimensionButtonPressed += SwitchDimensions;
    }

    void OnDisable()
    {
        PlayerMain.DimensionButtonPressed -= SwitchDimensions;
    }
    
    public void SwitchDimensions()
    {
        Debug.Log("SwitchDimensions called!");
    }
}
