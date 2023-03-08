using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float mouseSensitivity = 500f;
    public Transform TT;
    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   
    }

    // Update is called once per frame
    void Update()
    {
        //鏡頭控制
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        this.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);  //轉鏡頭
        TT.Rotate(Vector3.up * mouseX); //轉人

        //人物控制

        float speed = 5f;
        float V, H = 0f;

        TT.localEulerAngles = new Vector3(0, TT.localEulerAngles.y, 0);
        //Vertical
        if (Input.GetKey(KeyCode.S)) { V = -1f; }
        else if (Input.GetKey(KeyCode.W)) { V = 1f; }
        else { V = 0f; }

        //Horizenal
        if (Input.GetKey(KeyCode.D)) { H = 1f; }
        else if (Input.GetKey(KeyCode.A)) { H = -1f; }
        else { H = 0f; }

        //Fire
        if (Input.GetButton("Fire1")) { BroadcastMessage("Fire"); }


        //判斷是否可以移動
        Vector3 TTL = TT.localPosition;
        if( (TTL.x<11) & (TTL.x>-11) & (TTL.z<11) & (TTL.z>-11) & TTL.y<2 )
        {
            TT.Translate(0, 0, Time.deltaTime * speed * V);
            TT.Translate(Time.deltaTime * speed * H, 0, 0);
        }

        //判斷是否跌落
        if(TT.localPosition.y<-1)
        {
            TT.localPosition = new Vector3(Random.Range(2f, 8.0f), 1, Random.Range(2f, 8.0f));
        }

    }
}
