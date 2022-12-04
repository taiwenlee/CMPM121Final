using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighterPosition : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;

    public Vector3 offset;
    public float rotateSpeed;

    public float scale = 1f;
    void Update()
    {
        float rotateX = Input.GetAxis("Mouse X");
        offset = Quaternion.AngleAxis(rotateX * rotateSpeed, Vector3.up) * offset;
        transform.position = target.position + (offset * scale);
        transform.rotation = Quaternion.LookRotation(target.position - transform.position);
    }
}
