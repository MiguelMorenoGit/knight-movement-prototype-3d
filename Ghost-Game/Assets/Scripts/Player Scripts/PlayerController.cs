using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    //Variables movimiento
    public float horizontalMove;
    public float verticalMove;

    private Vector3 playerInput;

    public CharacterController player;
    public float playerWalkSpeed;
    public float playerRunSpeed;
    public float playerSpeed;
    public float gravity = 9.8f;
    public float fallVelocity;
    public float jumpForce;
    public bool playerCanMove = true;
    public bool playerIsRunning = false;
    public bool dustRunEffectIsActive = false;
    public string mobility = "2D";
    
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
    private SFX_Playing SFX_Play;
    public string clipName;

    //Variables Animacion
    public Animator playerAnimatorController;

    //Instancias Effectos
    public ParticleSystem DustJumpEffect; 
    public ParticleSystem DustRunEffect; 

    // Start is called before the first frame update
    void Start() {
        player = GetComponent<CharacterController>();
        SFX_Play = GetComponent<SFX_Playing>();
        playerAnimatorController = GetComponentInChildren<Animator>();
        playerSpeed = playerWalkSpeed;
    }

    // Update is called once per frame
    void Update() {

        horizontalMove = Input.GetAxis(Axis.HORIZONTAL_AXIS);
        verticalMove = Input.GetAxis(Axis.VERTICAL_AXIS);

        // Set speed for Walk or Run
        setPlayerSpeed();

        if(mobility == "2D") {
            // Jugabilidad 2D
            playerInput = new Vector3(horizontalMove,0,0); // los almacenamos en Vector3

        } else {

            // Jugabilidad 3D
            playerInput = new Vector3(horizontalMove,0,verticalMove); // los almacenamos en Vector3
        }
        
        playerInput = Vector3.ClampMagnitude(playerInput, 1); // Y limitamos su magnitud a 1 para evitar acelerones en movimientos diagonales

        playerAnimatorController.SetFloat(AnimationParameters.WALK_VELOCITY, playerInput.magnitude * playerSpeed);

        Debug.Log(playerInput.magnitude);
        //Decimos en que direccion mirara el personaje
        CamDirection();

        // almacenamos en moveplayer el vector de movimiento corregido con respecto a posicion de camara
        movePlayer = playerInput.x * camRight + playerInput.z * camForward; 

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
        // direction = player.transform.position + new Vector3(0,0, horizontalMove); //classic 2d movement
        // player.transform.LookAt(direction);

        SetGravity();  //Iniciamos Gravedad
        PlayerSkills(); //Iniciamos las skills
        PlayerMobility(); //Iniciamos la jugabilidad entre 3D y 2D
        PlayerEffects(); // Iniciamos los effectos que afectan al player

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
    public void PlayerMobility() {

        //Si estamos tocando el suelo y pulsamos el boton "Jump"
        if (player.isGrounded && Input.GetKeyDown(KeyCode.F1)) {

            if (mobility == "2D") mobility = "3D"; 
            else  mobility = "2D";
        }

    }

    public void setPlayerSpeed() {

        if (Input.GetKey(KeyCode.LeftShift) && player.isGrounded ) {

            if(playerSpeed < playerRunSpeed) playerSpeed = playerSpeed + 0.2f > playerRunSpeed ? playerRunSpeed : playerSpeed + 0.2f ;
            
            playerIsRunning = true;
        }

        if (!Input.GetKey(KeyCode.LeftShift) && player.isGrounded ) {

            if(playerSpeed > playerWalkSpeed) playerSpeed =  playerSpeed - 0.2f < playerWalkSpeed ? playerWalkSpeed : playerSpeed - 0.2f;
            
            playerIsRunning = false;
        }
    }


    //Funcion para las habilidades de nuestro jugador
    public void PlayerSkills() {

        //Si estamos tocando el suelo y pulsamos el boton "Jump"
        if (player.isGrounded && Input.GetKeyDown(KeyCode.Space)) {

            fallVelocity = jumpForce;
            movePlayer.y = fallVelocity;

            playerAnimatorController.SetTrigger(AnimationParameters.JUMP_TRIGGER);
            //añadimos el efecto de dustJump
            ActiveDustJumpEffect();
        }

        //comprobamos si esta corriendo

    }

    //Funcion para Activar FX and SFX asociados al player
    public void PlayerEffects() {

        // Jump
        if(player.isGrounded && Input.GetKeyDown(KeyCode.Space)) {

            // FX - Dust Jump
            ActiveDustJumpEffect();
            // SFX - Jump
            SFX_Play.PlayJump();
            //SFX_Play.PlayLaunchSpear();
        }

        // FX - Dust Run
        if(player.isGrounded && Input.GetKey(KeyCode.LeftShift) && playerSpeed == playerRunSpeed ) ActiveDustRunEffect();
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
            playerAnimatorController.SetFloat(AnimationParameters.VERTICAL_VELOCITY, player.velocity.y);
        }

        // SlideDown(); T0DO hacer que funcione bien cuando estas frente a un angulo recto(pared,escalones etc)
        playerAnimatorController.SetBool(Tags.IS_GROUNDED_TAG, player.isGrounded);
    }

    private void CheckPlayerCanMove() {

        currentAnimation = playerAnimatorController.GetCurrentAnimatorClipInfo(0);
        clipName = currentAnimation[0].clip.name;

        if(
            clipName.Trim() == AnimationNames.STANDARD_WALK || 
            clipName.Trim() == AnimationNames.STANDARD_RUN || 
            clipName.Trim() == AnimationNames.IDLE_STANDARD || 
            clipName.Trim() == AnimationNames.IDLE || 
            clipName.Trim() == "WALK" 
        
        ) {

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

    private void ActiveDustJumpEffect(){

        ParticleSystem DustJump = Instantiate(DustJumpEffect, player.transform.position + new Vector3(0,0.1f,0), player.transform.rotation);
        DustJump.Play();
        

    }

    private void ActiveDustRunEffect(){

        if( !dustRunEffectIsActive ) {
            ParticleSystem DustRun = Instantiate(DustRunEffect, player.transform.position + new Vector3(0,0.25f,0) + player.transform.forward * -1f, player.transform.rotation);
            DustRun.Play();
            
            StartCoroutine(RunEffectTime());
        } 
 
    }

    IEnumerator RunEffectTime() {
        
        dustRunEffectIsActive = true;
        yield return new WaitForSeconds(Random.Range(0.1f,0.3f));
        dustRunEffectIsActive = false;
        
    }



}
