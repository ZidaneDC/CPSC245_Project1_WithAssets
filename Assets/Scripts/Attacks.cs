using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    /*
 1.
 a. Delaney Tobin
 b. 2339699
 c. dtobin@chapman.edu  
 d. CPSC 245-01
 e. Eel Shooter - Milestone 2
 f. This is my own work, and I did not cheat on this assignment.
  
2. Attacks handles spawing both the waves and the dragons that can hurt your character.

*/
    public GameObject wave;
    public GameObject dragon;
    //public float thrust = 300f;

    // Start is called before the first frame update
    void Start()
    {
        //SpawnWave();
        //SpawnDragon();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void SpawnWave()
    {
        GameObject go1 = Instantiate(wave, new Vector3(-10, 0.4f, 0), Quaternion.identity);
        Rigidbody waveRigidbody = go1.GetComponent<Rigidbody>();
        waveRigidbody.AddForce(Vector3.right * 300f);
    }

    public void SpawnDragon()
    {
        GameObject go2 = Instantiate(dragon, new Vector3(-10, 1, 0), Quaternion.identity);
        Rigidbody dragonRigidbody = go2.GetComponent<Rigidbody>();
        dragonRigidbody.AddForce(Vector3.right * 300f);
    }

}
