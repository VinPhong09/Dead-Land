using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Pause = !Pause;
    }

    [SerializeField] private Image uiFill;
    [SerializeField] private Text uiTxt;

    public int duration;

    private int remainingDuration;
    private bool Pause;
    private bool enemyLvlUp ;
    private int lvlUp = 1;

    public bool EnemyLvlUp { get => enemyLvlUp; }
    public int LvlUp { get => lvlUp; }

    private void Start()
    {
        Being(duration);
    }
   
    private void Being(int second)
    {
        remainingDuration = second;
        StartCoroutine(UpdateTimer());
       
    }
    private IEnumerator UpdateTimer()
    {
        while (true && remainingDuration >= 0)
        {
           
            if (!Pause )
            {
                enemyLvlUp = false;
                uiTxt.text = $"{remainingDuration / 60:00}:{remainingDuration % 60:00}";
                uiFill.fillAmount = Mathf.InverseLerp(0, duration, remainingDuration);
                remainingDuration--;
                yield return new WaitForSeconds(1f);
               
            }
           if(remainingDuration < 0 && lvlUp < 9)
            {
                remainingDuration = duration;
                lvlUp++;
                enemyLvlUp = true;
                
            }    
            yield return null;
        }
       
    }

    
}
