using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{

/*
 1.
 a. Delaney Tobin
 b. 2339699
 c. dtobin@chapman.edu  
 d. CPSC 245-01
 e. Eel Shooter - Milestone 2
 f. This is my own work, and I did not cheat on this assignment.
  
2. GameLogic controls the overall game aspects such as lives, level updates, and pausing,

*/
    public int lives;
    private int currentLevel;
    public Character Character;
    public LevelLogic LevelLogic;
    public UI UI;
    //UI reference
    //total score

    void Start()
    {
        lives = 3;
        UI.UpdateLivesUI(lives);
        currentLevel = 1;
        UI.UpdateLevelUI(currentLevel);
    }

    void Update()
    {
        CheckLives();
    }

    public void LoseLife()
    {
        lives--;
        print("life lost: " + lives);
        UI.UpdateLivesUI(lives);
    }

    private void CheckLives()
    {
        if (lives <= 0)
            EndGame();
    }

    private void EndGame()
    {
            //end level: stop eel spawn
            //stop attack spawn
            LevelLogic.LevelComplete();
            //show end screen
            //character functionality ended
            Character.SetCanMoveToFalse();
        UI.GameOverScreen();
            Debug.Log("Game over.");
    }

    private void Pause()
    {
        //game stops running
        //pause screen comes up
    }

    public int UpdateLevel()
    {
        currentLevel++;
        return currentLevel;
    }
}
