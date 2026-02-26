using UnityEngine;
using System.Collections;
public class VehicleController : MonoBehaviour
{
    public Transform[] wheels;
    float enginePower = 150.0f;
    float power = 0.0f;
    float brake = 0.0f;
    float steer = 0.0f;
    float maxSteer = 25.0f;
    public float centerOfMassX = 0f;
    public float centerOfMassY = -1.0f;
    public float centerOfMassZ = 0.3f;
    void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = new Vector3(centerOfMassX, centerOfMassY, centerOfMassZ);
    }
    void Update()
    {
        power = Input.GetAxis("Vertical") * enginePower * Time.deltaTime * 250.0f;
        steer = Input.GetAxis("Horizontal") * -maxSteer;
        brake = Input.GetKey(KeyCode.Space) ? GetComponent<Rigidbody>().mass * 1.0f : 0.0f;
        GetCollider(0).steerAngle = steer;
        GetCollider(1).steerAngle = steer;
        if (brake > 0.0)
        {
            GetCollider(0).brakeTorque = brake;
            GetCollider(1).brakeTorque = brake;
            GetCollider(2).brakeTorque = brake;
            GetCollider(3).brakeTorque = brake;
            GetCollider(2).motorTorque = 0.0f;
            GetCollider(3).motorTorque = 0.0f;
        }
        else
        {
            GetCollider(0).brakeTorque = 0f;
            GetCollider(1).brakeTorque = 0f;
            GetCollider(2).brakeTorque = 0f;
            GetCollider(3).brakeTorque = 0f;
            GetCollider(2).motorTorque = power;
            GetCollider(3).motorTorque = power;
        }
    }
    WheelCollider GetCollider(int n)
    {
        return wheels[n].gameObject.GetComponent<WheelCollider>();
    }
}