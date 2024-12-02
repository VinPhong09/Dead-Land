using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    public SkillData skillData;
    //Check object is touching particle system
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Particle");
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
        Instantiate(skillData.skillEffect.effectPrefabs,this.gameObject.transform);
    }
}
