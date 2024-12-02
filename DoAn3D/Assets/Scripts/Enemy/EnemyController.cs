using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : IDamage
{
    private EnemyView _enemyView;
    private TakeDameEnemy _takeDame;
    private bool _isAttack;
    public void Initilize(EnemyView enemyView)
    {
      _enemyView = enemyView;
    }
    public void HandleEnemy(EnemyView enemyView)
    {
      
        if(Detected(enemyView))
        {
            enemyView.amin.SetBool(EnemyAmin.Attack.ToString(), true);
            enemyView.amin.SetBool(EnemyAmin.Run.ToString(), false);
            enemyView.amin.SetBool(EnemyAmin.Hit.ToString(), false);
            enemyView.CheckAttackRange();
           
        }  
        else
        {
            enemyView.Move();
        }
    }
    

    public bool Detected(EnemyView enemyView)
    {
        
        var distance = Vector3.Distance(EnemyManager.instance.playerPos.transform.position, enemyView._curEnemyPos.position);
        if (distance <= enemyView.enemyDataSO.stopToAttackDis)
        {
            return true;
        }
        return false;
    }
    #region
    public void getHit(float Damage)
    {
       
    }

    public float getHP()
    {
        throw new NotImplementedException();
    }
    #endregion
}
