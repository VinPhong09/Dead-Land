using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverDialogue : Dialogue
{
    public override void Show(bool value)
    {
        base.Show(value);
    }
    public void BackToMain()
    {
        GameController._instantie.SaveData();
        SceneManager.LoadScene(SceneName.MainMenu.ToString());
    }
    public override void Quit()
    {
        base.Quit();
    }

}
