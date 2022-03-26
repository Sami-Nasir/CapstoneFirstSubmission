using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookUp : MonoBehaviour
{
    private float mouseX;
    private float mouseY;
    public float speed;
    public GameObject gameObj;
    private Transform player;
    private float xRotation;
    private float angle=0f;
    // Start is called before the first frame update
    void Start()
    {
        player = gameObj.GetComponent<Transform>();
        //Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position = Vector3.Lerp(transform.position, player.position, speed * Time.deltaTime);
        transform.LookAt(player);
    }
    void MouseX()
    {

    }
    void MouseY()
    {
        //mouseX = Input.GetAxis("Mouse X");
        //position.transform.Rotate(mouseX * speed * Vector3.up * Time.deltaTime, Space.Self);
        //mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * speed;
        //xRotation -= mouseY;
        //xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            angle -= 5f;
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            angle += 5;
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }
}
