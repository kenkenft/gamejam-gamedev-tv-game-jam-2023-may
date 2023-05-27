using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public DimensionSwitch[] LevelObjects;
    [SerializeField] private int _currentLevel = 0;

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
        LevelObjects[_currentLevel].ChangeLayout();
    }
}
