using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Gameover : MonoBehaviour
{
    [SerializeField] GameObject gameoverPanel;
    [SerializeField] Transform vechicle;
    [SerializeField] TextMeshProUGUI scoreText;

    //Enables Gameover Panel & Pop up Animation
    public void ShowGameoverPanel()
    {
        gameoverPanel.SetActive(true);
        gameoverPanel.GetComponent<Animator>().Play("Pop_Up_Gameover");
        int score = FindObjectOfType<ScoreCounter>().GetScore();
        scoreText.text = (score * 10f).ToString();
        if(score > PlayerPrefs.GetInt("score"))
        {
            PlayerPrefs.SetInt("score", score);
        }
    }

    private void Update()
    {
        //Vechicle bountries (Vertical)
        if(vechicle.position.y < -30f)
        {
            if(!gameoverPanel.active)
            {
                ShowGameoverPanel();
            } 
        }
    }

    //Reload Main Scene
    public void Reset()
    {
        SceneManager.LoadScene("Main Scene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
