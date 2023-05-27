using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionSwitch : MonoBehaviour
{
    public GameObject StartPos;
    
    public void ChangeLayout()
    {
        Debug.Log("ChangeLayout called! " + gameObject.name);
    }
}
