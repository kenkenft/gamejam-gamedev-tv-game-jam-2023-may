using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCurrent : MonoBehaviour
{
    
    public float WaterForce;
    public enum FlowDirection
                                {
                                    up, down, left, right, still
                                };
    
    public FlowDirection RiverFlow;
    private Vector3[] _flowDirections = {
                                            Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.zero
                                        };

    public enum ForceModeType   {
                                    Force, Impulse
                                };
    public ForceModeType ModeSelection;

    private ForceMode2D[] _forceModes = {
                                            ForceMode2D.Force, ForceMode2D.Impulse, 
                                        };

    [HideInInspector] public delegate void OnPlaySFX(string audioName);
    [HideInInspector] public static OnPlaySFX PlaySFX;

    
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {  
            if(this.gameObject.tag != null)
            {   
                Debug.Log("Tag: " + this.gameObject.tag); 
                PlaySFX?.Invoke(this.gameObject.tag);}
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        // Debug.Log("Water entered! Water direction: " + RiverFlow + (int)RiverFlow);
        if(col.gameObject.tag == "Player")
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(_flowDirections[(int)RiverFlow] * WaterForce, _forceModes[(int)ModeSelection]);
            
    }

    void PlayCorrectSFX(string tag)
    {
        switch(tag)
        {
            case "Water":
            {
                PlaySFX?.Invoke("Splash");
                break;
            }

            case "Thorns":
            {
                PlaySFX?.Invoke("Splash");
                break;
            }
            default:
                break;
        }
    
    }
}
