using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class handles gravity control for the player. Made from scratch by TLF. THIS CLASS NEEDS CHARACTER CONTROLLER TO WORK!!!!!!!

public class PlayersGravityController : MonoBehaviour
{
    private CharacterController chController; //The CharacterController the GameObject has to have.

    [Header("Constant")]
    public float gravity = 9.81f; //The gravity value, works like a gravity in the real life. Usefull when we want to change the gravity due to the level characteristics (e.g. it is in the space). Try to avoid gravity < 1 because it looks pretty bad.
    public float jumpForce = 10.0f; //The force of the jump, decrease to make player jump lower or increase to make higher jumps.
    public float doubleJumpForce = 4.0f; //The force of the double jump, decrease to make player jump lower or increase to make higher jumps.

    [Header("Info")]
    public float gravityVelocity = 0.0f; //The velocity of the object due to the gravity (if != 0 then you can check whether the player is falling). Read-only, don't change manually the value.
    public float jumpVelocity = 0.0f; //The velocity of the object during jump (if != 0 then you can check whether the player is jumping). Read-only, don't change manually the value.
    public float doubleJumpVelocity = 0.0f; //The velocity of the double jump (if !0 then you can check whether the player is double jumping). Read-only, don't change manually the value.
    public bool isGrounded = false; //Very usefull boolean to check whether the object is grounded or in air. Read-only, don't change manually the value.
    public bool isDoubleJumpAvalible = true; //Changes its state when we do a double jump. Read-only, don't change manually the value.
    public bool isJumping = false; //Helping flag which disables gravity during jump. Read-only, don't change manually the value.
    public bool isDoubleJumping = false; //Helping flag which prevents infinite jumping in air. Read-only, don't change manually the value.

    private void Awake() {
        chController = gameObject.GetComponent<CharacterController>();
        jumpVelocity = jumpForce;
        doubleJumpVelocity = doubleJumpForce;
    }

    private void Update() {
        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.up * -2.0f, 2.1f) == true) {
            isGrounded = true;
        }
        else {
            isGrounded = false;
        }

        if (gravityVelocity != 0 && isJumping == false && isDoubleJumping == false && isGrounded == true) {
            isDoubleJumpAvalible = true;
        }
    }

    public void ExecuteGravityForces() {
        //Jump.
        if (isJumping == true && isDoubleJumping == false) {
            jumpVelocity -= ((float)Math.Pow(jumpForce, 0.1f) * Time.deltaTime * 12);

            if (Physics.Raycast(transform.position, gameObject.transform.up * 2.0f, 2.35f) == true) {
                jumpVelocity = 0;
                isJumping = false;
            }

            if (jumpVelocity <= 0) {
                isJumping = false;
                gravityVelocity = 0.007f;
            }

            chController.Move(new Vector3(0, jumpVelocity * Time.deltaTime + 0.002f, 0));
            
        }
        //Gravity.
        else if (isGrounded == false && isJumping == false && isDoubleJumping == false) {
            
            gravityVelocity += ((float)Math.Pow(gravity, 2) * Time.deltaTime / 325);

            chController.Move(new Vector3(0, -gravityVelocity, 0));

        }
        //Double jump.
        else if (isGrounded == false && isDoubleJumping == true && isJumping == false) {
            jumpVelocity = 0;

            doubleJumpVelocity -= ((float)Math.Pow(jumpForce, 0.05f) * Time.deltaTime * 8);

            if (doubleJumpVelocity <= 0) {
                isDoubleJumping = false;
                gravityVelocity = 0.004f;
            }

            chController.Move(new Vector3(0, doubleJumpVelocity * Time.deltaTime * 6, 0));
        }
        //Reset all states.
        else {
            gravityVelocity = 0;
            jumpVelocity = jumpForce;
            doubleJumpVelocity = doubleJumpForce;
            isJumping = false;
        }
    }

    public void Jump() {
        if (isGrounded == true) {
            isJumping = true;
        }
        else if (isDoubleJumpAvalible == true) {
            isDoubleJumping = true;
            isJumping = false;
            isDoubleJumpAvalible = false;
        }
    }
}
