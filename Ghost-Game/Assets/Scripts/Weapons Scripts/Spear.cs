using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{

    private  Rigidbody RB; 
    private  BoxCollider COLL;

    // public  GameObject reposeReference;
    // public  GameObject attackReference;

    public Transform player;
    public Transform containerRepose;
    public Transform containerAttack;

    public bool equiped = false;
    public bool throwed = false;
    // public static bool slotFull;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        COLL = GetComponent<BoxCollider>();
        // SpearPosition = transform.position;
        // SpearRotation = transform.rotation;
        onRepose();  
    }

    // Update is called once per frame
    void Update()
    {
        if( !equiped && Input.GetKey(KeyCode.Q) ) {
            Debug.Log("Spear at Hand for Attack!" );

            onAttack();
        }
        if ( equiped && Input.GetKey(KeyCode.E) ) {
            Debug.Log("Spear onRepose" );

            onRepose();
        }
        // if ( !throwed && Input.GetKey(KeyCode.C) ) {
        //     Debug.Log("Spear Throwed" );

        //     onThrow();
        // }
        // if(throwed) {
        //     setSpearThrowRotation();
        // }

        // getPosition();
    }

    // void getPosition() {

    //     Debug.Log("isOnRepose:" + isOnRepose);
    //     Debug.Log("reposeReference.transform.position:" + reposeReference.transform.position);
    //     Debug.Log("attackReference.transform.position:" + attackReference.transform.position);
    //     Debug.Log("SpearPosition:" + SpearPosition);
    //     // Debug.Log("repRelPos:" + repRelPos);
    //     var reposePosition = reposeReference.transform.position;
    //     var attackPosition = attackReference.transform.position;

    //     if(equiped) {
    //         onRepose() {

    //         }
           
    //         // SpearRotation = new Quaternion();
    //     }

    //     if(!equiped) {
    //         SpearPosition = new Vector3(attackPosition.x,attackPosition.y,attackPosition.z);
    //     }

    //     transform.position = SpearPosition;
           
    // }

    void onRepose() {
        // slotFull = false;

        //Make Spear a child of the containerRepose and move it to default position
        transform.SetParent(containerRepose);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        //Make RigidBody kinematic and BoxCollider a trigger
        // rb.isKinematic = false;
        // coll.isTrigger = false;
        equiped = false;

    }

    void onAttack() {

        // slotFull = true;

        //Make Spear a child of the containerAttack and move it to default position
        transform.SetParent(containerAttack);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;


        //Make RigidBody kinematic and BoxCollider a trigger
        // rb.isKinematic = true;
        // coll.isTrigger = true;

        equiped = true;
    }

    // void onThrow(){

    //     transform.SetParent(containerAttack);
    //     // transform.position = new Vector3(0,0,0);
    //     // transform.localRotation = Quaternion.Euler(new Vector3(0,0,0));
    //     // transform.localScale = Vector3.one;
    //     throwed = true;

    //     // Instantiate(this);
    

    // }

    // void setSpearThrowRotation() {

        

    // }
}
