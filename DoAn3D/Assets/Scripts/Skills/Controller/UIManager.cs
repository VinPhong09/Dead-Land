using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header(header:"HP_Bar")]
    private HP_BarController _hpBarController;
    public GameObject _hpBarPrefabs;
    public GameObject _hpBarEnemyPrefabs;
    public Transform _hpBarParent;
    public HP_BarView _hpBarView;
    [Header(header:"Skill")]
    private SkillController _skillController;
    public GameObject _skillPrefab;
    public Transform _skillParent;
    public SkillView _skillView;
    public void InitHP_Bar(PlayerModel _playerModel,HP_BarView _hpBarView)
    {
        _hpBarController = new HP_BarController();
        setHPData(_playerModel);
        setHPView(_hpBarView);
    }

    
    public void setHPData(PlayerModel _playerModel)
    {
        _hpBarController.setData(_playerModel);
    }
    
    public void setHPView(HP_BarView _hpBarView)
    {
        if (_hpBarView != null)
        {
            this._hpBarView = _hpBarView;
            _hpBarController.setView(_hpBarView);    
        }
        else
        {
           var hpView = Instantiate(_hpBarPrefabs, _hpBarParent);
           this._hpBarView = hpView.GetComponentInChildren<HP_BarView>();
           _hpBarController.setView(this._hpBarView);
        }
    }

    public void setSkillData(PlayerModel _playerModel)
    {
        
    }

    public void setSkillView(SkillView _skillView)
    {
        
    }



}
