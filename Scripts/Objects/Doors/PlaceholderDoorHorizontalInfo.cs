using Godot;
using System;

public class PlaceholderDoorHorizontalInfo : Node
{
    //This flags determines whether the doors is opened or closed. Be careful - when isOpened == true doesn't mean that the door is fully open - it changes the state every click on the door so first x-seconds will be a animation.
    public bool isOpened = false;
    public string animOpenName = "DoorOpenHorizontal";

    //TODO: Add SFX when opening door.
}
