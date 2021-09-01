using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SFX_Playing : MonoBehaviour {

    public AudioSource Simple_Jump_1;
    public AudioSource Double_Jump_1;
    public AudioSource Launch_Spear_1;


    public void PlayJump() {
        Simple_Jump_1.Play();
    }

    public void PlayDoubleJump() {
        Double_Jump_1.Play();
    }

    public void PlayLaunchSpear() {
        Launch_Spear_1.Play();
    }


}