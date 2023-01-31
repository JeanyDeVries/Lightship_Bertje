using Niantic.ARDK.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public bool collision = false;
    [SerializeField]
    private float rotationSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bertje")
        {
            collision = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Bertje")
        {
            collision = true;
        }
    }

    private void Update()
    {
        this.transform.Rotate(0f, Time.deltaTime * rotationSpeed, 0.0f, Space.Self);
    }
}
    