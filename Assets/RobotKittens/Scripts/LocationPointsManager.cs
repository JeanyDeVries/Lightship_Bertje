using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Niantic.ARDK.Extensions.Gameboard;

public class WayPointsManager : MonoBehaviour
{
    [SerializeField]
    List<LocationPoint> waypoints;

    [SerializeField]
    TMP_Text textTargetLocation;

    [SerializeField]
    GameObject UI_Finished, UI_Game;

    [SerializeField]
    Button restartButton;

    [SerializeField]
    GameObject prefabArrow;

    [SerializeField]
    float radius;

    [HideInInspector] public enum wayPointsStatus
    {
        startScavengerHunt,
        scavengerHuntTime,
        finished
    }
    [HideInInspector] public wayPointsStatus wayPointStatus;


    private GameObject targetLocationObject;
    private GameObject lastTargetLocationObject = null;
    private GameObject arrowObj;
    private float arrowRotationSpeed = 15;
    private List<LocationPoint> allWayPoints = new List<LocationPoint>();

    public void Start()
    {
        foreach (var waypoint in waypoints)
        {
            allWayPoints.Add(waypoint);
        }

        arrowObj = Instantiate(prefabArrow);
        arrowObj.transform.parent = this.transform;

        restartButton.onClick.AddListener(delegate { wayPointStatus = wayPointsStatus.startScavengerHunt; });

        wayPointStatus = wayPointsStatus.startScavengerHunt;
    }

    // Update is called once per frame
    void Update()
    {
        switch (wayPointStatus)
        {
            case wayPointsStatus.startScavengerHunt:
                if(waypoints.Count <= 0) {
                    foreach (var waypoint in allWayPoints)
                    {
                        waypoints.Add(waypoint);
                    }
                }
                AssignTargetLocation();

                UI_Game.SetActive(true);
                UI_Finished.SetActive(false);
                arrowObj.SetActive(true);

                wayPointStatus = wayPointsStatus.scavengerHuntTime;
                break;            
            case wayPointsStatus.scavengerHuntTime:
                if (targetLocationObject) CheckCollision();
                arrowObj.transform.Rotate(Time.deltaTime * arrowRotationSpeed, 0.0f, 0.0f, Space.Self);
                break;            
            case wayPointsStatus.finished:

                UI_Game.SetActive(false);
                UI_Finished.SetActive(true);
                arrowObj.SetActive(false);
                break;

        }
    }

    // check collision with bertje
    private void CheckCollision()
    {
        Collider[] hitColliders = Physics.OverlapSphere(targetLocationObject.transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.tag == "Bertje")
            {
                Debug.Log("succeeded to go to location");
                lastTargetLocationObject = targetLocationObject;
                if (waypoints.Count > 0) AssignTargetLocation();
                else wayPointStatus = wayPointsStatus.finished;
            }
        }
    }

    [ExecuteInEditMode]
    private void OnDrawGizmos()
    {
        foreach (var child in waypoints)
        {
            // Draw a blue sphere at the transform's position
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(child.gameObject.transform.position, radius);
        }
    }

    private void AssignTargetLocation()
    {
        Debug.Log("waypoints " + waypoints.Count);

        //Set a random target location
        int randomNumber = Random.Range(0, waypoints.Count);
        LocationPoint targetLocation = waypoints[randomNumber];
        targetLocation.isTargetLocation = true;
        targetLocationObject = targetLocation.gameObject;
        if (targetLocationObject == lastTargetLocationObject)
        {
            Debug.Log("assign again");
            AssignTargetLocation(); //maybe rewrite this later, can be a bit heavy when unlucky
            return;
        }
        waypoints.Remove(targetLocation);

        arrowObj.transform.position = new Vector3(targetLocationObject.transform.position.x, targetLocationObject.transform.position.y + 1, targetLocationObject.transform.position.z);

        textTargetLocation.text = "Go to " + targetLocation.nameLocation;
    }
}
