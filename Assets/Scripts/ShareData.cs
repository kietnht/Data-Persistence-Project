using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ShareData : MonoBehaviour
{
    public static ShareData Instance;

    public string BestPlayerName = "NoName";
    public int BestScore = 0;

    public string playerName;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadScore();
        } else
        {
            Destroy(gameObject);
        }
    }

    [System.Serializable]
    class SaveData
    {
        public string bestPlayerName;
        public int besetScore;
    }

    public static void SaveScore()
    {
        var data = new SaveData()
        {
            besetScore = Instance.BestScore,
            bestPlayerName = Instance.BestPlayerName,
        };
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/best_score.json", json);
    }

    public void LoadScore()
    {
        var path = Application.persistentDataPath + "/best_score.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            var data = JsonUtility.FromJson<SaveData>(json);
            BestPlayerName = data.bestPlayerName;
            BestScore = data.besetScore;
        }
    }
}
