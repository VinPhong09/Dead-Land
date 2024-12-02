using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour, IDamage
{
    public event Action<float> OnDamage;
    public event Func<float> GetHPFunc;
    
    public void getHit(float Damage)    
    {
        OnDamage?.Invoke(Damage);
    }
    public float getHP()
    {
        return GetHPFunc.Invoke();
    }
    
}


