using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MEC;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;

public class PlayerLevelUpDataDesign 
{
   private Dictionary<string, PlayerDataLv> _playerDataLevelDictionary ;
   public class PlayerDataLv
   {
      public int Level { get; set; }
      public int Exp { get; set; }
   }

   public IEnumerator Initialize()
   {
      string filePath = Path.Combine(Application.streamingAssetsPath +  "/DataDesignJson", "PlayerLevelUp.json");
      _playerDataLevelDictionary = new Dictionary<string, PlayerDataLv>();
      if (File.Exists(filePath))
      {
         UnityWebRequest www = UnityWebRequest.Get(filePath);
         yield return www.SendWebRequest();
         if (www.result == UnityWebRequest.Result.Success)
         {
            string jsonText = www.downloadHandler.text;
            _playerDataLevelDictionary = JsonConvert.DeserializeObject<Dictionary<string, PlayerDataLv>>(jsonText);
         }
      }
   }

   public Dictionary<string, PlayerDataLv> GetDictionary => _playerDataLevelDictionary;
}
