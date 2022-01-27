using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//THE SCRIPT IS NOT COMPLETED YET.

//Protip: you can find the whole documentation of the Input package here: https://docs.unity3d.com/ScriptReference/Input.html

public class Player_Movement : MonoBehaviour
{
    
    private Movement_Mapping moveMap;
    private CharacterController chController;
    private PlayersGravityController gravController;
    // private PlayersSmartColllisionHandler collisionHandler; IT ISN'T COMPLETE YET.
    private Transform trans;
    public float walkSpeed = 1f; //DON'T CHANGE IT, CHANGE WALKSPEEDREGULAR!;
    private float autoJumpHeight = 0f;

    [Header("Constant")]
    public float walkSpeedRegular = 1.0f; //0.25 = REALLY SLOW | 0.5 SLOW | 1.0 NORMAL | 1.25 FAST | 1.5 REALLY FAST
    public bool isMoving = false; //Helps to determine whether the player is moving or not.
    public bool isRunning = false; //Helps to determine whether the player is running or not.

    [Header("Info")]
    public string currentState = ""; //This strings represents current state of the player (moving / running / jumping / doubleJumping / falling), it will be really usefull for the animation system in the future.

    private void Awake()  {
        moveMap = gameObject.GetComponent<Movement_Mapping>(); //Protip: this is how to get a script from the GameObject
        chController = gameObject.GetComponent<CharacterController>();
        trans = gameObject.GetComponent<Transform>();
        gravController = gameObject.GetComponent<PlayersGravityController>();
        // collisionHandler = gameObject.GetComponent<PlayersSmartColllisionHandler>();
    }

    private void Update()  {

        // if (collisionHandler.canAutoJump == true) {
        //     autoJumpHeight += 0.01f;
        // }
        // else {
        //     autoJumpHeight = 0;
        // }

        if (Input.GetKey(moveMap.run.ToLower()) == true) {
            if (gravController.isGrounded == true && Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
                walkSpeed = walkSpeedRegular * 1.5f;
                isRunning = true;
            }
            else {
                walkSpeed = walkSpeedRegular * 1.25f;
            }
        }
        else {
            walkSpeed = walkSpeedRegular;
            isRunning = false;
        }

        if (chController.isGrounded == false) {
            chController.Move(trans.TransformDirection(new Vector3(Input.GetAxis("Horizontal") / 10f * walkSpeed, 0 + autoJumpHeight, Input.GetAxis("Vertical") / 10f * walkSpeed)));
            isMoving = true;
        }
        else {
            chController.Move(trans.TransformDirection(new Vector3(Input.GetAxis("Horizontal") / 15f * walkSpeed, 0 + autoJumpHeight, Input.GetAxis("Vertical") / 15f * walkSpeed)));
        }

        gravController.ExecuteGravityForces(); //To change gravity settings (and jump force) change PlayersGravityController component.

        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0) {
            isMoving = false;
        }

        if (Input.GetKeyDown(moveMap.jump.ToLower())) {
            gravController.Jump();
        }

        if (gravController.isJumping == true) {
            currentState = "jumping";
        }
        else if (gravController.isDoubleJumping == true) {
            currentState = "double jumping";
        }
        else if (gravController.isJumping == false && gravController.isDoubleJumping == false && gravController.isGrounded == false && gravController.gravityVelocity != 0) {
            currentState = "falling";
        }
        else if (isRunning == true) {
            currentState = "running";
        }
        else if (isMoving == true) {
            currentState = "moving";
        }
        else {
            currentState = "idle";
        }
    }

    public void EndAutoJump(float height) {
        // chController.Move(trans.TransformDirection(new Vector3(Input.GetAxis("Horizontal") / 15f * walkSpeed, height, Input.GetAxis("Vertical") / 15f * walkSpeed)));
    }
}
