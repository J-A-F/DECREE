using Godot;
using System;

public class InteractionRaycast : Camera
{
    Godot.Collections.Dictionary ray;
    public override void _PhysicsProcess(float delta) {
        if (Input.IsActionJustPressed("interact")) {

            ray = GetWorld().DirectSpaceState.IntersectRay(this.GlobalTransform.origin, this.GlobalTransform.Xform(new Vector3(0, 0, -10)));

            if (ray.Count > 0 && ray["collider"] != null) {
                Godot.Spatial collider = (Godot.Spatial)ray["collider"];
                
                if (collider.IsInGroup("DoorHorizontal") || collider.IsInGroup("DoorVertical")) {
                    ChangeDoorState(collider);
                }

                GD.Print("Camera pos: " + this.GlobalTransform.origin + " Ray collision detection: " + ray["position"] +  " " + ray["collider"]);
            }
            else {
                GD.Print("Camera pos: " + this.GlobalTransform.origin + " Calculated end point: " + this.GlobalTransform.Xform(new Vector3(0, 0, -10)));
            }
        }    
    }

    private void ChangeDoorState(Godot.Spatial door) {
        dynamic doorInfo = GetNode(door.Owner.GetPath());

        AnimationPlayer animPlayer = door.GetParent().GetNode("AnimationPlayer") as AnimationPlayer;

        if (doorInfo.isOpened == false) {
            doorInfo.isOpened = true;
            
            animPlayer.Stop(false);
            animPlayer.Play(doorInfo.animOpenName);
        }
        else {
            doorInfo.isOpened = false;

            animPlayer.Stop(false);
            animPlayer.PlayBackwards(doorInfo.animOpenName);

        }
    }
}
