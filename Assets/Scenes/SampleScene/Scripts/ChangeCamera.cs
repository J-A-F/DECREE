using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    public Camera firstPersonView;
    public Camera staticCamera;
    public GameObject firstPersonViewParent;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1")) {
            firstPersonView.enabled = true;
            staticCamera.enabled = false;
        }
        else if (Input.GetKeyDown("2")) {
            firstPersonViewParent.transform.localRotation = Quaternion.Euler(0, 0, 0);
            firstPersonView.enabled = false;
            staticCamera.enabled = true;
        }
    }
}
