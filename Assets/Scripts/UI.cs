using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{

    /*
1.
a. Delaney Tobin
b. 2339699
c. dtobin@chapman.edu  
d. CPSC 245-01
e. Eel Shooter - Milestone 2
f. This is my own work, and I did not cheat on this assignment.

2. This class handles all the UI elements in the game.

*/
    public CanvasGroup StartGameCanvasGroup;
    public CanvasGroup WaveWarningCanvasGroup;
    public CanvasGroup DragonWarningCanvasGroup;
    public CanvasGroup GameOverCanvasGroup;
    public CanvasGroup PauseCanvasGroup;
    public CanvasGroup InGameUICanvasGroup;
    public CanvasGroup BombEelWarningCanvasGroup;
    public CanvasGroup BonusLevelCanvasGroup;
    public Text LevelText;
    public Text LivesText;
    public Text ObjectiveCounter;
    public LevelLogic LevelLogic;
    public GameLogic GameLogic;
    public Image ObjectiveColorImage;

    // Start is called before the first frame update
    void Start()
    {
        ResetGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetGame()
    {
        StartGameCanvasGroup.alpha = 1;
        StartGameCanvasGroup.blocksRaycasts = true;
        InGameUICanvasGroup.alpha = 0;
        InGameUICanvasGroup.blocksRaycasts = false;
        WaveWarningCanvasGroup.alpha = 0;
        DragonWarningCanvasGroup.alpha = 0;
        //GameOverCanvasGroup.alpha = 0;
        //GameOverCanvasGroup.blocksRaycasts = false;
        PauseCanvasGroup.alpha = 0;
        PauseCanvasGroup.blocksRaycasts = false;
        BombEelWarningCanvasGroup.alpha = 0;
        BonusLevelCanvasGroup.alpha = 0;
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        StartGameCanvasGroup.alpha = 0;
        StartGameCanvasGroup.blocksRaycasts = false;
        InGameUICanvasGroup.alpha = 1;
        InGameUICanvasGroup.blocksRaycasts = true;
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        PauseCanvasGroup.alpha = 1;
        InGameUICanvasGroup.blocksRaycasts = false;
        InGameUICanvasGroup.alpha = 0;
        PauseCanvasGroup.blocksRaycasts = true;
        Time.timeScale = 0;
    }


    public void Restart()
    {
        GameOverCanvasGroup.alpha = 0;
        GameOverCanvasGroup.blocksRaycasts = false;
        ResetGame();
        LevelLogic.LevelReset();
    }

    public void ResumeGame()
    {
        PauseCanvasGroup.alpha = 0;
        PauseCanvasGroup.blocksRaycasts = false;
        InGameUICanvasGroup.alpha = 1;
        InGameUICanvasGroup.blocksRaycasts = true;
        Time.timeScale = 1;
    }

    public void GameOverScreen()
    {
        GameOverCanvasGroup.alpha = 1;
        GameOverCanvasGroup.blocksRaycasts = true;
    }

    public void Quit()
    {
        ResetGame();
    }

    public void UpdateObjective(string objectiveColor, int objectiveCount, int objectiveGoal)
    {
        ObjectiveCounter.text = objectiveCount + " / " + objectiveGoal;
        if (objectiveColor == "yellow")
            ObjectiveColorImage.GetComponent<Image>().color = Color.yellow;
        if (objectiveColor == "red")
            ObjectiveColorImage.GetComponent<Image>().color = Color.red;
        if (objectiveColor == "purple")
            ObjectiveColorImage.GetComponent<Image>().color = new Color (113, 45, 195);
        if (objectiveColor == "blue")
            ObjectiveColorImage.GetComponent<Image>().color = Color.blue;
        if (objectiveColor == "green")
            ObjectiveColorImage.GetComponent<Image>().color = Color.green;
    }

    public void UpdateLivesUI(int lives)
    {
        LivesText.text = "Lives: " + lives;
    }

    public void UpdateLevelUI(int level)
    {
        level = GameLogic.UpdateLevel();
        LevelText.text = "Level: " + level;
    }

    public void WarnForDragon()
    {
        DragonWarningCanvasGroup.alpha = 1;
    }

    public void WarnForWave()
    {
        WaveWarningCanvasGroup.alpha = 1;
    }

    public void HideDragonWarning()
    {
        DragonWarningCanvasGroup.alpha = 0;
    }

    public void HideWaveWarning()
    {
        WaveWarningCanvasGroup.alpha = 0;
    }

    public void WarnForBombEel()
    {
        BombEelWarningCanvasGroup.alpha = 1;
    }

    public void HideBombEelWarning()
    {
        BombEelWarningCanvasGroup.alpha = 0;
    }

    public void UpdateBonusLevelUI(bool bonusLevel)
    {
        if (bonusLevel == false)
        {
            BonusLevelCanvasGroup.alpha = 0;
        }
        else
        {
            BonusLevelCanvasGroup.alpha = 1;
        }
    }

}
