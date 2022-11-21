using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;

    // Lets us edit the X,Y,Z of the camera position
    public Vector3 offset;

    public float minZoom;
    public float maxZoom;
    public float rotateSpeed;

    private float scale = 1f;

    void LateUpdate()
    {
        float zoom = Input.GetAxis("Mouse ScrollWheel");
        float rotateX = Input.GetAxis("Mouse X");
        float rotateY = Input.GetAxis("Mouse Y");

        // zoom limit
        scale -= zoom;
        if (scale < minZoom)
            scale = minZoom;
        if (scale > maxZoom)
            scale = maxZoom;

        // Allows camera rotation, Quaternion is for rotations
        offset = Quaternion.AngleAxis(rotateX * rotateSpeed, Vector3.up) * offset;
        offset = Quaternion.AngleAxis(rotateY * rotateSpeed * -1, Vector3.right) * offset;

        // This line moves the camera with the ball
        transform.position = target.position + (offset * scale);

        // This line makes camera focus on ball
        transform.rotation = Quaternion.LookRotation(target.position - transform.position);
    }
}
