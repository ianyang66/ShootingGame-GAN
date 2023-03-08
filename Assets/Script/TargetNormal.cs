using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetNormal : MonoBehaviour
{
    public Rigidbody rBody;


    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        ResetTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.localPosition.y<-1)
        {
            ResetTarget();  
        }
    }
    void ResetTarget()
    {
        rBody.angularVelocity = Vector3.zero;
        rBody.velocity = Vector3.zero;
        this.transform.localPosition = new Vector3(Random.Range(2f, 8f), 2, Random.Range(2f, 8f));
        this.transform.localRotation = new Quaternion(0, 0, 0, 0);    
    }

}
