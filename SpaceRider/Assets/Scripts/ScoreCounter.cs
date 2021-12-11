using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] float scoreFactor = 10f;

    private int currentScore = 0;
    private int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score = (int)(currentScore + transform.position.x);
        DisplayScore();
    }

    public void ChangeUniverse()
    {
        currentScore = (int)score;
    }

    public int GetScore()
    {
        return score;
    }

    private void DisplayScore()
    {
        int scoreToDisplay = (int)(score * scoreFactor);
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
        
        scoreText.text = displayText;
    }
}
