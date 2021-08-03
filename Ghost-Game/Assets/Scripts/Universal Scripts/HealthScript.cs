using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour {

    // PUBLIC
    public float health = 100f;
    public bool is_Player;

    // PRIVATE
    private CharacterAnimation animationScript;
    private EnemyMovement enemyMovement;
    private Rigidbody rb;
    public Collider CapsuleCollider;
    public BoxCollider BoxCollider;

    public bool characterDied; // prueba
    public bool characterKnockdown; // prueba

    // METHODS
    private void Awake() {

        animationScript = GetComponentInChildren<CharacterAnimation>();
        enemyMovement = GetComponent<EnemyMovement>();
        CapsuleCollider = GetComponent<Collider>();
        BoxCollider = GetComponentInChildren<BoxCollider>();
    }

    public void ApplyDamage(float damage, bool knockDown) {

        if(characterDied){
            return;
        }

        health -= damage;

        // display health ui

        if(health <= 0f) {
            animationScript.Death();
            characterDied = true;
            enemyMovement.enabled= false;
            CapsuleCollider.enabled = false;
            BoxCollider.size = new Vector3(0.3f,0.2f,0.3f);
            rb.constraints = RigidbodyConstraints.FreezePosition;

            // if is player deactivate enemy script
            if(is_Player) {

            } else {
                CapsuleCollider.enabled = false;
                
            }

            return;
        }

        if(!is_Player) {

            if(knockDown) {
                animationScript.KnockDown();
                // if(Random.Range(0,2) > 0) {
                //     animationScript.KnockDown();
                // }

            } else {
                animationScript.HeadHit();
                // if(Random.Range(0,3) > 1) {
                //     animationScript.HeadHit();
                // }
            }

        }


    } // apply damage

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
