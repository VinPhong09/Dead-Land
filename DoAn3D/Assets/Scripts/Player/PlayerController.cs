using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UIElements;


public class PlayerController :  IAttack,Icharacter
{
    private PlayerModel _playerModel;
    private PlayerView _playerView;
    private InputManage _inputManage;
    private TakeDamage _takeDamage;
    private List<IDamage> _enemiesTakeDamages;
    private PlayerInputControls _playerInput;
    
    private float _dpsMeleeAttack;
    private float _dpsBulletAttack;
    private float clickInterval = .5f;
    private float lastClickTime;
    public float _currenExpBar { get; set; }
    public float _expToUplevelBar { get; set; }
    
    private bool _isBoostSpeed;
    private bool _canComboAttack;
    private bool _canMove;

    public void Create() { /*Create PlayerController.*/ }
    public void initialize()
    {
        _enemiesTakeDamages = new List<IDamage>();
        if (_playerInput == null)
            _playerInput = new PlayerInputControls();
        
        OnEnable();
    }
    private void InitAction()
    {
        _takeDamage.OnDamage += getHit;
        _takeDamage.GetHPFunc += getHP;
        _playerView.onAttackTarget += atkTarget;
    }

    public void onUpdate()
    {
        detectTarget();
        HandleInput();
    }
    
    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    public void HandleInput()
    {
        if (_playerView._animatorPlayer != null)
        {
            Vector2 MovementInput = _playerInput.Player.Move.ReadValue<Vector2>();
            if (MovementInput.x != 0 || MovementInput.y != 0 && _canMove && _playerInput.Player.BoostSpeed.IsPressed())
            {
                _playerView.Move(MovementInput, _playerModel._moveSpeed * 1.5f,true);
            }else if (MovementInput.x != 0 || MovementInput.y != 0 && _canMove )
            {
                _playerView.Move(MovementInput, _playerModel._moveSpeed ,false);
            }
            else
            {
                _playerView.Move(new Vector2(0, 0), _playerModel._moveSpeed, false);
            }
        
            if (_playerInput.Player.AttackTap.IsPressed())
            {
                if (CheckMulticlick())
                {
                    _canMove = false;
                    _playerView.onAttackAnimation(PlayerAttackType.ComboAttack);
                }
            }
            

            if (_playerInput.Player.ChangeCharacter.triggered)
            {
                _playerView.ChangeAttackType();
            }
           
        }
    }

    public Vector2 getCameraInputFromPlayer() => _playerInput.Player.Look.ReadValue<Vector2>();
    #region Function

        public void CheckUnlockSkill()
        {
            for (int i = 0; i < _playerModel.skillData.Count; i++)
            {
                _playerModel.skillData[i].unlockSkill(_playerModel);
            }
        }
        public bool CheckMulticlick()
        {
            float currentTime = Time.time;
            if (currentTime - lastClickTime <= clickInterval)
            {
                lastClickTime = currentTime;
                return true;
            }
            lastClickTime = currentTime;
            return false;
        }

        public void updateExpBarVariable()
        {
            if (_playerModel._isLevelUp)
            {
                _currenExpBar = _playerModel._expToUpLV() - _currenExpBar;
                _playerModel._level++;
                _expToUplevelBar = _playerModel._expToUpLV() - _currenExpBar;
                _playerModel._isLevelUp = false;
            }
        }
        public void detectTarget()
        {
            Collider[] colliders = Physics.OverlapSphere(_playerView._playerTransform.position, _playerModel._detectionRadius);
    
            foreach (Collider collider in colliders)
            {
               
                if (collider.gameObject.CompareTag("Enemy"))
                {
                    var _enemy = collider.gameObject.GetComponent<IDamage>();
                    if (_enemy != null)
                    {
                        if(!_enemiesTakeDamages.Contains(_enemy))
                            _enemiesTakeDamages.Add(_enemy);
                    }
                }
            }
        }

