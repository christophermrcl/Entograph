using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectMovement : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 5f;
    public float rotationSpeed = 5f;
    public float slopeForce = 3f;
    public float slopeRayLength = 1.5f;
    public float waypointReachedThreshold = 0.1f; // Distance threshold for considering waypoint reached
    public float xOffset = 0f;
    public float yOffset = 0f;
    public float zOffset = 0f;
    private int currentWaypointIndex = 0;


    private GameObject PauseStateObj;
    private Pause PauseGet;

    private Rigidbody rb;

    void Start()
    {
        PauseStateObj = GameObject.FindGameObjectWithTag("Pause");
        PauseGet = PauseStateObj.GetComponent<Pause>();

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MoveTowardsWaypoint();
    }

    void MoveTowardsWaypoint()
    {
        if (waypoints.Length == 0)
            return;

        Vector3 direction = waypoints[currentWaypointIndex].position - transform.position;

        // Move along the direction
        transform.Translate(direction.normalized * speed * Time.deltaTime * PauseGet.isNotPaused, Space.World);

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Vector3 eulerRotation = targetRotation.eulerAngles;
        eulerRotation.y = eulerRotation.y + yOffset;
        eulerRotation.x = transform.rotation.x+xOffset;
        eulerRotation.z = transform.rotation.z+zOffset;
        Quaternion rotationY = Quaternion.Euler(eulerRotation);

        // Rotate towards the target using Quaternion.Slerp, only around Y-axis
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationY, rotationSpeed * Time.deltaTime);

        // Check for slope
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, slopeRayLength))
        {
            // Calculate slope angle
            float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);

            // Apply force to stay grounded on slopea
            if (slopeAngle > 0 && slopeAngle < 90)
            {
                float slopeForceMagnitude = Mathf.Lerp(slopeForce, 0, slopeAngle / 90f);
                rb.AddForce(Vector3.down * slopeForceMagnitude, ForceMode.Acceleration);
            }
        }

        // Check if reached waypoint
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < waypointReachedThreshold)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
    }
}