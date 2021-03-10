using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
 /*
 1.
 a. Zidane De Cantuaria
 b. 002325417
 c. decantuaria@chapman.edu  
 d. CPSC 245-01
 e. Eel Shooter - Milestone 2
 f. This is my own work, and I did not cheat on this assignment.
  
2. ObjectPool is a class that creates a set of gameObjects to be used as targets that LevelLogic will activate and access for the player to hit.
    Once a target is hit, it should return to the object pool.

*/
    public static ObjectPool SharedInstance;
    public static List<GameObject> pooledTargets;
    public GameObject targetToPool;
    public int amountToPool;

    private void Awake()
    {
        SharedInstance = this;
    }

    public GameObject GetPooledTarget()
    {
        for(int i = 0; i < amountToPool; i++)
        {
            if (pooledTargets[i].activeInHierarchy)
            {
                return pooledTargets[i];
            }
        }

        return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        pooledTargets = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(targetToPool);
            tmp.SetActive(false);
            pooledTargets.Add(tmp);
        }
    }
}
