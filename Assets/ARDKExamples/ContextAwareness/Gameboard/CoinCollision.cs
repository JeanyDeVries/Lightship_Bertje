using Niantic.ARDK.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollision : MonoBehaviour
{
    public bool collision = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision");
        if(other.gameObject.tag == "Bertje")
        {
            Debug.Log("collision bertje");
            collision = true;
        }
    }
}
