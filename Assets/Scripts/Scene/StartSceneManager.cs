using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class StartSceneManager : MonoBehaviour
{
    public Text textBestScore;
    public InputField playerInput;
    public GameObject nameErrorMessage;

    // Start is called before the first frame update
    void Start()
    {
        string bestText = "No Best Score Yet";
        if(ShareData.Instance.BestScore != 0)
        {
            bestText = "Best Score : ";
            bestText += ShareData.Instance.BestPlayerName + " : ";
            bestText += ShareData.Instance.BestScore;
        }
        textBestScore.text = bestText;
    }

    public void StartNewGame()
    {
        string playerName = "NoName";
        if(!playerInput.text.Equals("Enter Player Name..."))
        {
            //validate data
            if (ValidName(playerInput.text))
                playerName = playerInput.text;
            else
            {
                nameErrorMessage.SetActive(true);
                return;
            }
        }
        ShareData.Instance.playerName = playerName;
        SceneManager.LoadScene(1);
    }

    public void StartHighScore()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public bool ValidName(string name)
    {
        return (name.Length > 0 && name.Length <= 15);
    }
}
