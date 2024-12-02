using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine.Networking;

public class SaveLoadSystem : MonoBehaviour
{
    public static SaveLoadSystem _instance;
    public string _filePath;
    public GameData _gameData { get; set; }
    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
        _filePath = Application.persistentDataPath + "/" + "SaveData.save";
    }

    public void SaveData(GameData _GD)
    {
        if (string.IsNullOrEmpty(_filePath))
        {
            CreateJsonFile(_filePath);
        }
        string jsonData = JsonConvert.SerializeObject(_GD);
        File.WriteAllText(_filePath,jsonData);
    }

    private void CreateJsonFile(String FileName)
    {
        File.Create(FileName);
    }

    public IEnumerator LoadData()
    {
        if (File.Exists(_filePath))
        {
            UnityWebRequest www = UnityWebRequest.Get(_filePath);
            yield return www.SendWebRequest();
            
            if (www.result == UnityWebRequest.Result.Success)
            {
                string jsonText = www.downloadHandler.text;
                _gameData = JsonConvert.DeserializeObject<GameData>(jsonText);
            }
        }
    }
}
