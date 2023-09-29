using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckController : MonoBehaviour
{

    [Header("Controller Setting")]
    [SerializeField] float accleration = 500f;
    [SerializeField] float breakForce = 300f;
    [SerializeField, Range(0, 90)] float maxTurnAngle = 15f;

    [Header("Wheel Collider")]
    [SerializeField] WheelCollider frontRightCollider;
    [SerializeField] WheelCollider frontLeftCollider;
    [SerializeField] WheelCollider backRightCollider;
    [SerializeField] WheelCollider backtLeftCollider;

    [Header("Wheel Object")]
    [SerializeField] Transform frontRightObject;
    [SerializeField] Transform frontLeftObject;
    [SerializeField] Transform backRightObject;
    [SerializeField] Transform backtLeftObject;


    //Private Variables
    private float currentAccleration, currentBreakForce, currentTurnAngle;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        currentAccleration = accleration * Input.GetAxis("Vertical");

        if (Input.GetAxis("Jump") > 0)
        {
            currentBreakForce = breakForce;
        }
        else
        {
            currentBreakForce = 0f;
        }

        backtLeftCollider.motorTorque = currentAccleration;
        backRightCollider.motorTorque = currentAccleration;

        frontLeftCollider.brakeTorque = currentBreakForce;
        frontRightCollider.brakeTorque = currentBreakForce;
        backtLeftCollider.brakeTorque = currentBreakForce;
        backRightCollider.brakeTorque = currentBreakForce;


        currentTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal");
        frontLeftCollider.steerAngle = currentTurnAngle;
        frontRightCollider.steerAngle = currentTurnAngle;

        UpdateWheel(frontLeftCollider, frontLeftObject);
        UpdateWheel(frontRightCollider, frontRightObject);
        UpdateWheel(backtLeftCollider, backtLeftObject);
        UpdateWheel(backRightCollider, backRightObject);
    }

    void UpdateWheel(WheelCollider col, Transform trans)
    {
        Vector3 wheelPosition;
        Quaternion wheelRotation;
        col.GetWorldPose(out wheelPosition, out wheelRotation);

        trans.position = wheelPosition;
        trans.rotation = wheelRotation;
    }
}
