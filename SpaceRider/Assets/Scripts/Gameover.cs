using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour
{
    [SerializeField] GameObject gameoverPanel;
    [SerializeField] Transform vechicle;

    public void ShowGameoverPanel()
    {
        gameoverPanel.SetActive(true);
        gameoverPanel.GetComponent<Animator>().Play("Pop_Up_Gameover");
    }

    private void Update()
    {
        if(vechicle.position.y < -30f)
        {
            if(!gameoverPanel.active)
            {
                ShowGameoverPanel();
            } 
        }
    }

    public void Reset()
    {
        SceneManager.LoadScene("Main Scene");
    }
}
