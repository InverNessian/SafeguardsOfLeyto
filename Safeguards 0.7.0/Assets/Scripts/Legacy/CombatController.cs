using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CombatController : MonoBehaviour
{
    //public static event DamageDealt;
    //this event has two parameters, the person taking damage and the damage dealer (gameObjects)

    /*
public static BeginAttack EvBeginAttack = new BeginAttack();
public static HitRate EvHitRate = new HitRate();
public static CritRate EvCritRate = new CritRate();
public static AttackStep EvAttackStep = new AttackStep();
public static DefenseStep EvDefenseStep = new DefenseStep();
public static DamageStep EvDamageStep = new DamageStep();
public static FinishAttack EvFinishAttack = new FinishAttack();
public static CombatEnd EvCombatEnd = new CombatEnd();
*/

    //public delegate void BeginAttack();

    public delegate void TriggerEffect();
    public delegate int TriggerBonus();

    void Start()
    {
        InputController.mDuelCombat.AddListener(DuelCombat);
    }
    //do we want to have events just for combat?  might make things easier since we could have each special combat really just call some Events.
        //these could be non-static events!
        //I think, considering the issues with deriveAttack, etc, this will need to be the solution.  it's just too awkward to have these methods with split info.
    
    public void DuelCombat(StatsManager initiate, StatsManager counter)
    {
        /*BeginAttack InitBeginAttack = new BeginAttack();
        HitRate InitHitRate = new HitRate();
        CritRate InitCritRate = new CritRate();
        AttackStep InitAttackStep = new AttackStep();
        DefenseStep InitDefenseStep = new DefenseStep();
        DamageStep InitDamageStep = new DamageStep();
        FinishAttack InitFinishAttack = new FinishAttack();
        CombatEnd InitCombatEnd = new CombatEnd();
        */

        TalentTrigger[] triggers = initiate.gameObject.GetComponents<TalentTrigger>();
        foreach (TalentTrigger trigger in triggers)
        {
            if (trigger is ICombat)
            {
                //BeginAttack InitBeginAttack = new BeginAttack();
                //InitBeginAttack.AddListener(((ICombat)trigger).BeginAttack);
                /*
                //InitBeginAttack.AddListener(((ICombat)trigger).FindAdvantage);
                InitHitRate.AddListener(((ICombat)trigger).Accuracy);
                InitHitRate.AddListener(((ICombat)trigger).BeginAttack);
                InitCritRate.AddListener(((ICombat)trigger).BeginAttack);
                InitCritRate.AddListener(((ICombat)trigger).BeginAttack);
                InitAttackStep.AddListener(((ICombat)trigger).BeginAttack);
                InitAttackStep.AddListener(((ICombat)trigger).BeginAttack);
                InitDefenseStep.AddListener(((ICombat)trigger).BeginAttack);
                InitDefenseStep.AddListener(((ICombat)trigger).BeginAttack);
                InitDamageStep.AddListener(((ICombat)trigger).BeginAttack);
                InitDamageStep.AddListener(((ICombat)trigger).BeginAttack);
                InitFinishAttack.AddListener(((ICombat)trigger).BeginAttack);
                InitFinishAttack.AddListener(((ICombat)trigger).BeginAttack);
                InitCombatEnd.AddListener(((ICombat)trigger).BeginAttack);
        BeginAttack CounterBeginAttack = new BeginAttack();
        HitRate CounterHitRate = new HitRate();
        CritRate CounterCritRate = new CritRate();
        AttackStep CounterAttackStep = new AttackStep();
        DefenseStep CounterDefenseStep = new DefenseStep();
        DamageStep CounterDamageStep = new DamageStep();
        FinishAttack CounterFinishAttack = new FinishAttack();
        CombatEnd CounterCombatEnd = new CombatEnd();
                */
            }
        }


    }


    private void SetTalentListeners(GameObject unit)
    {
        
    }

    /*
    public void DuelCombat(GameObject attacker, GameObject defender)
    {
        StatsData atkr = attacker.GetComponent<StatsManager>().statsData;
        StatsData dfndr = attacker.GetComponent<StatsManager>().statsData;


        List<AttackData> attacks = new List<AttackData>();
        attacks.Add(GenerateHit(attacker, defender, true));
        attacks.Add(GenerateHit(defender, attacker, true)); //we'll have to add some conditional checks here, or do an event system or something.  this is a temp measure.
        if(atkr.DeriveFollowup() - dfndr.DeriveFollowup() >= 5)
        {
            attacks.Add(GenerateHit(attacker, defender, true));
        }
        if (dfndr.DeriveFollowup() - atkr.DeriveFollowup() >= 5)
        {
            attacks.Add(GenerateHit(defender, attacker, true));
        }

        foreach(AttackData ad in attacks)
        {
            Debug.Log(ad.ToString()); //once UI is up, have this display attacks in the UI.
        }

        //now we return control back to the player
        InputController.SetState(InputController.PlayStates.UNIT_SELECT);

        //then 
    }
    */

    public void DoCombat(UnitData attacker, UnitData defender)
    {
        throw new System.NotImplementedException();
    }

    public AttackData GenerateHit(GameObject attacker, GameObject defender, bool preview)
    {
        StatsData atkr = attacker.GetComponent<StatsManager>().statsData;
        StatsData dfndr = attacker.GetComponent<StatsManager>().statsData;
        AttackData attackData = new AttackData();
        attackData.attacker = attacker.name;
        attackData.HitRate = 60 + atkr.DeriveAccuracy() - dfndr.DeriveEvasion();
        attackData.CriticalRate = atkr.DeriveCritical() - dfndr.DeriveGuard();
        attackData.DamageDealt = atkr.DeriveAttack(GetWeapon(attacker)) - dfndr.DeriveDefense(GetWeapon(attacker));
        //update this to interact with Dual Wield later on.

        if (!preview)
        {
            //roll RNG and then Invoke(DamageDealt(dfndr, atkr))
        }
        return attackData;
    }

    private Weapon GetWeapon(GameObject guy)
    {
        Equip equipped = guy.GetComponent<Equip>();
        if (equipped.item)
        {
            //check for weapon 
        }
        return (Weapon)equipped.item;
    }

}
