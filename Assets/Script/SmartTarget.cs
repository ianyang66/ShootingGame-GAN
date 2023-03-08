using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class SmartTarget : Agent
{
    public Transform tSmartShooter;
    public Rigidbody rBody;
    public new int MaxStep;

    public override void Initialize()
    {
        rBody = GetComponent<Rigidbody>();
        ResetTarget();
    }
    public override void OnEpisodeBegin()
    {
        MaxStep = 10000;
        ResetTarget();
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.localPosition.x);
        sensor.AddObservation(this.transform.localPosition.z);
        sensor.AddObservation(tSmartShooter.localPosition.x);
        sensor.AddObservation(tSmartShooter.localPosition.z);

        sensor.AddObservation(tSmartShooter.localRotation.y);
    }
    public float speed = 20f;
    public override void OnActionReceived(float[] vectorAction)
    {
        float controlGo, controlLF = 0;
        //Foward
        if (vectorAction[0] == 2)
        {
            controlGo = 1;
        }
        else
        {
            controlGo = -vectorAction[0];
        }
        //Left & Right
        if (vectorAction[1] == 2)
        {
            controlLF = 1;
        }
        else
        {
            controlLF = -vectorAction[1];
        }



        Vector3 TL = this.transform.localPosition;
        if ((TL.x < 11) & (TL.x > -11) & (TL.z < 11) & (TL.z > -11) & (TL.y < 0.6))
        {
            this.transform.Translate(0, 0, Time.deltaTime * speed * controlGo);
            this.transform.Translate(Time.deltaTime * speed * controlLF, 0, 0);
        }


        if (this.transform.localPosition.y < -1f)
        {
            SetReward(1 - (MaxStep * 0.0001f));
            EndEpisode();
        }
        if (MaxStep < 1)
        {
            SetReward(1.0f);
            EndEpisode();
        }
        MaxStep -= 1;
    }

    public override void Heuristic(float[] actionsOut)
    {
        //Foward
        if (Input.GetKey(KeyCode.S))
        {
            actionsOut[0] = 2f;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            actionsOut[0] = 1f;
        }
        else
        {
            actionsOut[0] = 0f;
        }
        //Left & Right
        if (Input.GetKey(KeyCode.A))
        {
            actionsOut[1] = 2f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            actionsOut[1] = 1f;
        }
        else
        {
            actionsOut[1] = 0f;
        }
    }
    void ResetTarget()
    {
        rBody.angularVelocity = Vector3.zero;
        rBody.velocity = Vector3.zero;
        this.transform.localPosition = new Vector3(Random.Range(-8f, 8f), 2, Random.Range(-8f, 8f));
        this.transform.localRotation = new Quaternion(0, 0, 0, 0);
    }

}