        public void atkTarget()
        {
            if (_playerView._swordCharacter.activeSelf)
            {
                AudioManager.ins.SetSound(AudioManager.ins.SwordSound);
                Collider[] colliders = Physics.OverlapSphere(_playerView._playerTransform.position,
                    _playerModel._detectionRadius);

                foreach (Collider collider in colliders)
                {
                    if (collider.gameObject.CompareTag("Enemy"))
                    {
                        var _enemy = collider.gameObject.GetComponent<IDamage>();
                        if (_enemy != null)
                        {
                            if (_enemy.getHP() <= 0)
                            {
                                _currenExpBar += EnemyManager.instance.EnemyExp;
                                _playerModel._currentExp += EnemyManager.instance.EnemyExp;
                                _playerModel.CheckLevelup();
                                updateExpBarVariable();
                                _playerView.UpdateExpBar(_currenExpBar,_expToUplevelBar,_playerModel._level);
                                
                            }
                            else
                            {
                                _enemy.getHit(_playerModel._damage);
                            }

                        }
                    }
                }
            }
            else if (_playerView._gunCharacter.activeSelf)
            {
                AudioManager.ins.SetSound(AudioManager.ins.ShootSound);
                Collider[] colliders = Physics.OverlapSphere(_playerView._playerTransform.position,
                    _playerModel._detectionRadius * 2);
                // Attack ClosetEnemy in Range
                IDamage EnemyCloset;
                EnemyView enemyClosetTransform;
                float nearestDistanceSqr = Mathf.Infinity;
                foreach (Collider collider in colliders)
                {
                    if (collider.gameObject.CompareTag("Enemy"))
                    {
                        Vector3 directionToEnemy = collider.transform.position - _playerView._playerTransform.position;
                        float distanceSqr = directionToEnemy.sqrMagnitude;
            
                        if (distanceSqr < nearestDistanceSqr)
                        {
                            nearestDistanceSqr = distanceSqr;
                            EnemyCloset = collider.gameObject.GetComponent<IDamage>();
                            if (EnemyCloset != null)
                            {
                                if (EnemyCloset.getHP() <= 0)
                                {
                                    _currenExpBar += EnemyManager.instance.EnemyExp;
                                    _playerModel._currentExp += EnemyManager.instance.EnemyExp;
                                    _playerModel.CheckLevelup();
                                    updateExpBarVariable();
                                    _playerView.UpdateExpBar(_currenExpBar, _expToUplevelBar, _playerModel._level);
                                }
                                else
                                {
                                    EnemyCloset.getHit(_playerModel._damage);
                                }
                            }
                            
                            enemyClosetTransform = collider.gameObject.GetComponent<EnemyView>();
                            if(enemyClosetTransform !=null)
                                _playerView._playeModelview.LookAt(enemyClosetTransform.GetComponent<Transform>());
                            
                        }
                    }
                }
                
                

            }
        }

        #endregion
        
    #region Get, Set Function   
        //set
        public void setData(IData _iData)
        {
            _playerModel = (PlayerModel)_iData;
            _currenExpBar = _playerModel._currentExp;
            _expToUplevelBar = _playerModel._expToUpLV();
        }

        public void setView(PlayerView _playerView)
        {
            this._playerView = _playerView;
            if (_playerView != null)
            {
                _takeDamage = _playerView.AddComponent<TakeDamage>();
                InitAction();
            }
            _playerView.initializeModel(_playerModel._SwordCharacterPrefab, _playerModel._GunCharacterprefab);
            _playerView.UpdateHealthBar(_playerModel._currentHealth, _playerModel._health);
            _playerView.UpdateExpBar(_playerModel._currentExp,50,_playerModel._level);
        }
        
        //get
        private void getHit(float value)
        {
            _playerModel._currentHealth -= value;
            _playerView.UpdateHealthBar(_playerModel._currentHealth, _playerModel._health);
        }
        private float getHP()
        {
            return _playerModel._currentHealth;
        }
        public float getStastus()
        {
            return _playerModel._currentHealth;
        }
    #endregion
    
}



