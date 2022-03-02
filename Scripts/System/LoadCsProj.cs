using Godot;
using System;

public class LoadCsProj : Node
{
    //This scripts solves the problem about loading Microsoft.CSharp reference (which enables use of dynamic type) freeze. When we'll create Main Menu -> Main Scene loading screen, this script should be included there. For now, it is included in the main scene.
    //More about it here: https://godotengine.org/qa/71170/freeze-when-using-c%23-dynamic-type
    public override void _Ready() {
        dynamic dynamic = 1;

        GD.Print(dynamic);

        QueueFree();
    }

}
