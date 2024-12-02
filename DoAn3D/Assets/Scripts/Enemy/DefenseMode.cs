using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DefenseMode : MonoBehaviour
{
    public ParticleSystem ParticleSystem;
    public Transform grid;

    private ParticleSystem _defenseVfx;
   
    public IEnumerator EnemyDefenseloop()
    {
        _defenseVfx = Instantiate(ParticleSystem, Vector3.zero, Quaternion.identity);
        _defenseVfx.transform.SetParent(grid);
        _defenseVfx.transform.localScale = Vector3.one;
        _defenseVfx.transform.position = transform.position;
        _defenseVfx.Play();
        Destroy(_defenseVfx, 1f);
        yield return null;
    }
}
