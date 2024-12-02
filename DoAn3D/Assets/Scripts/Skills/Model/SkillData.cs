using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillState
{
    ready,
    done
}

public enum TypeSkill
{
    circle,
    straight,
}
[CreateAssetMenu(menuName = "SkillData")]
public class SkillData : ScriptableObject,IBaseSkill
{
    public SkillData(int skillLevel, int skillCost, float skillCooldown, float skillDamage, bool isLocked, SkillState skillState, TypeSkill skillType)
    {
        this.skillLevel = skillLevel;
        this.skillCost = skillCost;
        this.skillCooldown = skillCooldown;
        this.skillDamage = skillDamage;
        this.isLocked = isLocked;
        this.skillState = skillState;
        this.skillType = skillType;
    }

    public string skillName;
    public string skillDescription;
    public Sprite skillIcon;
    public int skillLevel;
    public int skillCost;
    public float skillCooldown;
    public float skillDamage;
    public bool isLocked;
    public EffectSkill skillEffect;
    public SkillState skillState = SkillState.ready;
    public TypeSkill skillType = TypeSkill.circle;
    public float timeSkill;
    public float getDame()
    {
        return skillDamage;
    }
    public float getCooldown()
    {
        return skillCooldown;
    }

    public TypeSkill getSkill()
    {
        return skillType;
    }
    public void unlockSkill(PlayerModel playerModel)
    {
        if (playerModel._level >= skillLevel)
        {
            Debug.Log("unlockingSkill");
            isLocked = false;
        }
        else
        {
            Debug.Log("not unlockingSkill");
            isLocked = true;
        }
    }

    public void useSkill(SkillData skillData, Transform skillPosition, Transform skillParent)
    {
        var skillClone = Instantiate(skillData.skillEffect.effectPrefabs, skillPosition.position, Quaternion.identity,skillParent);
        Destroy(skillClone,skillData.skillCooldown);
    }
    

    public void destroySkill()
    {
        throw new System.NotImplementedException();
    }
}
