using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField]
    private int hpMax = 0;
    [SerializeField]
    private int hpNow;

    //public static event Down;

    // Start is called before the first frame update
    void Start()
    {
        if(hpMax == 0)
        {
            hpMax = GetComponentInParent<StatsManager>().statsData.healthValue;
        }
        hpNow = hpMax;
        //set TakeDamage as a listener for DamageDealt Event?
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amount)
    {
        if (amount > 0)
        {
            hpNow -= amount;
        }
        if (hpNow <= 0)
        {
            //call "Down" event
        }
    }
}
