using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePuzzleScript : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "item")
        {
            Rigidbody colRigidBody = collision.gameObject.GetComponent<Rigidbody>();
            colRigidBody.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
