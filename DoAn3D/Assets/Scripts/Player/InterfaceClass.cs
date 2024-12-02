using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IData
{
}
public interface IAttack
{
    public void setData(IData _data);
    public void detectTarget();
    public void atkTarget();
}
public interface Icharacter
{
    public void setData(IData _data);
    public void Create();
    public float getStastus();
}
public interface IDamage
{
    public void getHit(float Damage);

    public float getHP();
}

public interface IController
{
    public void Initialize();
    public void Move();
    public void Jump();
    public void Attack();

}
public interface IScore
{
    void UpdateScore(int score);
}
public interface IHP_Bar
{
    void ChangeHPBar(float fillAmount);
}