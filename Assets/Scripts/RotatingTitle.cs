using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingTitle : MonoBehaviour
{
    // Speed of rotation in degrees per second
    public float rotationSpeed = 45.0f;

    void Update()
    {
        // Rotate the object around its local Y axis at the specified speed
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
