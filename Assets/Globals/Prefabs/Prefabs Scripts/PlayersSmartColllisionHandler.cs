using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class handles collision control for the player. Made from scratch by TLF. THIS CLASS NEEDS CHARACTER CONTROLLER AND PLAYER MOVEMENT CONTROLLER TO WORK!!!!!!! 

public class PlayersSmartColllisionHandler : MonoBehaviour
{
    private Player_Movement movController;
    private CharacterController chController;

    [Header("Constant")]
    public Vector3 posAutoJumpDisabled = new Vector3(); //If the raycast hits the collider at this height level, autojump isn't allowed (the obstacle is too high).
    public Vector3 posAutoJumpLow = new Vector3(); //If the raycast hits the collider, autojump is allowed.
    public Vector3 posAutoJumpHigh = new Vector3(); //If the raycast hits the collider, autojump is allowed.
    public float raySize = 1.15f; //Ray size, try not to change it.
    
    [Header("Info")]
    public bool canAutoJump = false; //If the bool is true, the player can autoclimb on small objects.

    //We check the raycast at 3 levels: the highest one defines the max height of the object which player will be able to autoclimb/autojump. If the raycast doesn't hit the collider, we check on 2 height levels (because some object can be floating in air) if there is a collider, and if it is the player can autojump/autoclimb.

    private void Awake() {
        movController = gameObject.GetComponent<Player_Movement>();
    }

