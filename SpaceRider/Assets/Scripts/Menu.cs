using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    int score;
    // Start is called before the first frame update
    void Start()
    {
        score = PlayerPrefs.GetInt("score");
        DisplayScore();
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenSettingsPanel()
    {

    }

    private void DisplayScore()
    {
        //Print Score (XM XT XH)
        int scoreToDisplay = (int)(score * 10f);
        int million = scoreToDisplay / 1000000;
        int thousand = (scoreToDisplay % 1000000) / 1000;
        int hundred = scoreToDisplay % 1000;

        string displayText = hundred.ToString();
        if (thousand > 0)
        {
            displayText = thousand.ToString() + "K " + hundred.ToString();
        }
        if (million > 0)
        {
            displayText = million.ToString() + "M " + thousand.ToString() + "K " + hundred.ToString();
        }

        if(scoreText)
        {
            scoreText.text = displayText;
        }
        
    }
}
