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
       
    //     Rigidbody spearInstance;
    //     spearInstance = Instantiate(Spear, SpearLaunchPosition.transform.position, SpearLaunchPosition.transform.rotation);
    //     spearInstance.isKinematic = false;
    //     spearInstance.useGravity = false;
    //     spearInstance.transform.parent = null;
    //     spearInstance.AddForce(transform.forward * 25, ForceMode.Impulse);
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
        anim.Play(AnimationNames.IDLE);
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
