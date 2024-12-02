using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBg : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.ins.SetMusic(AudioManager.ins.playBG);
    }
}
