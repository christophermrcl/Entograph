using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PathMovement : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 5f;
    public float slopeForce = 5f;
    public float slopeRayLength = 1.5f;
    private int currentWaypointIndex = 0;

    public GameObject finish;

    private GameObject PauseStateObj;
    private Pause PauseGet;

    public AudioSource step;
    void Start()
    {
        PauseStateObj = GameObject.FindGameObjectWithTag("Pause");
        PauseGet = PauseStateObj.GetComponent<Pause>();
    }
    void Update()
    {
        MoveTowardsWaypoint();
    }

    void MoveTowardsWaypoint()
    {
        if (currentWaypointIndex < waypoints.Length)
        {
            Vector3 moveDirection = waypoints[currentWaypointIndex].position - transform.position;
            moveDirection.Normalize();

            // Handle slope
            HandleSlope(moveDirection);

            // Move in the adjusted direction
            transform.Translate(moveDirection * speed * Time.deltaTime * PauseGet.isNotPaused, Space.World);

            // Check if reached waypoint
            if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
            {
                currentWaypointIndex++;
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            step.volume = 0;
            finish.SetActive(true);

            /*
            GameObject gameObjectB = GameObject.FindGameObjectWithTag("SceneManage");
            ChangeScene script = gameObjectB.GetComponent<ChangeScene>();
            script.LoadScene("Title");
            */
        }
    }

    void HandleSlope(Vector3 moveDirection)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, slopeRayLength))
        {
            float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);
            if (slopeAngle > 0 && slopeAngle <= 45) // If slope angle is within a reasonable range
            {
                float moveDistance = Mathf.Abs(hit.point.y - transform.position.y);
                float targetAngle = Mathf.Atan2(moveDistance, slopeRayLength) * Mathf.Rad2Deg;
                float angleDifference = targetAngle - slopeAngle;

                // Adjust movement direction to move along the slope
                Vector3 forwardDirection = Quaternion.AngleAxis(angleDifference, Vector3.Cross(hit.normal, Vector3.forward)) * Vector3.forward;
                moveDirection = forwardDirection;
            }
        }
    }
}