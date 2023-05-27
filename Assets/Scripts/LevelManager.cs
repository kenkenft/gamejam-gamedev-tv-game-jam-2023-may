using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public DimensionSwitch[] LevelObjects;
    [SerializeField] private int _currentLevel = 0, _previousLevel = 0;

    [HideInInspector] public delegate void OnSomeEvent();
    [HideInInspector] public static OnSomeEvent TitleSwitchOccurred;

    void OnEnable()
    {
        PlayerMain.DimensionButtonPressed += SwitchDimensions;
        EndZone.LevelCompleted += SetCurrentLevel;
        UIManager.NextLevelRequested += StartNextLevel;
        UIManager.IntValueRequested += GetCurrentLevel;
    }

    void OnDisable()
    {
        PlayerMain.DimensionButtonPressed -= SwitchDimensions;
        EndZone.LevelCompleted -= SetCurrentLevel;
        UIManager.NextLevelRequested -= StartNextLevel;
        UIManager.IntValueRequested -= GetCurrentLevel;
    }
    
    public void SwitchDimensions()
    {
        Debug.Log("SwitchDimensions called!");

        if(_currentLevel == 0)
            TitleSwitchOccurred?.Invoke();

        LevelObjects[_currentLevel].ChangeLayout();
    }

    public void SetCurrentLevel(int nextLevel)
    {
        _previousLevel = _currentLevel;
        _currentLevel = nextLevel;
    }

    public void StartNextLevel()
    {
        // Pseudo code: 
        // Set up level layout
        // 
        Debug.Log("StartLevel called! Next Level: " + _currentLevel);
        LevelObjects[_previousLevel].gameObject.SetActive(false);
        LevelObjects[_currentLevel].gameObject.SetActive(true);
        LevelObjects[_currentLevel].SetupLevel();
    }

    public int GetCurrentLevel()
    {
        return _currentLevel;
    }
}
