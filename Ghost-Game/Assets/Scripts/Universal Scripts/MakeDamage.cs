using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeDamage : MonoBehaviour
{
    public int damage = 10;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {
            other.GetComponent<Health_and_Damage>().RestarVida(damage);
        }
        
    }

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Player")
        {
            other.GetComponent<Health_and_Damage>().RestarVida(damage);
        }
        
    }
}
