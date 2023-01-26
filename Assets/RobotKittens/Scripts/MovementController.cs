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

    private Rigidbody rigidbody;
    private JoystickManager joystickManager;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        joystickManager = GameObject.Find("ImgBGJoystick").GetComponent<JoystickManager>();

        Debug.Log("start");
    }

    private void FixedUpdate()
    {
        Debug.Log("joystick" + (joystickManager == null).ToString());
        Debug.Log("joystickRB" + (rigidbody == null).ToString());

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

        //Store user input as a movement vector
        //Vector3 inputMovement = new Vector3(inputX, 0, inputY);

        //Apply the movement vector to the current position, which is
        //multiplied by deltaTime and speed for a smooth MovePosition
        //rigidbody.MovePosition(transform.position + inputMovement * Time.deltaTime * movementSpeed);
    }
}
