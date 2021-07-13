using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTags {

    public const string MOVEMENT = "Movement";
    public const string MOVEMENT_GUARD = "MovementGuard";
    public const string WALK_VELOCITY = "PlayerWalkVelocity";
    public const string VERTICAL_VELOCITY = "PlayerVerticalVelocity";
    

    public const string JUMP_TRIGGER = "PlayerJump";

    public const string PUNCH_1_TRIGGER = "Punch1";
    public const string PUNCH_2_TRIGGER = "Punch2";
    public const string PUNCH_3_TRIGGER = "Punch3";

    public const string KICK_1_TRIGGER = "Kick1";
    public const string KICK_2_TRIGGER = "Kick2";
    public const string KICK_JUMP_TRIGGER = "PlayerJumpKick";

    public const string ATTACK_1_TRIGGER = "Attack1";
    public const string ATTACK_2_TRIGGER = "Attack2";
    public const string ATTACK_3_TRIGGER = "Attack3";

    public const string IDLE_ANIMATION = "Idle";

    public const string KNOCK_DOWN_TRIGGER = "KnockDown";
    public const string STAND_UP_TRIGGER = "StandUp";
    public const string HEAD_HIT_TRIGGER = "HeadHit";
    public const string MEDIUM_HIT_TRIGGER = "MediumHit";
    public const string DEATH_TRIGGER = "Death";
 
}

public class Axis {

    public const string HORIZONTAL_AXIS = "Horizontal";
    public const string VERTICAL_AXIS = "Vertical";  
}

public class Tags {

    public const string IS_GROUNDED_TAG = "IsGrounded";  
    public const string PLAYER_TAG = "Player";  
    public const string ENEMY_TAG = "Enemy"; 

    // public const string NORMAL_HIT_TAG = "NormalHit";
    // public const string KNOCKDOWN_HIT_TAG = "KnockDownHit";
    // public const string UNTAGGED_TAG = "Untagged";
    public const string PUNCH_1_TAG = "Punch1Tag";
    public const string PUNCH_2_TAG = "Punch2Tag";
    public const string PUNCH_3_TAG = "Punch3Tag";

    public const string KICK_1_TAG = "Kick1Tag";
    public const string KICK_2_TAG = "Kick2Tag";
    public const string KICK_JUMP_TAG = "PlayerJumpKickTag";

    public const string MAIN_CAMERA_TAG = "MainCamera";
    public const string HEALTH_UI_TAG = "HealthUI";


}
