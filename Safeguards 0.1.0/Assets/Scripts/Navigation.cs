using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour
{
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Data_Manager target = hit.collider.GetComponent<Data_Manager>();
                if (target != null && PlayerPrefs.GetString("selected", "none").Equals(target.character_name))
                {
                    agent.destination = hit.point;
                }
                
            }
        }
        if (Input.GetMouseButtonDown(1))
        { // if right button pressed...
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //if (cam.selected.Equals(GetComponent<Data_Manager>().character_name))
                Data_Manager target = hit.collider.GetComponent<Data_Manager>();
                if (target != null && GameController.master.selected.Equals(target.character_name))
                {
                    PlayerPrefs.SetString("selected", "none"); ;
                    Debug.Log("Deselect");
                }
                else
                {
                    //PlayerPrefs.SetString("selected", hit.collider.GetComponent<Data_Manager>().character_name);
                    //cam.selected = hit.collider.GetComponent<Data_Manager>().character_name; //something about this is not working right. it won't let Hadrian be selected unless Fant already was.
                    Debug.Log("Select");
                }
                
            }
        }
    }
}
