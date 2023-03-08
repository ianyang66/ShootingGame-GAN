using UnityEngine;

public class Gun : MonoBehaviour
{
    public Rigidbody Bullet;
    public float Speed = 20f;
    public float ReloadTime = 0.5f;

    float lastShot = -1f;

    void Fire()
    {
        if(Time.time>ReloadTime+lastShot)
        {
            //產生魚
            Rigidbody IBullet = Instantiate(Bullet, transform.position, transform.rotation) as Rigidbody;
            //發射魚
            IBullet.velocity = transform.TransformDirection(new Vector3(0, 0, Speed));
            //忽略碰撞
            if (transform.root.GetComponent<Collider>())
                Physics.IgnoreCollision(IBullet.GetComponent<Collider>(), transform.root.GetComponent<Collider>());

            lastShot = Time.time;
        }
    }
}
