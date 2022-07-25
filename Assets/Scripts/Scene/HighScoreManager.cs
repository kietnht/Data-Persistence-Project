using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject listParent;

    // Start is called before the first frame update
    void Start()
    {
        if(ShareData.Instance != null)
        {
            LoadHighScore();
        }
    }

    private void LoadHighScore()
    {
        for (int i = 0; i < ShareData.Instance.maxHighScoreEntry; i++)
        {
            if (ShareData.Instance.HighScoreArray[i].score == 0) return;
            var line = GetNewTextLine();
            line.text = ShareData.Instance.HighScoreArray[i].ToString();
        }
    }

    GameObject GetNewLine()
    {
        return Instantiate(linePrefab, listParent.transform);
    }

    Text GetNewTextLine()
    {
        var line = GetNewLine();
        return line.GetComponent<Text>();
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}
