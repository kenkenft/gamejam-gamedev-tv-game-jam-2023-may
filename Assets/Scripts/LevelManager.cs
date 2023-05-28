using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public DimensionSwitch[] LevelObjects;
    [SerializeField] private int _currentLevel = 0;

    public Camera MainCamera;
    public float[] CameraOrthSizes;
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
        _currentLevel = nextLevel;
    }

    public void StartNextLevel()
    {
        // Pseudo code: 
        // Set up level layout
        // 
        Debug.Log("StartLevel called! Next Level: " + _currentLevel);
        ToggleActiveLevel();
        SetCameraSize(); 
        LevelObjects[_currentLevel].SetupLevel();
    }

    void ToggleActiveLevel()
    {
        for(int i = 0; i < LevelObjects.Length; i++)
        {
            if(i == _currentLevel)
                LevelObjects[i].gameObject.SetActive(true);
            else
                LevelObjects[i].gameObject.SetActive(false);
        }
    }

    public int GetCurrentLevel()
    {
        return _currentLevel;
    }

    void SetCameraSize()
    {
        if(_currentLevel < CameraOrthSizes.Length)
            MainCamera.orthographicSize = CameraOrthSizes[_currentLevel];
        else
            MainCamera.orthographicSize = 20f;
    }
}
