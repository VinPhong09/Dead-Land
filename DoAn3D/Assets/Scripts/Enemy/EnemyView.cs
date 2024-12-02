using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyView : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator amin;
    public EnemyDataSO enemyDataSO;
    public Transform _curEnemyPos;
    public HeathBar healhBar;
    public TakeDamage _takeDamage;
    public Transform attackPos;
    public float range;
    public LayerMask playerLayer;
    public DefenseMode defenseMode;

    public bool _isDie { get; set; }
    
    private bool _canSpawn;
    private bool _isAttack;
    private float _defenseIndex = 0.4f;
    private bool _isHealed;
    private float _curHealh;

    public float CurHealh { get => _curHealh;  }

    public void Initilize()
    {
        agent = GetComponent<NavMeshAgent>();
        _curHealh = EnemyManager.instance.CurEnemyHealth;
        if (agent == null)
        {
            Destroy(gameObject); 
            return;
        }
    }
    
    public void Move()
    {
       
        if (agent!= null && EnemyManager.instance.playerPos.transform.position != null)
        {
          agent.SetDestination(EnemyManager.instance.playerPos.transform.position);
        }    
        else if(agent == null || !agent.pathPending && agent.path.status == NavMeshPathStatus.PathInvalid)
        {
            Destroy(gameObject);
        }    
        agent.speed = EnemyManager.instance.EnemySpeed;
        amin.SetBool(EnemyAmin.Attack.ToString(), false);
        amin.SetBool(EnemyAmin.Run.ToString(), true);
        amin.SetBool(EnemyAmin.Hit.ToString(), false);
    }
    public void CheckAttackRange()
    {
        Collider[] hit = Physics.OverlapSphere(attackPos.position, range, playerLayer);

        foreach (var attack in hit)
        {
            if (attack != null)
            {
                if(!_isAttack)
                {
                   
                    IDamage damage = attack.GetComponent<IDamage>();
                    damage?.getHit(EnemyManager.instance.EnemyAttack);
                    _isAttack = true;
                }    
               
            }
            else if(attack == null)
            {
                _isAttack = false;
            }    
        }
    }

    public void initTakeDamage()
    {
        _isDie = false;
        _takeDamage = gameObject.GetComponent<TakeDamage>();
        if(_takeDamage != null)
            InitAction();
    }
    private void InitAction()
    {
        _takeDamage.OnDamage += getHit;
        _takeDamage.GetHPFunc += getHP;
    }
    private void getHit(float value)
    {
        
        healhBar.HandEnemyhealth((int)value, this.gameObject.GetComponent<EnemyView>());
        healhBar.UpdateHealthBar();
        if (healhBar._healthBarImg.fillAmount <= 0)
        {
            _isDie = true;
            if (!_canSpawn)
            {
                EnemyManager.instance.Index--;
                EnemyManager.instance.EnemyEnough = false;
                StartCoroutine(EnemyManager.instance.CreatEnemy());
                _canSpawn = true;
            }
            
            Destroy(gameObject, 1.5f);
            
        }
        else if(healhBar._healthBarImg.fillAmount > 0)
        {
            DamePopupGenerator._instance.createPopUp(this.gameObject.transform,value,Color.red);
            _canSpawn = false;
            if(EnemyManager.instance.CurEnemyHealth <= 0.5f && !_isHealed)
            {
                EnemyManager.instance.CurEnemyHealth += (int)_defenseIndex;
                StartCoroutine(defenseMode.EnemyDefenseloop());
                _isHealed = true;
            }    
        }    
       
    }
    private float getHP()
    {
        if(_isDie)
            gameObject.GetComponent<Collider>().enabled = false;
        return healhBar._healthBarImg.fillAmount;
    }
    public void HitToIdle()
    {
        amin.SetBool(EnemyAmin.Hit.ToString(), false);
    }
    private void OnDestroy()
    {
       
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPos.position, range);

    }


}
