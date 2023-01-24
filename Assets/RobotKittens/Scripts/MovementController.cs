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
    }

    private void Update()
    {
        float inputX = joystickManager.InputHorizontal();
        float inputY = joystickManager.InputVertical();

        //Store user input as a movement vector
        Vector3 inputMovement = new Vector3(inputX, 0, inputY);

        //Apply the movement vector to the current position, which is
        //multiplied by deltaTime and speed for a smooth MovePosition
        rigidbody.MovePosition(transform.position + inputMovement * Time.deltaTime * movementSpeed);
    }
}
