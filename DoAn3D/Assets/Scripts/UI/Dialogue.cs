using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    public static Dialogue _instance;
    public virtual void Show(bool value)
    {
        gameObject.SetActive(value);
    }
    public virtual void close()
    {
        gameObject.SetActive(false);
    }
    public virtual void Quit()
    {
        Application.Quit();
    }
    public void MoveToPlayGame()
    {
        SceneManager.LoadScene("LoadingScene");//MainScene/LoadingScene
    }

    public void LoadData()
    { 
        StartCoroutine(SaveLoadSystem._instance.LoadData());
        MoveToPlayGame();
    }
}
