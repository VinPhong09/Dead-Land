using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamePopupGenerator : MonoBehaviour
{
    public static DamePopupGenerator _instance;
    public GameObject damePopup;

    public float timeDestroy;
    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [Button]
    public void createPopUp(Transform popupParent,float dame,Color color)
    {
        var popup = Instantiate(damePopup, new Vector3(popupParent.position.x, popupParent.position.y+1, popupParent.position.z),Quaternion.Euler(0,90,0),popupParent);
        
        var temp = popup.GetComponentInChildren<TextMeshProUGUI>();
        temp.text = dame.ToString();
        temp.color = color;
        Destroy(popup,timeDestroy);
    }
}
