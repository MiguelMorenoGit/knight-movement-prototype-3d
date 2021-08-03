using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterAnimation : MonoBehaviour {

    private Animator anim;
    public Rigidbody Spear;
    private GameObject SpearLaunchPosition;

    void Awake() {

        anim = GetComponent<Animator>();
        SpearLaunchPosition = GameObject.FindGameObjectsWithTag(Tags.SPEAR_LAUNCH_POSITION)[0];
    }

    // movements
    public void Walk(bool move) {
        anim.SetBool(AnimationParameters.MOVEMENT, move);
    }
    public void Walk_Guard(bool move) {
        anim.SetBool(AnimationParameters.MOVEMENT_GUARD, move);
    }

    // Punchs
    public void Punch_1() {
        anim.SetTrigger(AnimationParameters.PUNCH_1_TRIGGER);
    }
    
    public void Punch_2() {
        anim.SetTrigger(AnimationParameters.PUNCH_2_TRIGGER);
    }
    
    public void Punch_3() {
        anim.SetTrigger(AnimationParameters.PUNCH_3_TRIGGER);
    }

    // Kicks
    public void Kick_1() {
        anim.SetTrigger(AnimationParameters.KICK_1_TRIGGER);
    }
    public void Kick_2() {
        anim.SetTrigger(AnimationParameters.KICK_2_TRIGGER);
    }
    public void Kick_Jump() {
        
        anim.SetTrigger(AnimationParameters.KICK_JUMP_TRIGGER);
    }

    // Arms
    public void Throw_Spear_Animation_Start() {
        anim.SetTrigger(AnimationParameters.THROW_SPEAR);
    }
    public void Throw_Spear() {
       
        // Spear.isKinematic = false;
        // Spear.transform.parent = null;
        // Spear.AddForce(transform.forward * 20, ForceMode.Impulse);
  
        Rigidbody spearInstance;
        spearInstance = Instantiate(Spear, SpearLaunchPosition.transform.position, SpearLaunchPosition.transform.rotation);
        // spearInstance.GetComponent<Spear>().enabled = false;
        spearInstance.isKinematic = false;
        spearInstance.useGravity = false;
        // spearInstance.name = "Throwed_Spear";
        spearInstance.transform.parent = null;
        spearInstance.AddForce(transform.forward * 25, ForceMode.Impulse);
        // spearInstance.transform.SetParent(SpearLaunchPosition.transform.parent);
        // spearInstance.transform.localPosition = Vector3.zero;
        // spearInstance.transform.localRotation = Quaternion.Euler(Vector3.zero);
        // spearInstance.transform.localScale = Vector3.one;
        // spearInstance.transform.position = new Vector3(2,2,2);
        // Debug.Log("Spear position : " + Spear.transform.position);
        // Debug.Log("Spear rotation : " + Spear.transform.rotation);
        
       

    }

    //  ENEMY ANIMATIONS

    public void EnemyAttack(int attack) {
        if(attack == 0) {
            anim.SetTrigger(AnimationParameters.ATTACK_1_TRIGGER);
        }
        if(attack == 1) {
            anim.SetTrigger(AnimationParameters.ATTACK_2_TRIGGER);
        }
        if(attack == 2) {
            anim.SetTrigger(AnimationParameters.ATTACK_3_TRIGGER);
        }
    } // enemy attack

    public void Play_IdleAnimation() {
        anim.Play(AnimationParameters.IDLE_ANIMATION);
    }

    public void KnockDown() {
        anim.SetTrigger(AnimationParameters.KNOCK_DOWN_TRIGGER);
    }
    public void StandUp() {
        anim.SetTrigger(AnimationParameters.STAND_UP_TRIGGER);
    }

    public void HeadHit() {
        anim.SetTrigger(AnimationParameters.HEAD_HIT_TRIGGER);
    }
    public void MediumHit() {
        anim.SetTrigger(AnimationParameters.MEDIUM_HIT_TRIGGER);
    }
    public void Death() {
        anim.SetTrigger(AnimationParameters.DEATH_TRIGGER);
    }
    
    
}
