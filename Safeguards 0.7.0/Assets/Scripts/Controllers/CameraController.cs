using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public double speed = 0.05;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3((float)speed * -Input.GetAxis("Horizontal"), 0, 0);
        transform.position += new Vector3(0, 0, (float)speed * -Input.GetAxis("Vertical"));
        
    }
}
