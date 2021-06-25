using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventControllerEngine : MonoBehaviour
{
    //private Variables
    private List<Transform> nodes;
    private int currentNode = 0;
    //public variables
    public Transform path;

    public float maxMotorTorque = 100f;
    public float currentSpeed;
    public float maxSpeed = 100;
    public Vector3 centerOfMass;
    public bool isBraking = false;
    public float maxBrakeTorque = 150f;
    private float targetAngle = 0f;
    public bool atSignal = false;

    public bool avoiding = false;

    [Header("Wheels Customization")]
    public float maxSteerAngle = 45f;
    public float turnSpeed = 5f;
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public WheelCollider wheelBL;
    public WheelCollider wheelBR;

    [Header("Sensors")]
    public float sensorLength = 5f;
    public Vector3 frontSensorPos = new Vector3(0, 0, 2.22f);
    public float frontSideSensorPosition = 0.5f;
    public float frontSensorAngle = 30f;

    void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = centerOfMass;
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != path.transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SensorsFunc();
        ApplySteer();
        Drive();
        checkWaypointDistance();
        Braking();
        LerpToSteerAngle();
    }

    void SensorsFunc()
    {
        RaycastHit hit;
        Vector3 sensorStartPos = transform.position;
        sensorStartPos += transform.forward * frontSensorPos.z;
        sensorStartPos += transform.up * frontSensorPos.y;
        //float avoidMultiplier = 0;
        avoiding = false;

        //front center sensor
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
        {
            //if (hit.collider.CompareTag("Car"))
            if (hit.collider.tag == this.tag)
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                avoiding = true;
                /*if (hit.normal.x < 0)
                {
                    avoidMultiplier = -1;
                }
                else
                    avoidMultiplier = 1;*/
            }

        }
        //front right sensor
        sensorStartPos += transform.right * frontSideSensorPosition;
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
        {
            //if (hit.collider.CompareTag("Car"))
            if (hit.collider.tag == this.tag)
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                avoiding = true;
                //avoidMultiplier -= 1f;
            }
        }


        /*front right angle sensor
        if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength))
        {
            if (hit.collider.CompareTag("Terrain"))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                avoiding = true;
                avoidMultiplier -= 0.5f;
            }
        }*/


        //front left sensor
        sensorStartPos -= 2 * transform.right * frontSideSensorPosition;
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
        {
            //if (hit.collider.CompareTag("Car"))
            if (hit.collider.tag == this.tag)
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                avoiding = true;
                //avoidMultiplier += 1f;
            }
        }

        else
            avoiding = false;


        /*front left angle sensor, not required right now
        else if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(-frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength))
        {
            if (hit.collider.CompareTag("Terrain"))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                avoiding = true;
                avoidMultiplier += 0.5f;
            }
        }*/


        if (avoiding)
        {
            //targetAngle = maxSteerAngle * avoidMultiplier;
            isBraking = true;
        }

        else if (!avoiding && !atSignal)
        {
            isBraking = false;
        }
    }
    private void ApplySteer()
    {
        if (avoiding)
            return;
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        targetAngle = newSteer;

    }
    private void Drive()
    {
        currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000;
        if (currentSpeed < maxSpeed && !isBraking)
        {
            wheelFL.motorTorque = maxMotorTorque;
            wheelFR.motorTorque = maxMotorTorque;
        }
        else
        {
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
        }
    }

    private void checkWaypointDistance()
    {
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < 1.5f)
        {
            if (currentNode == nodes.Count - 1)
            {
                currentNode = 0;
            }
            else
            {
                currentNode++;
            }
        }
    }

    private void Braking()
    {
        if (isBraking)
        {
            wheelBL.brakeTorque = maxBrakeTorque;
            wheelBR.brakeTorque = maxBrakeTorque;
        }
        else
        {
            wheelBL.brakeTorque = 0;
            wheelBR.brakeTorque = 0;
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "turningPointEntryStop")
        {
            if (!GetComponent<Rigidbody>().isKinematic)
            {
                GetComponent<Rigidbody>().isKinematic = true;
            }
            atSignal = true;
            isBraking = true;
            Debug.Log("Enter stop");

        }
        if (other.gameObject.tag == "Slow")
        {
            atSignal = true;
            isBraking = true;
            Debug.Log("Enter slow");

        }
        if (other.gameObject.tag == "turningPointEntryGo")
        {
            atSignal = false;
            isBraking = false;
            if (GetComponent<Rigidbody>().isKinematic)
            {
                GetComponent<Rigidbody>().isKinematic = false;
            }
            maxSpeed = 50f;
            maxMotorTorque = 60f;
            Debug.Log("Enter go");

        }
        if (other.gameObject.tag == "turningPointExit")
        {

            isBraking = false;
            maxMotorTorque = 200f;
            maxSpeed = 150f;
            Debug.Log("Exit turn");

        }

    }

    private void LerpToSteerAngle()
    {
        wheelFL.steerAngle = Mathf.Lerp(wheelFL.steerAngle, targetAngle, Time.deltaTime * turnSpeed);
        wheelFR.steerAngle = Mathf.Lerp(wheelFR.steerAngle, targetAngle, Time.deltaTime * turnSpeed);
    }
}
