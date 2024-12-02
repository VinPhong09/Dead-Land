using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingVolDialogue : Dialogue
{
    public Slider soundVol;
    public Slider musicVol;

  
    public override void Show(bool value)
    {
       
        base.Show(value);
        soundVol.value = pref.SetSound;
        musicVol.value = pref.SetMusic;

    }
    public void SetSoundVol(float value)
    {
      AudioManager.ins.soundVol = value;
      AudioManager.ins.soundSour.volume = value;
      pref.SetSound = AudioManager.ins.soundVol;
       
    }    
    public void SetMusicVol(float value)
    {
        AudioManager.ins.musicVol = value;
        AudioManager.ins.musicSour.volume = value;
        pref.SetMusic = AudioManager.ins.musicVol;
       
    }
    public override void close()
    {
        base.close();
    }
}
