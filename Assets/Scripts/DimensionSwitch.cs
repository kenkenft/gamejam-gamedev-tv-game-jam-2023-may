using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionSwitch : MonoBehaviour
{
    public GameObject StartPos;
    public GameObject[] GroundTileMaps; // Assumes that index 0 is enabled, and index 1 is disabled at level start
    private int _activeMapIndex;
    public bool ShowMarker = false;

    [HideInInspector] public delegate void SetPositionEvent(Vector3 position);
    [HideInInspector] public static SetPositionEvent LevelStarted; 
    [HideInInspector] public delegate void SwitchBGM(int index);
    [HideInInspector] public static SwitchBGM BGMChangeRequested;

    [HideInInspector] public delegate void OnPlaySFX(string audioName);
    [HideInInspector] public static OnPlaySFX PlaySFX;

    
    public void ChangeLayout()
    {
        // Debug.Log("ChangeLayout called! " + gameObject.name);
        ToggleTileMaps();
        PlaySFX?.Invoke("Switch");
        BGMChangeRequested?.Invoke(_activeMapIndex);
    }

    void ToggleTileMaps()
    {
        // This enables the disabled tilemap first, and then disables the enabled tilemap.
        // This needs to be done in this specific order to prevent the player from "sinking" 
        // into the terrain
        for(int i = 0; i < GroundTileMaps.Length; i++)
        {
            if(i != _activeMapIndex)
                GroundTileMaps[i].SetActive(true);
        }

        for(int i = 0; i < GroundTileMaps.Length; i++)
        {
            if(i == _activeMapIndex)
                GroundTileMaps[i].SetActive(false);
        }

        if(_activeMapIndex == 0)
            _activeMapIndex = 1;
        else
            _activeMapIndex = 0;
    }

    public void SetupLevel()
    {
        _activeMapIndex = 0;
        GroundTileMaps[0].SetActive(true);
        GroundTileMaps[1].SetActive(false);
        StartPos.GetComponent<SpriteRenderer>().enabled = ShowMarker;
        LevelStarted?.Invoke(StartPos.transform.position);
        BGMChangeRequested?.Invoke(_activeMapIndex);
    } 
}
