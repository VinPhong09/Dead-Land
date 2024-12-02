using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    void GetEnemyhealth(int takeDamage);
}
public interface IDamageEnemy
{
    public void getHit(int Damage);

    public float getHP();
}

