using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLvlUpUI : MonoBehaviour
{
    public Text lvlUpText;

    private void LateUpdate()
    {
        UpdateEnemyLvl();
    }
    public void UpdateEnemyLvl()
    {
        lvlUpText.text = EnemyManager.instance.Timer.LvlUp.ToString();
    }
}
