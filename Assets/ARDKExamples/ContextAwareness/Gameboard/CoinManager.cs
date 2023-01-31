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
        Debug.Log("collision");
        if(other.gameObject.tag == "Bertje")
        {
            Debug.Log("collision bertje");
            collision = true;
        }
    }

    private void Update()
    {
        this.transform.Rotate(0f, Time.deltaTime * rotationSpeed, 0.0f, Space.Self);
    }
}
    