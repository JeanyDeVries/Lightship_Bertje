using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private Button forwardsBtn, backwardsBtn, leftBtn, rightBtn;

    private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        backwardsBtn.GetOrAddComponent<ButtonEvents>().onHold.AddListener(MoveBackwards);
        forwardsBtn.GetOrAddComponent<ButtonEvents>().onHold.AddListener(MoveForward);
        leftBtn.GetOrAddComponent<ButtonEvents>().onHold.AddListener(MoveLeft);
        rightBtn.GetOrAddComponent<ButtonEvents>().onHold.AddListener(MoveRight);
    }

    void MoveBackwards()
    {
        Vector3 newTransform = new Vector3(0f, 0f, speed);
        rigidbody.MovePosition(transform.position + newTransform * Time.deltaTime);
    }
    void MoveForward()
    {
        Vector3 newTransform = new Vector3(0f, 0f, -speed);
        rigidbody.MovePosition(transform.position + newTransform * Time.deltaTime);
    }
    void MoveLeft()
    {
        Vector3 newTransform = new Vector3(-speed, 0f, 0f);
        rigidbody.MovePosition(transform.position + newTransform * Time.deltaTime);
    }
    void MoveRight()
    {
        Vector3 newTransform = new Vector3(speed, 0f, 0f);
        rigidbody.MovePosition(transform.position + newTransform * Time.deltaTime);
    }
}
