using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;



public class LocationPointsManager : MonoBehaviour
{
    [SerializeField]
    List<LocationPoint> children;

    [SerializeField]
    TMP_Text textTargetLocation;

    [SerializeField]
    GameObject prefabArrow;

    [SerializeField]
    float radius;

    private GameObject targetLocationObject;
    private GameObject lastTargetLocationObject = null;
    private GameObject arrowObj;
    private float arrowRotationSpeed = 15;

    public void Start()
    {
        arrowObj = Instantiate(prefabArrow);
        arrowObj.transform.parent = this.transform;

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

        arrowObj.transform.Rotate(Time.deltaTime * arrowRotationSpeed, 0.0f, 0.0f, Space.Self);
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

        arrowObj.transform.position = new Vector3(targetLocationObject.transform.position.x, targetLocationObject.transform.position.y + 1, targetLocationObject.transform.position.z);

        textTargetLocation.text = "Go to " + targetLocation.nameLocation;
    }
}
