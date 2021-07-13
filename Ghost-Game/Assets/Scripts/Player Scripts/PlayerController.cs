using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    //Variables movimiento
    public float horizontalMove;
    public float verticalMove;

    private Vector3 playerInput;

    public CharacterController player;
    public float playerSpeed;
    public float gravity = 9.8f;
    public float fallVelocity;
    public float jumpForce;
    public bool playerCanMove = true;
    
    //Variables movimiento relativo a camara
    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;
    private Vector3 movePlayer;
    private Vector3 direction;

    //Variables fisicas - TODO solucionar el tema pendientes y activar
    // public bool isOnSlope = false;
    // private Vector3 hitNormal;
    // public float slideVelocity;
    // public float slopeForceDown;

    private AnimatorClipInfo[] currentAnimation;
    public string clipName;

    //Variables Animacion
    public Animator playerAnimatorController;

    // Start is called before the first frame update
    void Start() {
        player = GetComponent<CharacterController>();
        playerAnimatorController = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update() {

        horizontalMove = Input.GetAxis(Axis.HORIZONTAL_AXIS);
        verticalMove = Input.GetAxis(Axis.VERTICAL_AXIS);

        playerInput = new Vector3(horizontalMove,0,verticalMove); // los almacenamos en Vector3
        playerInput = Vector3.ClampMagnitude(playerInput, 1); // Y limitamos su magnitud a 1 para evitar acelerones en movimientos diagonales

        playerAnimatorController.SetFloat(AnimationTags.WALK_VELOCITY, playerInput.magnitude * playerSpeed);
        //Decimos en que direccion mirara el personaje
        CamDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camForward; // almacenamos en moveplayer el vector de movimiento corregido con respecto a posicion de camara

        // velocidad del player en funcion de si esta en el aire o no
        // comprobamos si el player esta en posicion de moverse

        CheckPlayerCanMove();

        if (player.isGrounded && playerCanMove){
            

            movePlayer = playerInput * playerSpeed;

        } else if (player.isGrounded && !playerCanMove) {

            movePlayer = playerInput * 0f;

        } else {

            movePlayer = playerInput * playerSpeed * 1.5f;
        }
        player.transform.LookAt(player.transform.position + movePlayer); // asignamos hacia donde va a mirar el jugador

        //classic 2d movement 
        //direction = player.transform.position + new Vector3(0,0, horizontalMove); //classic 2d movement
        //player.transform.LookAt(direction);

        SetGravity();  //Iniciamos Gravedad
        
        PLayerSkills(); //Iniciamos las skills

        player.Move(movePlayer * Time.deltaTime); // iniciamos el movimiento del player

        // Debug.Log(player.velocity.magnitude);
        // Debug.Log(direction);
        // Debug.Log(player.transform);
        
    }

    // Funcion camara
    void CamDirection() {

        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    //Funcion para las habilidades de nuestro jugador
    public void PLayerSkills() {

        //Si estamos tocando el suelo y pulsamos el boton "Jump"
        if (player.isGrounded && Input.GetKeyDown(KeyCode.Space)) {

            fallVelocity = jumpForce;
            movePlayer.y = fallVelocity;

            playerAnimatorController.SetTrigger(AnimationTags.JUMP_TRIGGER);
        }

    }

    //Funcion para la Gravedad
    public void SetGravity() {

        movePlayer.y = -gravity * Time.deltaTime;
        //Si estamos tocando el suelo - isGrounded es una funcion integrada en el controller
        if (player.isGrounded){

            fallVelocity = -gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;

        } else {

            fallVelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
            playerAnimatorController.SetFloat(AnimationTags.VERTICAL_VELOCITY, player.velocity.y);
        }

        // SlideDown(); T0DO hacer que funcione bien cuando estas frente a un angulo recto(pared,escalones etc)
        playerAnimatorController.SetBool(Tags.IS_GROUNDED_TAG, player.isGrounded);
    }

    private void CheckPlayerCanMove() {

        currentAnimation = playerAnimatorController.GetCurrentAnimatorClipInfo(0);
        clipName = currentAnimation[0].clip.name;

        if(clipName.Trim() == "Standard Walk" || clipName.Trim() == "Idle"){
            playerCanMove = true;

        } else {
            playerCanMove = false;
        }

    }

    // public void SlideDown() { 
    //     // angulo >= angulo maximo del characterController
    //     isOnSlope = Vector3.Angle(Vector3.up, hitNormal) >= player.slopeLimit && Vector3.Angle(Vector3.up, hitNormal) < 89f;

    //     if (isOnSlope) {
    //         movePlayer.x += ((1f - hitNormal.y) * hitNormal.x) * slideVelocity;
    //         movePlayer.z += ((1f - hitNormal.y) * hitNormal.z) * slideVelocity;

    //         movePlayer.y += slopeForceDown;
    //     }
    // }

    // private void OnControllerColliderHit(ControllerColliderHit hit) {
    //     hitNormal = hit.normal;
    // }

    private void OnAnimatorMove(){

    }



}
