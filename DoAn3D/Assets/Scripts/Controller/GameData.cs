using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData 
{
    public int _level;
    public float _health;
    public float _damage;
    public float _exp;
    public int _enemylevel;
    public GameData(PlayerModel _pm, int Elevel)
    {
        if (_pm != null)
        {
            _level = _pm._level;
            _health = _pm._currentHealth;
            _damage = _pm._damage;
            _exp = _pm._currentExp;
            _enemylevel = Elevel;
        }
    }
}
