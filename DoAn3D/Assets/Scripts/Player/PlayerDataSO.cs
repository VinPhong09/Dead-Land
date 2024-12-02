using System.Collections;
using System.Collections.Generic;using UnityEditor;
using UnityEngine;

[System.Serializable]
public enum PlayerAttackType
{
    Melee,
    ComboAttack,
    Shoot
}
public enum AnimationString
{
    Walk,
    Run,
    Jump,
    Idle,
    Attack1,
    Attack2,
    Attack3,
    Attack4,
}
[CreateAssetMenu(menuName = "Data/PlayerData")]
public class PlayerDataSO : ScriptableObject
{
    public int _level;
    public float _health;
    public float _damage;
    public float _moveSpeed;
    public float _jumpForce;
    public float _dpsMeleeAttack;
    public float _dpsBulletAttack;
    public float _bulletSpeed;
    public float _detectionRadius;
    public float _exp;
    public List<SkillData> _abilities;

    public PlayerDataSO(GameData _data)
    {
        _level = _data._level;
        _health = _data._health;
        _exp = _data._exp;
        _damage = _data._damage;
    }
}
