using System;
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
            LoadBestScore();
            LoadHighScore();
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

    public static void SaveBestScore()
    {
        var data = new SaveData()
        {
            besetScore = Instance.BestScore,
            bestPlayerName = Instance.BestPlayerName,
        };
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/best_score.json", json);
    }

    public void LoadBestScore()
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

    [System.Serializable]
    public class HighScoreData
    {
        public string playerName;
        public int score;
        public string date;

        public override string ToString()
        {
            return $"{playerName} : {score} : {date}";
        }
    }

    [Serializable] public struct HighScoreDataWrapper { public HighScoreData[] highScoreArray; }
    HighScoreDataWrapper highScoreData;

    public HighScoreData[] HighScoreArray
    {
        get { return highScoreData.highScoreArray; }
        
    }

    //public HighScoreData[] highScoreArray;
    public int maxHighScoreEntry = 11;

    public static void EvaluateScore(int score)
    {
        if (score <= 0) return;

        var data = Instance.HighScoreArray;
        int i = Instance.maxHighScoreEntry - 1;
        while (i > 0)
        {
            if (data[i - 1].score >= score) break;
            data[i] = data[i - 1];
            i--;
        }
        if (data[i].score <= score)
            data[i] = new HighScoreData
            {
                playerName = Instance.playerName,
                score = score,
                date = DateTime.Now.ToString("dd/MM/yyyy")
            };
        
        SaveHighScore();
    }
    
    public static void SaveHighScore()
    {
        string json = JsonUtility.ToJson(Instance.highScoreData);
        File.WriteAllText(Application.persistentDataPath + "/high_score.json", json);
    }

    public void LoadHighScore()
    {
        var path = Application.persistentDataPath + "/high_score.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            highScoreData = JsonUtility.FromJson<HighScoreDataWrapper>(json);
        } else
        {
            highScoreData.highScoreArray = new HighScoreData[maxHighScoreEntry];
            for (int i = 0; i < maxHighScoreEntry; i++)
            {
                highScoreData.highScoreArray[i] = new HighScoreData();
            }
        }
    }

}
