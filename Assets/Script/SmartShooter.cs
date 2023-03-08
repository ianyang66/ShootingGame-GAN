using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Packages.Rider.Editor.PostProcessors;

public class SmartShooter : Agent
{
    public new int MaxStep;
    public Transform tTarget;
    public Rigidbody rTarget;
    public Rigidbody rBody;
    public override void Initialize()
    {
        rBody = this.GetComponent<Rigidbody>();
        MaxStep = 10000;
        ResetMe();
    }

    public override void OnEpisodeBegin()
    {


        MaxStep = 10000;

    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.localPosition.x);
        sensor.AddObservation(this.transform.localPosition.z);

        sensor.AddObservation(tTarget.localPosition.x);
        sensor.AddObservation(tTarget.localPosition.z);

        sensor.AddObservation(this.transform.localRotation.y);
        sensor.AddObservation(tTarget.transform.localRotation.y);
    }


    public float speed = 5f;
    public float Rspeed = 180f;
    public override void OnActionReceived(float[] vectorAction)
    {
        this.transform.localEulerAngles = new Vector3(0, this.transform.localEulerAngles.y, 0);

        float controlGo, controlSpin, controlLF = 0;
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
        //Rotation
        if (vectorAction[2] == 2)
        {
            controlSpin = 1;
        }
        else
        {
            controlSpin = -vectorAction[2];
        }
        //Fire
        if (vectorAction[3] == 1)
        {
            BroadcastMessage("Fire");
        }


        Vector3 TL = this.transform.localPosition;
        if ((TL.x < 11) & (TL.x > -11) & (TL.z < 11) & (TL.z > -11) & (TL.y < 0.6))
        {
            this.transform.Translate(0, 0, Time.deltaTime * speed * controlGo);
            this.transform.Translate(Time.deltaTime * speed * controlLF, 0, 0);
            this.transform.Rotate(0, Time.deltaTime * Rspeed * controlSpin, 0);
        }
        if (tTarget.localPosition.y < -1)
        {
            SetReward(MaxStep * 0.0001f);
            EndEpisode();
        }
        if (this.transform.localPosition.y < -1)
        {
            SetReward(-MaxStep * 0.0001f);
            ResetMe();
            EndEpisode();
        }
        if (MaxStep < 1)
        {
            EndEpisode();
        }
        MaxStep -= 1;

    }
    void ResetMe()
    {

        this.transform.localPosition = new Vector3(Random.Range(-2f, -8.0f), 1, Random.Range(-2f, -8.0f));
        rBody.angularVelocity = Vector3.zero;
        rBody.velocity = Vector3.zero;
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
        if (Input.GetKey(KeyCode.D))
        {
            actionsOut[1] = 2f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            actionsOut[1] = 1f;
        }
        else
        {
            actionsOut[1] = 0f;
        }

        //Spin
        if (Input.GetKey(KeyCode.Q))
        {
            actionsOut[2] = 2f;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            actionsOut[2] = 1f;
        }
        else
        {
            actionsOut[2] = 0f;
        }
        //Fire
        if (Input.GetButton("Fire1"))
        {
            actionsOut[3] = 1f;
        }
        else
        {
            actionsOut[3] = 0f;
        }
    }

}
