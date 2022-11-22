using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamThirdPersonFollow : MonoBehaviour
{
    public float CameraSens = 120.0f;
    public GameObject CameraFollowObj;
    Vector3 FollowPOS;
    public float clampAngle = 80.0f;
    public float inputSensitivity = 150.0f;
    public GameObject PlayerObj;
    public float camDistanceXToPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
