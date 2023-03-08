using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rBody;
    public float timeOut = 1.0f;

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        rBody.angularVelocity = new Vector3(0, 0, 0);
        Destroy(gameObject, timeOut);
    }

}
