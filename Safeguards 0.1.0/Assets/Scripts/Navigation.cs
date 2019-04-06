using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour
{
    NavMeshAgent agent;
    CameraController cam;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        cam = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100) && cam.selected.Equals(GetComponent<Data_Manager>().character_name))
            {
                agent.destination = hit.point;
            }
        }
        if (Input.GetMouseButtonDown(1))
        { // if left button pressed...
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (cam.selected.Equals(GetComponent<Data_Manager>().character_name))
                {
                    cam.selected = "none";
                    Debug.Log("Deselect");
                }
                else
                {
                    cam.selected = hit.collider.GetComponent<Data_Manager>().character_name; //something about this is not working right. it won't let Hadrian be selected unless Fant already was.
                    Debug.Log("Select");
                }
                
            }
        }
    }
}
