using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    /*
 1.
 a. Delaney Tobin
 b. 2339699
 c. dtobin@chapman.edu  
 d. CPSC 245-01
 e. Eel Shooter - Milestone 2
 f. This is my own work, and I did not cheat on this assignment.
  
2. Character controls the character's movement, as well as its collision with attacks

*/
    public GameObject dragon;
    public Rigidbody dragonRigidbody;
    private int charaacterHappiness;
    private int characterEnergy;
    private float thrust = 300f;
    private Vector3 smallScale;
    private Vector3 bigScale;
    private float sizeTimer = .75f;
    private Boolean isGrounded = false;
    private Boolean canMove;
    public GameLogic GameLogic;

    // Start is called before the first frame update
    void Start()
    {
        smallScale = new Vector3(1f, 0.5f, 1f);
        bigScale = new Vector3(1f, 1f, 1f);
        isGrounded = true;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canMove)
            shootFire();
        if (Input.GetKeyDown(KeyCode.W) && isGrounded && canMove)
            jump();
        if (Input.GetKeyDown(KeyCode.S) && canMove)
            duck();
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        int layerMask = 1 << 8;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            isGrounded = true;
            //print("is grounded");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            isGrounded = false;
            //print("is NOT grounded");
        }
    }

    private void shootFire()
    {
        //animation trigger
    }

    private void jump()
    {
        //character moves up with a certain amount of force
        //dragon.transform.position = new Vector3(transform.position.x, 3, transform.position.z);
        dragonRigidbody.AddForce(Vector3.up * thrust);
    }

    private void duck()
    {
        //character moving a certain height down
        if (canMove)
        {
            dragon.transform.position = new Vector3(transform.position.x, 0.35f, transform.position.z);
            dragon.transform.localScale = smallScale;
            StartCoroutine(waitForNormalSize());
        }
    }

    private IEnumerator waitForNormalSize()
    {
        yield return new WaitForSeconds(sizeTimer);
        GoToNormalSize();
    }

    private void GoToNormalSize()
    {
        dragon.transform.position = new Vector3(transform.position.x, 0.58f, transform.position.z);
        dragon.transform.localScale = bigScale;
    }

    public void SetCanMoveToFalse()
    {
        canMove = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Attack")
        {
            //print("life lost");
            GameLogic.LoseLife();
            Destroy(collision.gameObject);
        }
    }

}
