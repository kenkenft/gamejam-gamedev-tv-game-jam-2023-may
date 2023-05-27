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
    void Start()
    {
        EndZoneTriggerMarker.enabled = ShowEndZoneMarker;
    }

    public void TriggerEndLevel()
    {
        Debug.Log("End zone entered!");
        EndZoneEntered?.Invoke();
        if(!IsFinalLevel)
            LevelCompleted?.Invoke(NextLevel);
        else
            LevelCompleted?.Invoke(0);
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
}
