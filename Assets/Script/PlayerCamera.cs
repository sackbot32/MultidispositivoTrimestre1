using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerCamera : MonoBehaviour
{
    public InputActionAsset map;
    public float sensitivityY;
    public float sensitivityX;
    float mouseX, mouseY;
    float xRotation;
    private Transform currentCam;
    public Transform firstPerson;
    public Transform thirdPerson;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        firstPerson.GetComponent<CinemachineVirtualCamera>().Priority = 5;
        thirdPerson.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        currentCam = firstPerson;
    }

    public void RecieveInput()
    {
        mouseX = map.FindAction("MouseX").ReadValue<float>() * sensitivityX;
        mouseY = map.FindAction("MouseY").ReadValue<float>() * sensitivityY;
    }

    private void changeCam()
    {
        if(currentCam == firstPerson)
        {
            firstPerson.GetComponent<CinemachineVirtualCamera>().Priority = 0;
            thirdPerson.GetComponent<CinemachineVirtualCamera>().Priority = 5;
            currentCam = thirdPerson;
        }
        else
        {
            firstPerson.GetComponent<CinemachineVirtualCamera>().Priority = 5;
            thirdPerson.GetComponent<CinemachineVirtualCamera>().Priority = 0;
            currentCam = firstPerson;
        }
    }

    private void Update()
    {
        RecieveInput();
        transform.Rotate(Vector3.up * mouseX * Time.deltaTime);
        firstPerson.Rotate(Vector3.right * -mouseY * Time.deltaTime);

        if (map.FindAction("ChangeView").WasPressedThisFrame())
        {
            changeCam();
        }

    }

}
