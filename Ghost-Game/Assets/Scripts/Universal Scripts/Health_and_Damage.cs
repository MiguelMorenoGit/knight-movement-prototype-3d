using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_and_Damage : MonoBehaviour
{
    public int health = 100;
    public bool invencible = false;
    public float tiempo_invencible = 1f;
    public float tiempo_frenado = 0.5f;

    private Animator anim;
    private GameController game;

    private void Start() {
        anim = GetComponent<Animator>();
        game = FindObjectOfType <GameController>();
    }


    public void RestarVida(int damage)
    {
        if (!invencible && health > 0) 
        {
            health -= damage;
            anim.Play("DamageFromFloorObject");
            StartCoroutine(Invulnerabilidad());
            StartCoroutine(FrenarVelocidad());
            if (health == 0)
            {
                game.GameOver();
            }
        }
    }

    void GameOver()
    {
        Debug.Log("GAME OVER");
        Time.timeScale = 0;
    }

    IEnumerator Invulnerabilidad()
    {
        invencible = true;
        yield return new WaitForSeconds(tiempo_invencible);
        invencible = false;
    }

    IEnumerator FrenarVelocidad()
    {
        var velocidadActual = GetComponent<PlayerController>().playerSpeed;
        GetComponent<PlayerController>().playerSpeed = 0;
        yield return new WaitForSeconds(tiempo_frenado);
        GetComponent<PlayerController>().playerSpeed = velocidadActual;
    }
}
