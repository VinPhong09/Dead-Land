using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager ins;
    public AudioSource musicSour;
    public AudioSource soundSour;

    [Range(0f, 1f)]
    public float soundVol;
    [Range(0f, 1f)]
    public float musicVol;

  
    [Header("Player")]

    public AudioClip SwordSound;
    public AudioClip ShootSound;
    public AudioClip lightingSound;


    [Header("MainMenu")]

    public AudioClip musicBGSound;
    public AudioClip playBG;
    private void Awake()
    {

        MakeSingleton();
    }
    private void Start()
    {
        soundVol =  pref.SetSound;
        soundSour.volume = soundVol;
        musicVol =  pref.SetMusic ;
        musicSour.volume = musicVol;
    }
  

    public void SetSound(AudioClip audio)
    {
        if (soundSour)
        {
            soundSour.PlayOneShot(audio, soundVol);
        }
    }
   
    public void SetMusic(AudioClip audio, bool loop = true)
    {
        if (musicSour)
        {
            musicSour.clip = audio;
            musicSour.loop = loop;
            musicSour.volume = musicVol;
            musicSour.Play();
        }
    }
    public void PlayBackgroundMusic()
    {
        SetMusic(musicBGSound, true);

    }
    public void StopPlay()
    {
        if (musicSour) musicSour.Stop();
    }    
  
    void MakeSingleton()
    {
        if (ins == null)
        {
            ins = this;
           DontDestroyOnLoad(this);
        }
        else
        {
           Destroy(this.gameObject);
        }    
        
    }
   
}
