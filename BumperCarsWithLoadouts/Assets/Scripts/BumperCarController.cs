using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperCarController : MonoBehaviour
{
    // Wheel colliders
    public WheelCollider frontLeft;
    public WheelCollider backLeft;
    public WheelCollider frontRight;
    public WheelCollider backRight;

    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetJoystickNames().Length > 0)
        {
            // joystick plugged in so get inout from the controller
            GetInputJoystick();
        }
	}

    void GetInputJoystick()
    {
        float torque = maxMotorTorque * Input.GetAxis("Vertical_P1");
        float steerAngle = maxSteeringAngle * Input.GetAxis("Horizontal_P1");

        // turn the front wheels and apply the motor torque
        frontLeft.steerAngle = steerAngle;
        frontRight.steerAngle = steerAngle;
        frontLeft.motorTorque = torque;
        frontRight.motorTorque = torque;
    }
}
