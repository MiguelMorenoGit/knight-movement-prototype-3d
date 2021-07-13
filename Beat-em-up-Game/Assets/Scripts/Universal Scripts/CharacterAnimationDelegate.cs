using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationDelegate : MonoBehaviour {

    public GameObject Punch_1_Attack_Point,Punch_2_Attack_Point,Punch_3_Attack_Point, 
    Kick_1_Attack_Point, Kick_2_Attack_Point, Jump_Kick_Attack_Point;

    public float stand_Up_Timer = 2f;

    private CharacterAnimation animationScript;

    private void Awake() {
        animationScript = GetComponent<CharacterAnimation>();
    }
   
    // PUNCHES
    void Punch_1_Attack_On() {
        Punch_1_Attack_Point.SetActive(true);
    }
    void Punch_1_Attack_Off() {
        if( Punch_1_Attack_Point.activeInHierarchy ){
            Punch_1_Attack_Point.SetActive(false);
        }
    }

    void Punch_2_Attack_On() {
        Punch_2_Attack_Point.SetActive(true);
    }
    void Punch_2_Attack_Off() {
        if( Punch_2_Attack_Point.activeInHierarchy ){
            Punch_2_Attack_Point.SetActive(false);
        }
    }

    void Punch_3_Attack_On() {
        Punch_3_Attack_Point.SetActive(true);
    }
    void Punch_3_Attack_Off() {
        if( Punch_3_Attack_Point.activeInHierarchy ){
            Punch_3_Attack_Point.SetActive(false);
        }
    }

    // KICKS
    void Kick_1_Attack_On() {
        Kick_1_Attack_Point.SetActive(true);
    }
    void Kick_1_Attack_Off() {
        if( Kick_1_Attack_Point.activeInHierarchy ){
            Kick_1_Attack_Point.SetActive(false);
        }
    }
    
    void Kick_2_Attack_On() {
        Kick_2_Attack_Point.SetActive(true);
    }
    void Kick_2_Attack_Off() {
        if( Kick_2_Attack_Point.activeInHierarchy ){
            Kick_2_Attack_Point.SetActive(false);
        }
    }

    void Jump_Kick_Attack_On() {
        Jump_Kick_Attack_Point.SetActive(true);
    }
    void Jump_Kick_Attack_Off() {
        if( Jump_Kick_Attack_Point.activeInHierarchy ){
            Jump_Kick_Attack_Point.SetActive(false);
        }
    }


    void Enemy_StandUp() {
        StartCoroutine(StandUpAfterTime());
    }

    IEnumerator StandUpAfterTime() {
        yield return new WaitForSeconds(stand_Up_Timer);
        animationScript.StandUp();
    }
}

