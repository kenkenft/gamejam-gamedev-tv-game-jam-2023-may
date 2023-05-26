using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZone : MonoBehaviour
{
    public SpriteRenderer EndZoneTriggerMarker;
    public bool ShowEndZoneMarker = false;

    [HideInInspector] public delegate void OnSomeEvent();
    [HideInInspector] public static OnSomeEvent EndZoneEntered;

    void Start()
    {
        EndZoneTriggerMarker.enabled = ShowEndZoneMarker;
    }

    public void TriggerEndLevel()
    {
        Debug.Log("End zone entered!");
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
}
