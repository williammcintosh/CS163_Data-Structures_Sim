using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    public float movespeed = 2.0f;
    public bool setup = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (setup) {
            if(Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(Vector3.right * movespeed * Time.deltaTime);
            }
            if(Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(Vector3.left * movespeed * Time.deltaTime);
            }
            if(Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(Vector3.down * movespeed * Time.deltaTime);
            }
            if(Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(Vector3.up * movespeed * Time.deltaTime);
            }
            if(Input.GetKey(KeyCode.Q))
            {
                transform.Translate(Vector3.forward * movespeed * Time.deltaTime);
            }
            if(Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.back * movespeed * Time.deltaTime);
            }
        }
    }
}
