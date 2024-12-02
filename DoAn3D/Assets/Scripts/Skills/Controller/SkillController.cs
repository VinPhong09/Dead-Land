using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class SkillController : MonoBehaviour,IBaseSkill
{
    private List<SkillData> _skillData;
    [SerializeField]public List<SkillView> _skillView;
    private Indicator _indicator;
    public GameObject baseSkillPrefab;
    public void Init(PlayerDataSO player)
    {
        
    }

    public void setData(PlayerModel playerModel)
    {
        _skillData = new List<SkillData>();
        if (playerModel != null)
        {
            _skillData = playerModel.skillData;
            StartCoroutine(spawnPrefabs(playerModel));
        }
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
        throw new NotImplementedException();
    }
    
    

    public void destroySkill()
    {
        
    }
    [Button]
    public IEnumerator spawnPrefabs(PlayerModel playerModel)
    {
        yield return new WaitForSeconds(.2f);
        if (playerModel != null)
        {
            if (playerModel.skillData.Count != 0)
            {
                for (int i = 0; i < playerModel.skillData.Count; i++)
                {
                    _skillView[i].data = playerModel.skillData[i];
                    _skillView[i].initSkill(playerModel);
                    _skillView[i].data.skillState = SkillState.ready;
                }
            }
        }
    }
}
