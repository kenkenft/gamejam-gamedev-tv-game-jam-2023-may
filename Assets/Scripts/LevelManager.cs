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
        EndZone.LevelCompleted += SetCurrentLevel;
    }

    void OnDisable()
    {
        PlayerMain.DimensionButtonPressed -= SwitchDimensions;
        EndZone.LevelCompleted -= SetCurrentLevel;
    }
    
    public void SwitchDimensions()
    {
        Debug.Log("SwitchDimensions called!");
        LevelObjects[_currentLevel].ChangeLayout();
    }

    public void SetCurrentLevel(int nextLevel)
    {
        _currentLevel = nextLevel;
    }
}
