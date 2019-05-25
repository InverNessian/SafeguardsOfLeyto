using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SupportMenu : MonoBehaviour
{
    public GameObject[] menuItems;
    public GameObject supportButton;
    public StatsManager person;
    public GrowthManager gm;
    public string shownSupport;

    private void Awake()
    {
        person = GameObject.Find(CampController.selected).GetComponent<StatsManager>();
        gm = person.GetComponent<GrowthManager>();
        ResetUnitInfo();
        UpdateSupportList();
    }

    public void UpdateInfoDisplay(string sname)
    {
        //first we reset the display
        ResetDisplay();
        shownSupport = sname;

        //then we fetch relevant info
        Support support = ImportController.GetSupportInfo(person.statsData.displayName, sname);

        //and set it to the appropriate panels
        menuItems[0].GetComponent<Text>().text = support.Friend;

        menuItems[1].GetComponentInChildren<Text>().text += support.HP + support.Speed;
        menuItems[1].GetComponentInChildren<Text>().text += support.Might + support.Favor;
        menuItems[2].GetComponentInChildren<Text>().text += support.Focus + support.Armor;
        menuItems[2].GetComponentInChildren<Text>().text += support.Skill + support.Ward;


        //set the text of the buy button
        menuItems[4].GetComponentInChildren<Text>().text = "Rank Up";
        if (CheckIfSupportAvailable(sname))
        {
            menuItems[4].GetComponent<Image>().color = Color.white;
        }
        else
        {
            menuItems[4].GetComponent<Image>().color = Color.gray;
        }
    }

    public void TryGainSupport()  //this needs to be fixed
    {
        if (CheckIfSupportAvailable(shownSupport))
        {
            //this part doesn't work as intended??
            if (gm.growthData.GainSupportRank(shownSupport))
            {
                //call the function to learn the support, then update display
                Support temp = ImportController.GetSupportInfo(person.statsData.displayName, shownSupport);
                gm.growthData.ApplySupportBonuses(temp);
                person.statsData.ApplySupportBonuses(temp);

                ResetDisplay();
                ResetUnitInfo();
                UpdateSupportList();
                UpdateInfoDisplay(shownSupport);
            }
            else
            {
                Debug.Log("Unit does not have enough support ticks to advance a rank!");
            }

        }
        else
        {
            Debug.Log("This check is unnecessary!");
        }
    }

    private bool CheckIfSupportAvailable(string sname)
    {
        if (gm.growthData.friends.Contains(sname))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void ResetUnitInfo()
    {
        //set name and total ticks
        menuItems[5].GetComponent<Text>().text = person.statsData.displayName;
        int temp = 0;
        foreach(int ticker in gm.growthData.ticks)
        {
            temp += ticker;
        }
        menuItems[6].GetComponent<Text>().text = "Total Ticks: " + temp;
    }

    private void ResetDisplay()
    {
        menuItems[0].GetComponent<Text>().text = "";
        menuItems[1].GetComponentInChildren<Text>().text = "";
        menuItems[2].GetComponentInChildren<Text>().text = "";
        for (int i = 0; i < menuItems[3].transform.childCount; i++)
        {
            Destroy(menuItems[3].transform.GetChild(i).gameObject);
        }

    }

    public void UpdateSupportList()
    {
        //first we clear out old buttons
        SupportButton[] supportButtons = GameObject.Find("SupportListPanel").GetComponentsInChildren<SupportButton>();
        for (int i = 0; i < supportButtons.Length; i++)
        {
            Destroy(supportButtons[i].gameObject);
        }

        //based on person, loop through their natural talents/available talents
        GameObject holder;
        foreach (string friend in gm.growthData.friends)
        {
            //create a prefab of the talent button
            holder = Instantiate(supportButton);
            holder.GetComponent<SupportButton>().support = friend;
            holder.GetComponent<SupportButton>().menu = this;
            holder.transform.SetParent(GameObject.Find("SupportListPanel").transform);
            holder.GetComponentInChildren<Text>().text = friend;

            //mark them differently if they are already learned
            if (!CheckIfSupportAvailable(friend))
            {
                holder.GetComponent<Image>().color = Color.gray;
            }
        }

        //now, for quality of life, we update the display with the first talent in the list
        UpdateInfoDisplay(GameObject.Find("SupportListPanel").transform.GetChild(0).GetComponent<SupportButton>().support);
    }
}
