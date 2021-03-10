using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAddForce : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Wave();
    }
    void Wave()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.right * 300f);
    }
}
