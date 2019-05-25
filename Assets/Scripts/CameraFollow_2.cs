using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow_2 : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;
    public float smoothSpeed = 0.4f;
    void LateUpdate()
    {
        //transform.position = target.transform.position + offset;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        //transform.LookAt(target);
    }
}
