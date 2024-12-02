using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent: MonoBehaviour
{
   public PlayerView _playerView;

   private void Start()
   {
      _playerView = gameObject.GetComponentInParent<PlayerView>();
   }
   
   public void onAttackTarget(){_playerView.Attack();}
   public void onComboAttackAnimation() { _playerView.onComboAttackAnimation(); }
   public void onCompleComboAnimation() { _playerView.onCompleComboAnimation(); }
   public void onCompleteAnimation() { _playerView.onCompleteanimation(); }
}
