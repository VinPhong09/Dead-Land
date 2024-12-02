using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseDialogue : Dialogue
{
    public override void Show(bool value)
    {
        base.Show(value);
        Time.timeScale = 0f;
    }
    public override void close()
    {
        base.close();
        Time.timeScale = 1f;
    }
    public void Resume()
    {
        close();
    } 
    public void BackToMainMenu()
    {
        close();
        GameController._instantie.SaveData();
        SceneManager.LoadScene(SceneName.MainMenu.ToString());
    }
    public override void Quit()
    {
        base.Quit();
    }
     
}
