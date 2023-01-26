using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    WayPointsManager wayPointsManager;

    private Rigidbody rigidbody;
    private JoystickManager joystickManager;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        joystickManager = GameObject.Find("ImgBGJoystick").GetComponent<JoystickManager>();
    }

    private void FixedUpdate()
    {
        if (wayPointsManager.wayPointStatus == WayPointsManager.wayPointsStatus.finished)
        {
            //stop movement when game is finished
            joystickManager.Reset();
            return; 
        }

        float inputX = joystickManager.InputHorizontal();
        float inputY = joystickManager.InputVertical();


        Vector3 forwardCamera = Camera.main.transform.forward;
        Vector3 rightCamera = Camera.main.transform.right;
        forwardCamera.y = 0;
        rightCamera.y = 0;
        forwardCamera = forwardCamera.normalized;
        rightCamera = rightCamera.normalized;


        Vector3 verticalMovement = inputY * forwardCamera * movementSpeed;
        Vector3 horizontalMovement = inputX * rightCamera * movementSpeed;

        Vector3 cameraRelativeMovement = verticalMovement + horizontalMovement;
        this.transform.Translate(cameraRelativeMovement, Space.World);
    }
}
