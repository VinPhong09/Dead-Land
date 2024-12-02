using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSkillDamage : MonoBehaviour
{
   private ParticleSystem ptc;
   private PlayerModel _pm;
   private PlayerView _pv;
   private PlayerController _pc;
   private void Start()
   {
      ptc = this.gameObject.GetComponent<ParticleSystem>();
      _pm = GameController._instantie.getPlayerModel;
      _pv = GameController._instantie.getPlayerView;
      _pc = GameController._instantie.getPlayerController;
   }

   public void Update()
   {
      if (ptc.isPlaying)
      {
         Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, 2f);

         foreach (Collider collider in colliders)
         {
            if (collider.gameObject.CompareTag("Enemy"))
            {
               var _enemy = collider.gameObject.GetComponent<IDamage>();
               if (_enemy != null)
               {
                  if (_enemy.getHP() <= 0)
                  {
                     _pc._currenExpBar += EnemyManager.instance.EnemyExp;
                     _pm._currentExp += EnemyManager.instance.EnemyExp;
                     _pm.CheckLevelup();
                     _pc.updateExpBarVariable();
                     _pv.UpdateExpBar(_pc._currenExpBar, _pc._expToUplevelBar,_pm._level);
                  }
                  else
                     _enemy.getHit(_pm._damage);
               }
            }
         }
      }
   }
}

