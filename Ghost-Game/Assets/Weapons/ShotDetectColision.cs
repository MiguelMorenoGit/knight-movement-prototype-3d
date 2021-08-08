using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotDetectColision : MonoBehaviour
{

    private Rigidbody rb;
    private BoxCollider box;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        box = GetComponent<BoxCollider>();
    }

    void OnCollisionEnter(Collision col) {
        Debug.Log(gameObject.name + " has collided with " + col.gameObject.name);
        print("we call a coroutine");

        if (col.gameObject.name != "Player") {
            // we freeze position of gameObject
            if(rb != null) {
                rb.constraints = RigidbodyConstraints.FreezePosition;
            }
            if(box != null) {
                box.enabled = false;
            }

            // wait a few seconds before destroy GO
            StartCoroutine(DestroyGameObject());
        }

    }

    void OnTriggerEnter(Collider other) {
        Debug.Log(gameObject.name + " was trigged by " + other.gameObject.name); 
        // print(gameObject.name +  " will be destroy");
        // Destroy(gameObject);
    }

    IEnumerator DestroyGameObject() {

        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
    
}
