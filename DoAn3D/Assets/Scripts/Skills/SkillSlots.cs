using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlots : MonoBehaviour
{
    public SkillData data;
    public Image iconSkill;
    public float skillCooldown;
    public GameObject lockedPanel;
    private void Start()
    {
        initSkill();
    }

    public void initSkill()
    {
        iconSkill.sprite = data.skillIcon;
        skillCooldown = data.skillCooldown;
        CheckUnlockSkill();
    }

    public void CheckUnlockSkill()
    {
        if (data.isLocked)
            lockedPanel.SetActive(true);
        else
            lockedPanel.SetActive(false);
    }
    
}
