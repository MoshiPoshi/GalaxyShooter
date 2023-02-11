using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite [] lives; 
    public Image  livesImagesDisplay;
    public GameObject titlescreen;

    public Text  scoreText; 
    public int score;

    public void UpdateLives(int currentLives)
    {
        Debug.Log("Player lives: " + currentLives); 
        livesImagesDisplay.sprite = lives[currentLives];
    }

    public void UpdateScore()
    {
        score += 10;

        scoreText.text = "Score: " + score;
    }

     public void ShowTitleScreen()
     {
        titlescreen.SetActive(true);
     }

     public void HideTitleScreen()
     {
        titlescreen.SetActive(false);
        scoreText.text = "Score: ";
     }
}