using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public Transform playerPos;
    public Transform centerPos;
    public float offSet;
    public GameObject enemyPrefab;
    public int _enemyQuantity;
    public EnemyDataSO enemyDataSO;
   
   
    private int _curEnemyHealth ;
    private EnemyView _enemyView;
    public EnemyController _enemyController;
    private float m_transformXR;
    private float m_transformXL;
    private float m_transformZR;
    private float m_transformZL;
    private bool m_enemyEnough;
    private int index ;
    private Vector3 m_movingPos;
    private EnemyLevel enemyDataSo;
    private Timer timer;
    private int curEnemyLevel;
    private float _enemySpeed = 3.5f;
    private float _enemyAttack = 5f;
    private float _enemyExp = 2;
    private int _maxhealth = 3;
    private Transform _parentTransform;
    private GameObject _enemyObj;
    private int _enemySpawnAmount = 10;
    private float _timeToSpawn;
  

    public EnemyLevel EnemyDataSo { get => enemyDataSo; }
    public EnemyView EnemyView { get => _enemyView;  }
    public int CurEnemyHealth { get => _curEnemyHealth; set => _curEnemyHealth = value; }
    public Timer Timer { get => timer;  }
    public float EnemyAttack { get => _enemyAttack;  }
    public float EnemyExp { get => _enemyExp;  }
    public int Maxhealth { get => _maxhealth;  }
    public Vector3 MovingPos { get => m_movingPos;  }
    public float EnemySpeed { get => _enemySpeed;  }
    public int Index { get => index; set => index = value; }
    public bool EnemyEnough { get => m_enemyEnough; set => m_enemyEnough = value; }
    public int EnemySpawnAmount { get => _enemySpawnAmount;  }
    public GameObject EnemyObj { get => _enemyObj;  }

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        AudioManager.ins.PlayBackgroundMusic();
        playerPos = GameObject.FindWithTag("PlayerPosition").GetComponent<Transform>();
        centerPos = playerPos;
        _curEnemyHealth = _maxhealth;
        _enemyController = new EnemyController();
        m_transformXR = centerPos.position.x + offSet;
        m_transformXL = centerPos.position.x - offSet;
        m_transformZR = centerPos.position.z + offSet;
        m_transformZL = centerPos.position.z - offSet;
        timer = FindFirstObjectByType<Timer>();
        StartCoroutine(CreatEnemy());
        enemyDataSo = enemyDataSO.enemyLevels[1];
        SetParentTransform();
        _timeToSpawn = 3;

    }
    public void Update()
    {
        
        EnemyView[] enemies = FindObjectsOfType<EnemyView>();
        foreach (EnemyView enemy in enemies)
        {
            _enemyView = enemy;
            _enemyView.Initilize();
            _enemyController.Initilize(_enemyView);
            _enemyController.HandleEnemy(_enemyView);
            if (timer.EnemyLvlUp)
            {
                index = 0;
                m_enemyEnough = false;
            }    
        }
        SetEnemyTransform();
        UpdateEnemyLevel();
       
    }
    public IEnumerator CreatEnemy()
    {
        while (!m_enemyEnough)
        {
            yield return new WaitForSeconds(_timeToSpawn);
            m_enemyEnough = false;
            _enemyObj = Instantiate(enemyPrefab, new Vector3(m_movingPos.x, m_movingPos.y, m_movingPos.z), enemyPrefab.transform.rotation);
            _enemyObj.AddComponent<TakeDamage>();
            _enemyObj.GetComponent<EnemyView>().initTakeDamage();
            if (_enemyObj.GetComponent<EnemyView>().agent == null)
            {
                Destroy(_enemyObj.gameObject);
            } 
            _enemyObj.transform.SetParent(_parentTransform);
            index++;
            if (index >= _enemySpawnAmount)
            {
                m_enemyEnough = true;
            }
        }
    }
    
   
    private void SetParentTransform()
    {
        var parentName = GameObject.Find("EnemyPooling");
        if(parentName)
        {
            _parentTransform = parentName.transform;
        }   
        
    }
   
    public void UpdateEnemyLevel()
    {
        enemyDataSo = enemyDataSO.enemyLevels[timer.LvlUp - 1];
        if(timer.EnemyLvlUp)
        {
            _timeToSpawn -= 0.2f;
            if (_timeToSpawn <= 1)
                _timeToSpawn = 1;
            
            _maxhealth = enemyDataSo.hp;
            _curEnemyHealth = _maxhealth;
            _enemySpeed = enemyDataSo.moveSpeed;
            _enemyAttack = enemyDataSo.attack;
            _enemyExp = enemyDataSo.exp;
            _enemySpawnAmount = enemyDataSo.enemySpawnAmount;
            StartCoroutine(CreatEnemy());
        }
    }
    public void SetEnemyTransform()
    {
        float posX = Random.Range(m_transformXR, m_transformXL);
        float posZ = Random.Range(m_transformZR, m_transformZL);
        var newVector = new Vector3(posX, centerPos.position.y -3f, posZ);
        m_movingPos = newVector;
    }

    // public void setEnemyLevel(int Level)
    // {
    //     Enemy
    // }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(centerPos.position, offSet);

    }

}
