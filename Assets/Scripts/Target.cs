using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
 /*
 1.
 a. Zidane De Cantuaria
 b. 002325417
 c. decantuaria@chapman.edu  
 d. CPSC 245-01
 e. Eel Shooter - Milestone 2
 f. This is my own work, and I did not cheat on this assignment.
  
2. Target controls the properties and behaviour of the targets once hit. Information about targets will be passed into and from LevelLogic, which will determine and modify target properties.

*/

    public string targetColor;
    public float targetSpeed;
    public int targetValue;
    public bool isBomb; 
    public bool isHit; 
    public LevelLogic levelLogic; 

    public Target()
    {
        targetColor = "null";
        targetSpeed = 0;
        targetValue = 0;
        isBomb = false;
        isHit = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    //SetTargetValues is a method to be called by level logic to change a targets properties based on level difficulty
    public void SetTargetValues(string inputColor, float inputSpeed, int inputValue, bool inputIsBomb) 
    {
        targetColor = inputColor;
        targetSpeed = inputSpeed;
        targetValue = inputValue;
        isBomb = inputIsBomb;
    }


   //When clicked on, targets will register as being hit and send information to LevelLogic
    private void OnMouseDown()
    {
        isHit = true;
        Debug.Log("Target Hit!");
        levelLogic.Hit(this.gameObject); 
    }
}
