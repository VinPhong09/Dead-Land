using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HPBar_LookAt : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);   
    }
}
