using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour {

    private Animator anim;
    public Rigidbody Spear;

    void Awake() {

        anim = GetComponent<Animator>();
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
        // var rigibodyNewSpear = newSpear.GetComponent<Rigidbody>();

        Spear.transform.parent = null;
        Spear.isKinematic = false;

        var newSpear = Instantiate(Spear);
        newSpear.transform.SetParent(null);
        // var newSpearParent = newSpear.GetComponent<Transform>();
        // newSpear.transform.parent = null;
        // newSpear.transform.parent = null;
        // newSpear.transform.parent = null;
        // newSpear.transform.parent = null;
        // newSpear.transform.parent = null;

        // var newSpear = Instantiate(Spear);
        // var parent = Spear.GetComponentInParent<Transform>();
        
        // newSpear.transform.position = Spear.transform.position;
        // newSpear.transform.rotation = Spear.transform.rotation;
        // newSpear.transform.localScale = Spear.transform.localScale;
        // newSpear.transform.SetParent(parent);

        // Debug.Log(newSpear.transform.position);
        // Debug.Log(newSpear.transform.rotation);
        // Debug.Log(Spear.transform.position);
        // Debug.Log(Spear.transform.rotation);

        // Debug.Log(newSpear.transform.localPosition);
        // Debug.Log(Spear.transform.localPosition);
        
        // Debug.Log(newSpear.transform.localPosition);
        // Debug.Log(Spear.transform.localPosition);

        

        newSpear.isKinematic = false;
        // newSpear.transform.parent = null;
        newSpear.AddForce(transform.forward * 20, ForceMode.Impulse);

        // var newSpear = Instantiate(Spear);
        // Spear.isKinematic = false;
        // // Spear.transform.parent = null;
        // Spear.AddForce(transform.forward * 20, ForceMode.Impulse);

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
