using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeathBar : MonoBehaviour
{
    public Image _healthBarImg;

    private Camera _cam;
    private int _curhealth;
    private int _maxHealth;
    private bool _isDead;

    private void Start()
    {
        _curhealth = EnemyManager.instance.Maxhealth;
        _cam = Camera.main;
    }
    private void Update()
    {
        _maxHealth = EnemyManager.instance.Maxhealth;
        if (EnemyManager.instance.Timer.EnemyLvlUp)
        {
            _curhealth = _maxHealth;
        }
        transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position);
    }
    public void HandEnemyhealth(int takeDamage,EnemyView enemy)
    {
        _curhealth -= takeDamage;
        if (_curhealth > 0)
        {
            
        }
        else if (_curhealth <= 0)
        {
            if(!_isDead)
            {
                enemy.amin.SetTrigger(EnemyAmin.Die.ToString());
                enemy.agent.speed = 0f;
                _isDead = true;
            }    
            
        }
    }
    public void UpdateHealthBar()
    {
       
        float slderPercenr = (float)_curhealth / _maxHealth;  
        _healthBarImg.fillAmount = slderPercenr;
    }
}
