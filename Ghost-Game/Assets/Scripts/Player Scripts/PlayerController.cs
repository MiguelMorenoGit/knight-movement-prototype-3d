using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    // Variables publicas
    public string clipName;
    public float horizontalMove;
    public float verticalMove;
    public float playerWalkSpeed;
    public float playerRunSpeed;
    public float playerSpeed;
    public float gravity = 9.8f;
    public float fallVelocity;
    public float fallMultiplier = 1;
    public float jumpForce;
    public float doubleJumpForce;
    public bool playerCanMove = true;
    public bool playerIsRunning = false;
    public bool playerIsDoubleJump = false;
    public bool playerIsJumping = false;
    public bool playerIsFalling = false;
    public float coyoteTime = 2f;
    public bool coyoteTimeIsActive = false;
    public bool coyoteTimeCanBeActivated = true;
    public bool dustRunEffectIsActive = false;
    public string mobility = "2D";

    //Variables privadas
    private Vector3 playerInput;
    private Vector3 camForward;
    private Vector3 camRight;
    private Vector3 movePlayer;
    private Vector3 direction;

    //Variables fisicas - TODO solucionar el tema pendientes y activar
    // public bool isOnSlope = false;
    // private Vector3 hitNormal;
    // public float slideVelocity;
    // public float slopeForceDown;

    //Instancias publicas
    public CharacterController player;
    public Animator playerAnimatorController;
    public Camera mainCamera;
    public ParticleSystem DustJumpEffect; 
    public ParticleSystem DustRunEffect; 

    //Instancias privadas
    private AnimatorClipInfo[] currentAnimation;
    private SFX_Playing SFX_Play;

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
        
        // SlideDown();
        // Debug.Log(playerInput.magnitude);
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
        PlayerEffects(); // Iniciamos los effectos que afectan al player
        PlayerSkills(); //Iniciamos las skills
        PlayerGamePlayDimension(); //Iniciamos la jugabilidad entre 3D y 2D

        player.Move(movePlayer * Time.deltaTime); // iniciamos el movimiento del player

        // Debug.Log(player.velocity.magnitude);
        // Debug.Log(direction);
        
        
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
    public void PlayerGamePlayDimension() {

        //Si estamos tocando el suelo y pulsamos el boton "Jump"
        if (player.isGrounded && Input.GetKeyDown(KeyCode.F1)) {

            if (mobility == "2D") mobility = "3D"; 
            else  mobility = "2D";
        }

    }

    public void setPlayerSpeed() {
        // RUNNING
        // if (Input.GetKey(KeyCode.LeftShift) && !playerIsFalling && !playerIsDoubleJump && (fallVelocity >= -5) ) {
        if (Input.GetKey(KeyCode.LeftShift) && (player.isGrounded || coyoteTimeIsActive) ) {

            playerIsRunning = true;

            if(playerSpeed < playerRunSpeed) playerSpeed = playerSpeed + 0.2f > playerRunSpeed ? playerRunSpeed : playerSpeed + 0.2f ;  
        }
        // WALKING
        // if (!Input.GetKey(KeyCode.LeftShift) && !playerIsFalling && !playerIsDoubleJump && (fallVelocity >= -5) ) {
        if (!Input.GetKey(KeyCode.LeftShift) && (player.isGrounded || coyoteTimeIsActive)) {
            playerIsRunning = false;

            if(playerSpeed > playerWalkSpeed) playerSpeed =  playerSpeed - 0.2f < playerWalkSpeed ? playerWalkSpeed : playerSpeed - 0.2f; 
        }
        // FALLING FROM PLATFORM
        if (playerIsFalling && (fallVelocity < 0) && !player.isGrounded && !coyoteTimeIsActive ) {
            playerIsFalling = true;

            if(playerSpeed > playerWalkSpeed) playerSpeed =  playerSpeed - 1.5f;
            
        }
        // FALLING FROM JUMP
        if ((playerIsJumping || playerIsDoubleJump) && (fallVelocity < -5) && !player.isGrounded && !coyoteTimeIsActive ) {
            
            if(playerSpeed > playerWalkSpeed) playerSpeed =  playerSpeed - 0.6f;
            
        }
    }


    //Funcion para las habilidades de nuestro jugador
    public void PlayerSkills() {

        //JUMP from ground
        if (Input.GetKeyDown(KeyCode.Space) && (player.isGrounded || coyoteTimeIsActive)) {

            playerIsJumping = true;
            fallVelocity = jumpForce;
            movePlayer.y = fallVelocity;

            playerAnimatorController.SetTrigger(AnimationParameters.JUMP_TRIGGER);

            if(coyoteTimeIsActive) {

                coyoteTimeIsActive = false;
                playerIsFalling = false;
                playerIsJumping = true;
            }
        }
        //JUMP FRON COYOTETIME
        else if (!player.isGrounded && playerIsFalling && !coyoteTimeIsActive && coyoteTimeCanBeActivated) {
            ActiveCoyoteTime();
        }
        //DOUBLE JUMP from air
        else if (!player.isGrounded && Input.GetKeyDown(KeyCode.Space) && !playerIsDoubleJump && !playerIsFalling && playerIsJumping) {

            playerIsDoubleJump = true;
            fallVelocity = doubleJumpForce;
            movePlayer.y = fallVelocity;

            playerAnimatorController.SetTrigger(AnimationParameters.DOUBLE_JUMP_TRIGGER);
        }
        //FALLING FROM PLATFORM
        else if (!playerIsJumping && (fallVelocity < 0) && !player.isGrounded ) {
            playerIsFalling = true;
            playerIsDoubleJump = false;
            playerIsJumping = false;
        }
        //HIT UNDER PLATFORM WHEN PLAYER IS JUMPING
        else if(!player.isGrounded && playerIsJumping && (fallVelocity > 0) && (player.velocity.y == 0f)) {
            Debug.Log("Debug.Log: colission under platform");
            print("print: colission under platform " );
            print("print: colission under platform " );
            fallVelocity = 0f;
        }
        //GROUNDED
        else if (player.isGrounded) {
            playerIsDoubleJump = false;
            playerIsJumping = false;
            playerIsFalling = false;
            coyoteTimeCanBeActivated = true;
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
        }
        // DoubleJump
        if(!player.isGrounded && Input.GetKeyDown(KeyCode.Space) && !playerIsDoubleJump && !playerIsFalling && playerIsJumping) {
            // FX - Dust Jump
            ActiveDustJumpEffect();
            // SFX - Jump
            SFX_Play. PlayDoubleJump();
            
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

            //esta subiendo
            if(fallVelocity >= 0) {

                fallVelocity -= gravity * Time.deltaTime;
                movePlayer.y = fallVelocity;
            }

            //esta bajando
            else {

                fallVelocity -= gravity * fallMultiplier * Time.deltaTime;
                movePlayer.y = fallVelocity;
            }

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

    // angulo >= angulo maximo del characterController
    // public void SlideDown() { 
        
    //     isOnSlope = (Vector3.Angle(Vector3.up, hitNormal) >= player.slopeLimit) && (Vector3.Angle(Vector3.up, hitNormal) < 89f);
    //     Debug.Log("Vector3.Angle(Vector3.up, hitNormal) > slope limit" + Vector3.Angle(Vector3.up, hitNormal));
    //     Debug.Log("player.slopeLimit < 89 ?" + player.slopeLimit);
    //     Debug.Log("89f" + 89f);
       

    //     if (isOnSlope) {
    //         movePlayer.x += ((1f - hitNormal.y) * hitNormal.x) * slideVelocity;
    //         movePlayer.z += ((1f - hitNormal.y) * hitNormal.z) * slideVelocity;

    //         movePlayer.y += slopeForceDown;
    //     }
    // }

    // private void OnControllerColliderHit(ControllerColliderHit hit) {
    //     hitNormal = hit.normal;
    // }

    // private void OnAnimatorMove(){

    // }

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

    private void ActiveCoyoteTime(){
        StartCoroutine(coyoteTimeCoroutine());
    }

    IEnumerator coyoteTimeCoroutine() {
        
        coyoteTimeIsActive = true;
        coyoteTimeCanBeActivated = false;
        yield return new WaitForSeconds(coyoteTime);
        coyoteTimeIsActive = false;
        
    }

    private void OnCollisionEnter(Collision other) {
        Debug.Log("colission");
        print("We Hit the " + other.gameObject.name);
        print("the tag name is Stage ? " + gameObject.CompareTag(Tags.STAGE));
    }



}
