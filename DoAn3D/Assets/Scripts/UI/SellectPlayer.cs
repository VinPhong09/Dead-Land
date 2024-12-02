using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;

public class SellectPlayer : MonoBehaviour
{
    public static SellectPlayer ins;
    public int index;
    public Button button;
    public GameObject checkMale;
    public GameObject checkFemale;

    private void Awake()
    {
        ins = this;
    }
    private void Update()
    {
        if (button == null) return;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => ChoosePlayer());
    }
    public void ChoosePlayer()
    {
        bool value = index == 1?true:false;
        Show(value);
        SceneManager.LoadScene("StartGame");

    }
    public void Show(bool value)
    {
        
        if(checkFemale)
        {
            checkFemale.SetActive(value);
        }  
        if(checkMale)
        {
            checkMale.SetActive(!value);
        }    
    }    

}


