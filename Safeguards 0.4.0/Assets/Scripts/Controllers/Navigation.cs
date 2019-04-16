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
                UnitData target = hit.collider.GetComponent<UnitData>();
                if (target != null && PlayerPrefs.GetString("selected", "none").Equals(target.unit_name))
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
                UnitData target = hit.collider.GetComponent<UnitData>();
                if (target != null && GameController.master.selected.Equals(target.unit_name))
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
