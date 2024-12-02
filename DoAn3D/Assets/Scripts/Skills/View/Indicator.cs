using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Werewolf.StatusIndicators.Components;
using UnityEngine.EventSystems;

public class Indicator : MonoBehaviour
{
    private SplatManager Splats;
    public GameObject SpellIndicator;
    private GameObject CanceltheSpellButton;
    
    public GameObject baseGameObj;
    public SkillView skillView;
    public GameObject[] Indicators;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Splats = this.GetComponent<SplatManager>();
        foreach(GameObject Indicator in Indicators)
        {
            Indicator.gameObject.GetComponent<Projector>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (skillView != null)
        {
            if (GameController.spellon == true)
            {
                if (Input.touchCount > 0)
                {
                    // Move the cube if the screen has the finger moving.
                    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && skillView.isUse == true && !skillView.data.isLocked)
                    {
                        SpellON();
                    }
                }
            }
            if (GameController.spelloff == false)
            {
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && skillView.isUse == true && !skillView.data.isLocked )
                {
                    SpellBtnUnclickState();
                    SpellOFF();
                    FireSpell();
                }
            }
        }
        
    }
    GameObject SpellIndicatorClone;

    void SpellON()
    {
        foreach (GameObject Indicator in Indicators)
        {
            Indicator.gameObject.GetComponent<Projector>().enabled = true;
        }
        skillView.castSkill(skillView.data,this.transform,this.transform.parent);
    }
    void SpellOFF()
    {
        foreach (GameObject Indicator in Indicators)
        {
            Indicator.gameObject.GetComponent<Projector>().enabled = false;
        }
               
    }
    void FireSpell()
    {
        if (baseGameObj != null)
        {
            skillView.useSkill(skillView.data,baseGameObj.transform,this.transform.parent);
        }
    }
   
    void SpellBtnUnclickState()
    {
        GameController.spelloff = true;
    }
}
