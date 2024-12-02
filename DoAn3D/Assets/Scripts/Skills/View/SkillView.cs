using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class SkillView : MonoBehaviour,IBaseSkill
{
    private GameController _gameController;
    public Indicator _indicator;
    public GameObject lockedPanel;
    public SkillData data;
    public Image iconSkill;
    public float skillCooldown;
    public Image skillCooldownFill;
    public bool isUse;
    private PlayerModel _playerModel;

    

    private void Update()
    {
        renewSkill();
        if (data.isLocked)
            CheckUnlockSkill();
    }

    
    private void Start()
    {
        _gameController = FindFirstObjectByType<GameController>();
        
    }

    public void initSkill(PlayerModel playerModel)
    {
        iconSkill.sprite = data.skillIcon;
        skillCooldown = data.skillCooldown;
        skillCooldownFill.fillAmount = 0f;
        _playerModel = playerModel;
        
    }
    public void renewSkill()
    {
        if (skillCooldownFill.fillAmount <= 0)
        {
            GetComponent<Button>().interactable = true;
        }
        else
        {
            GetComponent<Button>().interactable = false;
            skillCooldownFill.fillAmount -= 1/data.skillCooldown * Time.deltaTime;
        }
    }
    public void CheckUnlockSkill()
    {
        if (_playerModel._level < data.skillLevel)
        {
            data.isLocked = true;
            lockedPanel.SetActive(true);
        }
        else
        {
            data.isLocked = false;
            lockedPanel.SetActive(false);
        }
            
    }

    public IEnumerator delaySkill(float delay)
    {
        isUse = false;
        data.skillState = SkillState.done;
        skillCooldownFill.fillAmount = 1;
        yield return new WaitForSeconds(delay);
        data.skillState = SkillState.ready;
        isUse = true;
    }
    public float getDame()
    {
        throw new NotImplementedException();
    }

    public void unlockSkill(PlayerModel playerModel)
    {
        throw new NotImplementedException();
    }

    public void useSkill(SkillData skillData, Transform skillPosition, Transform skillParent)
    {
        StartCoroutine(delaySkill(skillData.skillCooldown));
        var skill = Instantiate(skillData.skillEffect.effectPrefabs, skillPosition.position, Quaternion.identity,
            skillParent);
        Destroy(skill,skillData.timeSkill);
    }
    public void castSkill(SkillData skillData, Transform skillPosition, Transform skillParent)
    {
        if (skillData.skillEffect.effectCastPref != null)
        {
            var castSkill = Instantiate(skillData.skillEffect.effectCastPref, skillPosition.position, Quaternion.identity,
                        skillParent);
            Destroy(castSkill,skillData.timeSkill);
        }
        
    }
    public void SpellOn()
    {
        GameController.spellon = true;
        _indicator = _gameController._indicator;
        _gameController._indicator.skillView = this;
    }
    public void SpellOff()
    {
        GameController.spelloff = false;
        StartCoroutine(spellOff());
    }

    IEnumerator spellOff()
    {
        yield return new WaitForSeconds(0.5f);
        _gameController._indicator.skillView = null;
    }
    public void destroySkill()
    {
        throw new NotImplementedException();
    }
}
