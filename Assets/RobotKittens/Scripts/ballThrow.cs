using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Niantic.ARDK.AR;
using Niantic.ARDK.AR.ARSessionEventArgs;

using Niantic.ARDK.AR.Configuration;

using Unity.Collections;

using Niantic.ARDK.AR.Mesh;

using Niantic.ARDK.Utilities;
using Niantic.ARDK.Utilities.Input.Legacy;

public class ballThrow : MonoBehaviour
{
    public GameObject _ball;
    public Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        _ball.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlatformAgnosticInput.touchCount <= 0) { return; }

        //if the user touches the screen enable the _ball
        var touch = PlatformAgnosticInput.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            _ball.SetActive(true);

            //get the rigidbody from teh _ball and reset it.
            Rigidbody rb = _ball.GetComponent<Rigidbody>();
            rb.velocity = new Vector3(0f, 0f, 0f);
            rb.angularVelocity = new Vector3(0f, 0f, 0f);

            //move the _ball to the a point infront of the camera.
            _ball.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
            _ball.transform.position = _camera.transform.position + _camera.transform.forward;

            //then add a force to the rigid body to launch it from teh camera.
            float force = 100.0f;
            rb.AddForce(_camera.transform.forward * force);

        }
    }
}
