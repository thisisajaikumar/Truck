using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset = new Vector3(0f, 5f, -10f);
    [SerializeField, Range(1, 10)] float smoothSpeed = 5f;
    private Vector3 velocity = Vector3.zero; 

    void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("No target set for the camera to follow.");
            return;
        }

        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed, Mathf.Infinity, Time.deltaTime);

        transform.LookAt(target);
    }
}