    // Update is called once per frame
    private void Update()
    {
        posAutoJumpDisabled = new Vector3(transform.position.x, transform.position.y - 0.75f, transform.position.z);
        posAutoJumpLow = new Vector3(transform.position.x, transform.position.y - 1.6f, transform.position.z);
        posAutoJumpHigh = new Vector3(transform.position.x, transform.position.y - 1.0f, transform.position.z);

        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 0.75f, transform.position.z), transform.TransformDirection(Vector3.forward) * 1.15f, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 0.75f, transform.position.z), transform.TransformDirection(Vector3.forward) * -1.15f, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 0.75f, transform.position.z), transform.TransformDirection(Vector3.right) * 1.15f, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 0.75f, transform.position.z), transform.TransformDirection(Vector3.right) * -1.15f, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 0.75f, transform.position.z), transform.TransformDirection(Vector3.right + Vector3.forward) * 1.15f, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 0.75f, transform.position.z), transform.TransformDirection(Vector3.right + Vector3.forward) * -1.15f, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 0.75f, transform.position.z), transform.TransformDirection(Vector3.left + Vector3.forward) * 1.15f, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 0.75f, transform.position.z), transform.TransformDirection(Vector3.left + Vector3.forward) * -1.15f, Color.red);

        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 1.6f, transform.position.z), transform.TransformDirection(Vector3.forward) * 1.15f, Color.green);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 1.6f, transform.position.z), transform.TransformDirection(Vector3.forward) * -1.15f, Color.green);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 1.6f, transform.position.z), transform.TransformDirection(Vector3.right) * 1.15f, Color.green);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 1.6f, transform.position.z), transform.TransformDirection(Vector3.right) * -1.15f, Color.green);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 1.6f, transform.position.z), transform.TransformDirection(Vector3.right + Vector3.forward) * 1.15f, Color.green);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 1.6f, transform.position.z), transform.TransformDirection(Vector3.right + Vector3.forward) * -1.15f, Color.green);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 1.6f, transform.position.z), transform.TransformDirection(Vector3.left + Vector3.forward) * 1.15f, Color.green);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 1.6f, transform.position.z), transform.TransformDirection(Vector3.left + Vector3.forward) * -1.15f, Color.green);

        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), transform.TransformDirection(Vector3.forward) * -1.15f, Color.blue);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), transform.TransformDirection(Vector3.right) * 1.15f, Color.blue);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), transform.TransformDirection(Vector3.right) * -1.15f, Color.blue);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), transform.TransformDirection(Vector3.forward) * 1.15f, Color.blue);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), transform.TransformDirection(Vector3.right + Vector3.forward) * 1.15f, Color.blue);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), transform.TransformDirection(Vector3.right + Vector3.forward) * -1.15f, Color.blue);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), transform.TransformDirection(Vector3.left + Vector3.forward) * 1.15f, Color.blue);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), transform.TransformDirection(Vector3.left + Vector3.forward) * -1.15f, Color.blue);

        if (CheckRaycastAutoJump("all") == true) {
            canAutoJump = true;
        }
        else if (CheckRaycastAutoJump("low") == true) {
            movController.EndAutoJump(posAutoJumpLow.y);
            if (CheckRaycastAutoJump("low") == false) {
                movController.EndAutoJump(posAutoJumpLow.y);
                canAutoJump = false;
            }
        }
        else {
            canAutoJump = false;
        }
    }

    //CheckRaycastAutoJump("all") -> check if the highest raycast doesn't collide with object and the second or third collides.
    //CheckRaycastAutoJump("low") -> check if the highest raycast and the medium doesn't collide with obejct and the lowest collides.
    public bool CheckRaycastAutoJump(string type) {
        //Be aware of this monster. :P
        switch(type) {
            case "all":
                if (
                movController.isMoving == true
                && Physics.Raycast(posAutoJumpDisabled, transform.TransformDirection(Vector3.forward), raySize) == false 
                && Physics.Raycast(posAutoJumpDisabled, transform.TransformDirection(Vector3.forward), -raySize) == false 
                && Physics.Raycast(posAutoJumpDisabled, transform.TransformDirection(Vector3.right), raySize) == false 
                && Physics.Raycast(posAutoJumpDisabled, transform.TransformDirection(Vector3.right), -raySize) == false
                && Physics.Raycast(posAutoJumpDisabled, transform.TransformDirection(Vector3.forward + Vector3.right), raySize) == false
                && Physics.Raycast(posAutoJumpDisabled, transform.TransformDirection(Vector3.forward + Vector3.right), -raySize) == false
                && Physics.Raycast(posAutoJumpDisabled, transform.TransformDirection(Vector3.forward + Vector3.left), raySize) == false
                && Physics.Raycast(posAutoJumpDisabled, transform.TransformDirection(Vector3.forward + Vector3.left), -raySize) == false
                //-----------------------------------------------------------------------------------------------------------------
                && Physics.Raycast(posAutoJumpLow, transform.TransformDirection(Vector3.forward), raySize) == true
                || Physics.Raycast(posAutoJumpLow, transform.TransformDirection(Vector3.forward), -raySize) == true
                || Physics.Raycast(posAutoJumpLow, transform.TransformDirection(Vector3.right), raySize) == true
                || Physics.Raycast(posAutoJumpLow, transform.TransformDirection(Vector3.right), -raySize) == true
                || Physics.Raycast(posAutoJumpLow, transform.TransformDirection(Vector3.right + Vector3.forward), raySize) == true
                || Physics.Raycast(posAutoJumpLow, transform.TransformDirection(Vector3.right + Vector3.forward), -raySize) == true
                || Physics.Raycast(posAutoJumpLow, transform.TransformDirection(Vector3.left + Vector3.forward), raySize) == true
                || Physics.Raycast(posAutoJumpLow, transform.TransformDirection(Vector3.left + Vector3.forward), -raySize) == true
                || Physics.Raycast(posAutoJumpHigh, transform.TransformDirection(Vector3.forward), raySize) == true
                || Physics.Raycast(posAutoJumpHigh, transform.TransformDirection(Vector3.forward), -raySize) == true
                || Physics.Raycast(posAutoJumpHigh, transform.TransformDirection(Vector3.right), raySize) == true
                || Physics.Raycast(posAutoJumpHigh, transform.TransformDirection(Vector3.right), -raySize) == true
                || Physics.Raycast(posAutoJumpHigh, transform.TransformDirection(Vector3.right + Vector3.forward), raySize) == true
                || Physics.Raycast(posAutoJumpHigh, transform.TransformDirection(Vector3.right + Vector3.forward), -raySize) == true
                || Physics.Raycast(posAutoJumpHigh, transform.TransformDirection(Vector3.left + Vector3.forward), raySize) == true
                || Physics.Raycast(posAutoJumpHigh, transform.TransformDirection(Vector3.left + Vector3.forward), -raySize) == true
                ) {
                    return true;
                }
                else {
                    return false;
                }
            case "low":
                if (
                movController.isMoving == true
                && Physics.Raycast(posAutoJumpDisabled, transform.TransformDirection(Vector3.forward), raySize) == false 
                && Physics.Raycast(posAutoJumpDisabled, transform.TransformDirection(Vector3.forward), -raySize) == false 
                && Physics.Raycast(posAutoJumpDisabled, transform.TransformDirection(Vector3.right), raySize) == false 
                && Physics.Raycast(posAutoJumpDisabled, transform.TransformDirection(Vector3.right), -raySize) == false
                && Physics.Raycast(posAutoJumpDisabled, transform.TransformDirection(Vector3.forward + Vector3.right), raySize) == false
                && Physics.Raycast(posAutoJumpDisabled, transform.TransformDirection(Vector3.forward + Vector3.right), -raySize) == false
                && Physics.Raycast(posAutoJumpDisabled, transform.TransformDirection(Vector3.forward + Vector3.left), raySize) == false
                && Physics.Raycast(posAutoJumpDisabled, transform.TransformDirection(Vector3.forward + Vector3.left), -raySize) == false
                && Physics.Raycast(posAutoJumpLow, transform.TransformDirection(Vector3.forward), raySize) == false
                && Physics.Raycast(posAutoJumpLow, transform.TransformDirection(Vector3.forward), -raySize) == false
                && Physics.Raycast(posAutoJumpLow, transform.TransformDirection(Vector3.right), raySize) == false
                && Physics.Raycast(posAutoJumpLow, transform.TransformDirection(Vector3.right), -raySize) == false
                && Physics.Raycast(posAutoJumpLow, transform.TransformDirection(Vector3.right + Vector3.forward), raySize) == false
                && Physics.Raycast(posAutoJumpLow, transform.TransformDirection(Vector3.right + Vector3.forward), -raySize) == false
                && Physics.Raycast(posAutoJumpLow, transform.TransformDirection(Vector3.left + Vector3.forward), raySize) == false
                && Physics.Raycast(posAutoJumpLow, transform.TransformDirection(Vector3.left + Vector3.forward), -raySize) == false
                //-----------------------------------------------------------------------------------------------------------------
                && Physics.Raycast(posAutoJumpHigh, transform.TransformDirection(Vector3.forward), raySize) == true
                || Physics.Raycast(posAutoJumpHigh, transform.TransformDirection(Vector3.forward), -raySize) == true
                || Physics.Raycast(posAutoJumpHigh, transform.TransformDirection(Vector3.right), raySize) == true
                || Physics.Raycast(posAutoJumpHigh, transform.TransformDirection(Vector3.right), -raySize) == true
                || Physics.Raycast(posAutoJumpHigh, transform.TransformDirection(Vector3.right + Vector3.forward), raySize) == true
                || Physics.Raycast(posAutoJumpHigh, transform.TransformDirection(Vector3.right + Vector3.forward), -raySize) == true
                || Physics.Raycast(posAutoJumpHigh, transform.TransformDirection(Vector3.left + Vector3.forward), raySize) == true
                || Physics.Raycast(posAutoJumpHigh, transform.TransformDirection(Vector3.left + Vector3.forward), -raySize) == true
                ) {
                    return true;
                }
                else {
                    return false;
                }
        }

        return false;
    }
}
