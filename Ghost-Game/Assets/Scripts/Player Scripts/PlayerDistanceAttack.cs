using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDistanceAttack : MonoBehaviour {

    public GameObject DistanceWeapon; 
    public GameObject WeaponLaunchHorizontal;
    public GameObject WeaponLaunchVertical;
    public GameObject WeaponLaunchDiagonal;

    private GameObject WeaponLaunchPosition;
   

    public float VelocityOfAttack = 25;
    public float offsetLaunchTime = 0.5f;

    // Start is called before the first frame update
    void Awake() {
        // DistanceWeapon = GetComponent<GameObject>();
        // WeaponLaunchHorizontal = GetComponent<GameObject>();
        // WeaponLaunchVertical = GetComponent<GameObject>();
        // WeaponLaunchDiagonal = GetComponent<GameObject>(); 
        WeaponLaunchPosition = WeaponLaunchHorizontal;
    }

    // Update is called once per frame
    void Update() {
        CheckInputAttack();
    }

    void CheckInputAttack() {

        Set_Launch_Position();

        if(Input.GetKeyDown(KeyCode.C)) {


            StartCoroutine(LaunchAfterTime());
        }
    }

    void Set_Launch_Position() {

        if(Input.GetKey(KeyCode.V) ) {

            WeaponLaunchPosition = WeaponLaunchDiagonal;

        } else if (Input.GetKey(KeyCode.UpArrow) ) {

            WeaponLaunchPosition = WeaponLaunchVertical;

        } else {
            WeaponLaunchPosition = WeaponLaunchHorizontal;
        }
    }

    IEnumerator LaunchAfterTime() {
        print("Courutine start");

        yield return new WaitForSeconds(offsetLaunchTime);
        Throw_Spear();
    }

    void Throw_Spear() {

        print("Spear was Launched");
        print("WeaponLaunchPosition " + WeaponLaunchPosition.name);
       
        Rigidbody WeaponInstance;
        WeaponInstance = Instantiate(DistanceWeapon.GetComponent<Rigidbody>(), WeaponLaunchPosition.transform.position, WeaponLaunchPosition.transform.rotation);
        WeaponInstance.isKinematic = false;
        WeaponInstance.useGravity = false;
        WeaponInstance.transform.parent = null;
        WeaponInstance.AddForce(WeaponLaunchPosition.transform.forward * VelocityOfAttack, ForceMode.Impulse);
    }
}
