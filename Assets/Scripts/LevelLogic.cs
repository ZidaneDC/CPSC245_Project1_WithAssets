using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelLogic : MonoBehaviour
{

 /*
 1.
 a. Zidane De Cantuaria
 b. 002325417
 c. decantuaria@chapman.edu  
 d. CPSC 245-01
 e. Eel Shooter - Milestone 2
 f. This is my own work, and I did not cheat on this assignment.
  
2. LevelLogic keeps track of player objectives and progress in a level, behaviour once a target is hit, as well as spawning attacks and targets to hit at given intervals.
    It also determines the difficulty of a given level by setting variables such as target speeds and attack rates. 
    Once a level has been completed, its information will be sent to GameLogic.

*/
    
    int levelCount;
    string objectiveColor;
    int levelScore;
    bool bonusLevel;
    int objectiveGoal; //number of correct targets that needs to be hit to beat the level
    int objectiveCount; //how many correct targets have been hit already
    double objectiveChance; //odds that the correct type of target is spawned
    int scoreMultiplier;
    float targetTimer; //how often a new wave of targets appears
    float attackTimer; //how often an attack appears
    int targetCount; //how many targets are spawned at once
    float targetSpeed; //speed targets move at
    public List<Transform> spawnPoints;

    List<string> objectiveOptions = new List<string>() { "red", "blue", "green", "yellow", "purple"}; //color options, bomb targets will be set to black
    System.Random random = new System.Random(); //for objective randomization

    public Attacks attacks;
    public ObjectPool objectPool;
    public UI UI; //UI reference to call methods


    //default constructor
    public LevelLogic()
    {
        levelCount = 1;
        objectiveColor = "null";
        bonusLevel = false;
        levelScore = 0;
        objectiveGoal = 1;
        objectiveCount = 0;
        objectiveChance = 1; //1 being 100% chance
        scoreMultiplier = 1;
        targetTimer = 7;
        attackTimer = 15f;
        targetCount = 3;
        targetSpeed = 1.0f;

    }


    // Start is called before the first frame update
    void Start()
    {
        //call game or ui class to display the level start panel
        //SetOdds(); //determine objectives
        Debug.Log("Level Number: " + levelCount);
        Debug.Log("Objective Color: " + objectiveColor);
        UI.UpdateObjective(objectiveColor, objectiveCount, objectiveGoal);
        Invoke("LevelStart", 2f);
    }

    void LevelStart()
    {
        StartCoroutine(SpawnAttacks());
        StartCoroutine(SpawnTargets());
    }

    // Update is called once per frame
    void Update()
    {
        if(objectiveCount == objectiveGoal)
        {
            LevelComplete();
            levelCount += 1;
            LevelReset();
            StartCoroutine(SpawnAttacks());
            StartCoroutine(SpawnTargets());
        }
       
    }


    public void Hit(GameObject hitObject)
    {
        Debug.Log("Hit Registered");
        Target hitTarget = hitObject.GetComponent<Target>();
       //if the target is not the correct color
       if(hitTarget.targetColor != objectiveColor)
        {
            hitTarget.isHit = true;
            hitObject.SetActive(false);
        }
        //if the target is the correct color 
        else
        {
            hitTarget.isHit = true;
            hitObject.SetActive(false);
            //add points and point multiplier
            levelScore += hitTarget.targetValue;
            objectiveCount += 1;
            UI.UpdateObjective(objectiveColor, objectiveCount, objectiveGoal);
            Debug.Log("Objective Count: " + objectiveCount + " / " + objectiveGoal);
        }

        if (hitTarget.isBomb == true)
        {
            DestroyAll();
        }
    }


   

    //SetOdds is a method that will set the chances of objectives and attacks spawning, DO THIS LAST
    public void SetOdds()
    {

    }

    
    IEnumerator SpawnTargets()
    {
        //FIXME: this method still needs a way to return to return targets back to the obect pool once a wave has ended

        while (true)
        {
            List<GameObject> targetsToLaunch = new List<GameObject>();
            for (int i = 0; i < targetCount; i++)
            {
                int randPos = random.Next(0, ObjectPool.pooledTargets.Count);
                targetsToLaunch.Add(ObjectPool.pooledTargets[randPos]);
            }

            foreach (GameObject launchTarget in targetsToLaunch)
            {
                if (this.bonusLevel == true)
                {
                    UI.WarnForBombEel();
                    Debug.Log("Bomb target incoming!");
                    launchTarget.GetComponent<Target>().SetTargetValues(objectiveColor, targetSpeed, 100, true);
                    UI.Invoke("HideBombEelWarning", 2.0f);
                }

                launchTarget.gameObject.SetActive(true);
                //to add in later: method that determines the odds of the target ebing the correct color
                launchTarget.GetComponent<Target>().SetTargetValues(objectiveColor, targetSpeed, 100, false);
                Rigidbody targetRigidBody = launchTarget.GetComponent<Rigidbody>();
                launchTarget.gameObject.transform.position = PickSpawnLocation();
                targetRigidBody.AddForce(Vector3.up* 50f);
            }

            yield return new WaitForSeconds(targetTimer);
        }
    }

   
    IEnumerator SpawnAttacks()
    {
        while (true)
        {
            if (random.Next(0, 2) == 0)
            {
                UI.WarnForWave();
                attacks.SpawnWave();
                UI.Invoke("HideWaveWarning", 2.0f);
            }
            else
            {
                UI.WarnForDragon();
                attacks.SpawnDragon();
                UI.Invoke("HideDragonWarning", 2.0f);
            }

            yield return new WaitForSeconds(attackTimer);
        }
    }

       
    
    public void DestroyAll()
    {
        //FIXME: shouldn't access all of the object pool, only the targets that are currently active
        foreach(GameObject targetToDisable in ObjectPool.pooledTargets){
            if (gameObject.activeSelf)
            {
                targetToDisable.GetComponent<Target>().isHit = true;
                targetToDisable.gameObject.SetActive(false);
            }
        }
        
    }

   
   public LevelLogic LevelComplete()
    {
        DestroyAll();
        StopAllCoroutines();
        return this;
    }

    //LevelReset is a method that will activate once a level is beaten, and assign new values to the level, such as increased target spawn, bonus level status, different objective color, etc.
    public void LevelReset()
    {
        objectiveCount = 0;
        if(levelCount == 2)
        {
            targetTimer = 6;
            attackTimer = 15;
            targetCount = 4;
            objectiveGoal += 2;
        }

        else if (levelCount == 3)
        {
            targetTimer = 5;
            attackTimer = 12;
            targetCount = 5;
            objectiveGoal += 3;
        }

        else
        {
            targetTimer = 4;
            attackTimer = 10;
            targetCount = 6;
            objectiveGoal += 3;
        }
        UI.UpdateObjective(objectiveColor, objectiveCount, objectiveGoal);
        Debug.Log("New level! Level " + levelCount);
        Debug.Log("Objective Color: " + objectiveColor);

        if (random.Next(0, 2) == 1)
        {
            bonusLevel = true;
            Debug.Log("Bonus Level!");
        }

        else
        {
            bonusLevel = false;
        }
        UI.UpdateBonusLevelUI(bonusLevel);
    }

    private Vector3 PickSpawnLocation()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Count);
        return spawnPoints[spawnIndex].position;
    }

}
