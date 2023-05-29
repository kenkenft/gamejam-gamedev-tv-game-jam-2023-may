using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZone : MonoBehaviour
{
    public SpriteRenderer EndZoneTriggerMarker;
    public bool ShowEndZoneMarker = false;

    public bool IsFinalLevel;
    public int NextLevel;

    [HideInInspector] public delegate void OnSomeEvent();
    [HideInInspector] public static OnSomeEvent EndZoneEntered;

    [HideInInspector] public delegate void SetValueEvent(int value);
    [HideInInspector] public static SetValueEvent LevelCompleted;
    [HideInInspector] public delegate void BoolValueSet(bool value);
    [HideInInspector] public static BoolValueSet IsFinalLevelCompleted;
    
    void OnEnable()
    {
        UIManager.IsFinalLevelRequested += GetIsFinalLevel;
    }

    void Disable()
    {
        UIManager.IsFinalLevelRequested -= GetIsFinalLevel;
    }
    void Start()
    {
        EndZoneTriggerMarker.enabled = ShowEndZoneMarker;
    }

    public void TriggerEndLevel()
    {
        // Debug.Log("End zone entered!");
        
        if(!IsFinalLevel)
            LevelCompleted?.Invoke(NextLevel);
        else
            LevelCompleted?.Invoke(0);
        IsFinalLevelCompleted?.Invoke(IsFinalLevel);
        EndZoneEntered?.Invoke();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch(col.tag)
        {
            case("Player"):
            {
                TriggerEndLevel();
                break;
            }

            default:
                break;
        }
    }

    public int GetNextLevel()
    {
        return NextLevel;
    }
    public bool GetIsFinalLevel()
    {
        return IsFinalLevel;
    }
}
