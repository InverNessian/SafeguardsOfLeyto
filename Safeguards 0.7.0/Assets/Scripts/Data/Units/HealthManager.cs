using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField]
    private int hpMax = 0;
    [SerializeField]
    private int hpNow;

    public DamageEvent damageEvent = new DamageEvent();
    public HealEvent healEvent = new HealEvent();
    public DownEvent downEvent = new DownEvent();

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

    public void TakeDamage(int amount)
    {
        if (amount > 0)
        {
            hpNow -= amount;
            damageEvent.Invoke(this, amount);
            if (hpNow <= 0)
            {
                downEvent.Invoke(this);
            }
        }
    }

    public void HealDamage(int amount)
    {
        if(amount > 0)
        {
            hpNow += amount;
            healEvent.Invoke(this, amount); //maybe I need to adjust these so they can leverage the Events better
        }
    }

    public int CheckHP()
    {
        return hpNow;
    }
}
