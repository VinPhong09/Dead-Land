using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_BarController : IHP_Bar
{
    [SerializeField]private HP_Data _hpData;

    [SerializeField]private HP_BarView _hpBarView;

    

    public void Init()
    {
        
    }

    public void setData(PlayerModel _playerModel)
    {
        _hpData = new HP_Data();
        if (_playerModel != null)
        {
            _hpData.setData(_playerModel._currentHealth,_playerModel._health);
            //_hpBarView.ChangeHPBar(1);
        }
    }

    public void setView(HP_BarView _hpBarView)
    {
        this._hpBarView = _hpBarView;
        if (this._hpBarView != null)
        {
            listenAction();
        }
    }

    public void listenAction()
    {
        
        _hpBarView.fillHP += ChangeHPBar;
    }
    
    public void ChangeHPBar(float fillAmount)
    {

        _hpBarView.perHP.fillAmount = fillAmount;
        if (fillAmount >= 0.7)
        {
            _hpBarView.perHP.color = Color.green;
        }else if (fillAmount>= 0.3 && fillAmount<0.7)
        {
            _hpBarView.perHP.color = new Color(1,(float)0.6,0,1);
        }
        else
        {
            _hpBarView.perHP.color = Color.red;
        }
    }
}
