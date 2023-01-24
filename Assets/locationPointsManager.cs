using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;


public class locationPointsManager : MonoBehaviour
{
    [SerializeField]
    private List<LocationPoint> children;

    [SerializeField]
    TMP_Text textTargetLocation; 

    [SerializeField]
    private float radius;

    private GameObject targetLocationObject;
    private GameObject lastTargetLocationObject = null;

    // Start is called before the first frame update
    void Start()
    {
        AssignTargetLocation();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetLocationObject)
        {
            Collider[] hitColliders = Physics.OverlapSphere(targetLocationObject.transform.position, radius);
            foreach (var hitCollider in hitColliders)
            {
                if(hitCollider.tag == "Bertje")
                {
                    Debug.Log("succeeded to go to location");
                    lastTargetLocationObject = targetLocationObject;
                    AssignTargetLocation();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        foreach (var child in children)
        {
            // Draw a blue sphere at the transform's position
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(child.gameObject.transform.position, radius);
        }
    }

    private void AssignTargetLocation()
    {
        //Set a random target location
        int randomNumber = Random.Range(0, children.Count);
        LocationPoint targetLocation = children[randomNumber];
        targetLocation.isTargetLocation = true;
        targetLocationObject = targetLocation.gameObject;
        if (targetLocationObject == lastTargetLocationObject)
        {
            Debug.Log("assign again");
            AssignTargetLocation(); //maybe rewrite this later, can be a bit heavy when unlucky
            return;
        }

        textTargetLocation.text = "Go to " + targetLocation.nameLocation;
    }
}
