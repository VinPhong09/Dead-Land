using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Data 
{
    public float CurHp
    {
        get => curHP;
        set => curHP = value;
    }

    public float MaxHp
    {
        get => maxHP;
        set => maxHP = value;
    }

    public void setData(float curHp, float maxHp)
    {
        curHP = curHp;
        maxHP = maxHp;
    }

    private float curHP;
    private float maxHP;
    
}
