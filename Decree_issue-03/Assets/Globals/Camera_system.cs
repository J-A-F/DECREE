using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_system : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Some code form Brackeys's video about first person in Unity
        //link: https://www.youtube.com/watch?v=_QajrabyTJc
        //Idk how it works
        //especially this code below
        //but hey, we're gonna do it without overworking, right?

        xRotation = xRotation - mouseY;
        xRotation = Mathf.Clamp(xRotation, -89f , 89f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
