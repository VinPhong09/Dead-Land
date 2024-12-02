using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Layer
{
    Player,
    Enemy,
    DeadZone
}
public enum Tag
{
    Player,
    Enemy,
    Weapon
}
public enum EnemyAmin
{
    Run,
    Defend,
    Attack,
    Hit,
    Die
}
public enum SceneName
{
    Phuc_Scene,
    StartGame,
    LoadingScene,
    MainMenu
}
public enum Volume
{
    sound,
    music
}
[System.Serializable]
public class EnemyLevel
{
    public string enemyLvl;
    public int hp;
    public float attack;
    public float exp;
    public float moveSpeed;
    public int enemySpawnAmount;
}
public static class pref
{
    public static float SetSound
    {
        set => PlayerPrefs.SetFloat(Volume.sound.ToString(), value);
        get => PlayerPrefs.GetFloat(Volume.sound.ToString());
    }
    public static float SetMusic
    {
        set => PlayerPrefs.SetFloat(Volume.music.ToString(), value);
        get => PlayerPrefs.GetFloat(Volume.music.ToString());
    }
}