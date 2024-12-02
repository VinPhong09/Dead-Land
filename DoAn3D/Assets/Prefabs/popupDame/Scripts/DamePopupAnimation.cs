using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamePopupAnimation : MonoBehaviour
{
    public AnimationCurve opacityCurve;
    public AnimationCurve scaleCurve;
    public AnimationCurve heightCurve;
    
    private TextMeshProUGUI tmp;
    private float time = 0;
    private Vector3 origin;
    
    private void Awake()
    {
        tmp = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        origin = this.transform.position;
    }
    void Update()
    {
        tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, opacityCurve.Evaluate(time));
        transform.localScale = Vector3.one * scaleCurve.Evaluate(time);
        transform.position = origin+new Vector3(0, 1+heightCurve.Evaluate(time), 0);
        time += Time.deltaTime;
    }
}
