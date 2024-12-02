using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDameEnemy : MonoBehaviour, IDamageEnemy
{
    public event Action<int> OnDamage;
    public event Func<int> GetHPFunc;

    public void getHit(int Damage)
    {
        OnDamage?.Invoke(Damage);
    }
    public float getHP()
    {
        return GetHPFunc.Invoke();
    }
}
