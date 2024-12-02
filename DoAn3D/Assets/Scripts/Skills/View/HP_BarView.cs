using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_BarView : MonoBehaviour,IHP_Bar
{
    public event Action<float> fillHP;
    public Image perHP; 
    

    public void ChangeHPBar(float fillAmount)
    {
        fillHP?.Invoke(fillAmount);
    }
    
}
