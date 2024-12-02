using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MEC;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Networking;


public class PlayerModel : IData
{
    private PlayerDataSO _playerDataSO;
    private GameData _gameData;
    public void Initialize()
    {
        if (_playerDataSO == null)
            _playerDataSO = Resources.Load<PlayerDataSO>("PlayerData");
        if (_GunCharacterprefab == null)
            _GunCharacterprefab = Resources.Load<GameObject>("MainCharacterPrefabs/GunCharacter");
        if (_SwordCharacterPrefab == null)
            _SwordCharacterPrefab = Resources.Load<GameObject>("MainCharacterPrefabs/SwordCharacter");
        InitData();
        if (_playerLevelUpDataDesign == null)
        {
            _playerLevelUpDataDesign = new PlayerLevelUpDataDesign();
             GameController._instantie.StartCoroutine(_playerLevelUpDataDesign.Initialize());
        }
    }
    public PlayerModel(){}

    public PlayerModel(GameData GD)
    {
        _gameData = GD;
    }
    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public void InitData()
    {
        switch (_gameData)
        {
            case null:
                _moveSpeed = _playerDataSO._moveSpeed;
                _jumpForce = _playerDataSO._jumpForce;
                _damage = _playerDataSO._damage;
                _health = _playerDataSO._health;
                _dpsMeleeAttack = _playerDataSO._dpsMeleeAttack;
                _dpsBulletAttack = _playerDataSO._dpsBulletAttack;
                _bulletSpeed = _playerDataSO._bulletSpeed;
                _detectionRadius = _playerDataSO._detectionRadius;
                _currentHealth = _health;
                _level = _playerDataSO._level;
                skillData = _playerDataSO._abilities;
                _currentExp = 0;
                break;
            case not null:
                _level = _gameData._level;
                _health = _gameData._health;
                _currentHealth = _health;
                _damage = _gameData._damage;
                _currentExp = _gameData._exp;
                skillData = _playerDataSO._abilities;
                _moveSpeed = 5;
                _detectionRadius = 4;
                break;
                
        }
        
    }
    
    public PlayerLevelUpDataDesign _playerLevelUpDataDesign { get; set; }
    public GameObject _GunCharacterprefab { get; set; }
    public GameObject _SwordCharacterPrefab { get; set; }
    public float _bulletSpeed { get; set; }
    public float _dpsMeleeAttack { get; set; }
    public float _dpsBulletAttack { get; set; }
    public float _moveSpeed { get; set; }
    public float _jumpForce { get; set; }
    public float _damage { get; set; }
    public float _health { get; set; }
    public float _currentHealth { get; set; }
    public float _detectionRadius { get; set; }
    public float _currentExp { get; set; }
    public List<SkillData> skillData { get; set; }
    public int _level { get; set; }

    public bool _isLevelUp { get; set; }
    public int _expToUpLV()
    {
        if (_playerLevelUpDataDesign.GetDictionary != null)
        {
            foreach (var kvp in _playerLevelUpDataDesign.GetDictionary)
            {
                if (kvp.Key.Contains(_level.ToString()))
                {
                    PlayerLevelUpDataDesign.PlayerDataLv data = kvp.Value;
                    return data.Exp;
                }
            }
        }
        return 0;
    }
    
    public void CheckLevelup()
    {
        if (_currentExp >= _expToUpLV())
        {
            _isLevelUp = true;
            _damage += _damage;
        }
    }
}



    

