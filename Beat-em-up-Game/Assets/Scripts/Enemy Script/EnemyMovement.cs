using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    //components
    private Transform playerTarget;
    private CharacterAnimation enemy_Anim;
    private Rigidbody myBody;
    private Animator animator;
    private HealthScript playerHealth;

    // public variables
    public float speed = 5f;
    public float attack_Distance = 1f;

    // private variables
    private float chase_Player_After_Attack = 1f;
    private float default_Attack_Time = 1.8f;
    private float waitBeforeMove_Time = 1.5f;
    private float current_Attack_Time;
    private float numberOfAttacks;
    public bool followPlayer;
    private bool attackPlayer;

    private void Awake() {
        enemy_Anim = GetComponentInChildren<CharacterAnimation>();
        myBody = GetComponent<Rigidbody>();
        playerTarget = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;
        animator = GetComponentInChildren<Animator>();
        playerHealth = GetComponent<HealthScript>();
    }

    // Start is called before the first frame update
    void Start()
    {
        followPlayer = true;
        current_Attack_Time = default_Attack_Time;
        numberOfAttacks = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        FollowTarget();
        Attack();
    }

    void FollowTarget() {

        // si no queremos que persiga al player
        if (!followPlayer) {
            return;
        }
        // if(!playerHealth.characterDied) {

            if (Vector3.Distance(transform.position, playerTarget.position) > attack_Distance) {

                transform.LookAt(new Vector3(playerTarget.position.x, 0, playerTarget.position.z));
                myBody.velocity = transform.forward * speed;

                if (myBody.velocity.sqrMagnitude != 0) { // comprobamos que nos estamos moviendo
                    enemy_Anim.Walk(true);
                }


            } else if ( Vector3.Distance(transform.position, playerTarget.position) <= attack_Distance) {
                
                myBody.velocity = Vector3.zero;
                enemy_Anim.Walk(false);

                followPlayer = false;
                attackPlayer = true;
                numberOfAttacks = 0f;
            }
        // } else {
        //     return;
        // }
    }

    void Attack() {

        if (!attackPlayer) {
            return;
        }

        current_Attack_Time += Time.deltaTime;
        transform.LookAt(new Vector3(playerTarget.position.x, 0, playerTarget.position.z));

        if(numberOfAttacks == 0){ //  no queremos que espere para realizar el primer ataque
            enemy_Anim.EnemyAttack(Random.Range(0, 3)); // el 3 nunca estara incluido si tuvieramos 4 aatques deberiamos poner 5
            // Debug.Log(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name); 
            current_Attack_Time = 0f;
            numberOfAttacks++;
        }

        if (current_Attack_Time > default_Attack_Time && numberOfAttacks > 0) { // si realiza mas de un ataque seguido que espere un tiempo default entre ataques
            enemy_Anim.EnemyAttack(Random.Range(0, 3)); // el 3 nunca estara incluido si tuvieramos 4 aatques deberiamos poner 5
            current_Attack_Time = 0f;
            numberOfAttacks++;
            // StartCoroutine(WaitAndTakeClip());
        }

        if (Vector3.Distance(transform.position, playerTarget.position) > 
                attack_Distance + chase_Player_After_Attack) {

            attackPlayer = false;
            StartCoroutine(WaitBeforeMove());
            
            
        }

    }

    IEnumerator WaitBeforeMove(){
        yield return new WaitForSeconds(waitBeforeMove_Time);
        followPlayer = true;
    }

    // IEnumerator WaitAndTakeClip(){
    //     yield return new WaitForSeconds(0.1f);
    //     Debug.Log(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
    // }
}
