using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseSkill
{
    float getDame();
    void unlockSkill(PlayerModel playerModel);
    void useSkill(SkillData skillData,Transform skillPosition,Transform skillParent);
    void destroySkill();
}
