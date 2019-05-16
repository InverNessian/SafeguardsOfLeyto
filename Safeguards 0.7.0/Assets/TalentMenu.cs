using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentMenu : MonoBehaviour
{
    public GameObject[] menuItems;
    public GameObject tagPanel;
    public GameObject talentButton;
    public StatsManager person;

    private void Awake()
    {
        person = GameObject.Find(CampController.selected).GetComponent<StatsManager>();
        menuItems[5].GetComponent<Text>().text = person.statsData.displayName;
        menuItems[6].GetComponent<Text>().text = "Talent Points: " + person.GetComponentInParent<GrowthManager>().growthData.talentPoints;
        UpdateTalentLists();
    }

    public void UpdateInfoDisplay(string tname)
    {
        //first we reset the display
        ResetDisplay();

        //then we fetch relevant info
        Talent talent = ImportController.GetTalentInfo(tname);

        //and set it to the appropriate panels
        menuItems[0].GetComponent<Text>().text = talent.TalentName;
        menuItems[1].GetComponentInChildren<Text>().text = talent.Description;
        foreach(string note in talent.Notes)
        {
            menuItems[2].GetComponentInChildren<Text>().text += note;
        }
        foreach (string tag in talent.Tags)
        {
            GameObject temp = Instantiate(tagPanel, new Vector3(0, 0, 0), Quaternion.identity);
            temp.transform.SetParent(menuItems[3].transform);
            temp.GetComponentInChildren<Text>().text = tag;
        }

        //set the text of the buy button
        menuItems[4].GetComponentInChildren<Text>().text = "Buy: " + talent.Cost + " Point(s)";
        if (CheckIfTalentLearned(tname))
        {
            menuItems[4].GetComponent<Image>().color = Color.gray;
        }
        else
        {
            menuItems[4].GetComponent<Image>().color = Color.white;
        }
    }

    private bool CheckIfTalentLearned(string tname)
    {
        if (person.statsData.talents.Contains(tname))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void ResetDisplay()
    {
        menuItems[0].GetComponent<Text>().text = "";
        menuItems[1].GetComponentInChildren<Text>().text = "";
        menuItems[2].GetComponentInChildren<Text>().text = "";
        for(int i=0; i<menuItems[3].transform.childCount; i++)
        {
            Destroy(menuItems[3].transform.GetChild(i).gameObject);
        }
    }

    public void UpdateTalentLists()
    {
        //based on person, loop through their natural talents/available talents
        GrowthManager growthManager = person.GetComponentInParent<GrowthManager>();
        GameObject holder;
        foreach (string native in growthManager.growthData.naturalTalents)
        {
            //create a prefab of the talent button
            holder = Instantiate(talentButton);
            holder.GetComponent<TalentButton>().talent = native;
            holder.GetComponent<TalentButton>().menu = this;
            holder.transform.SetParent(GameObject.Find("TalentsNativePanel").transform);
            holder.GetComponentInChildren<Text>().text = native;

            //mark them differently if they are already learned
            if (CheckIfTalentLearned(native))
            {
                holder.GetComponent<Image>().color = Color.gray;
            }
        }
        foreach (string shared in growthManager.growthData.acquiredTalents)
        {
            //create a prefab of the talent button
            holder = Instantiate(talentButton);
            holder.GetComponent<TalentButton>().talent = shared;
            holder.GetComponent<TalentButton>().menu = this;
            holder.transform.SetParent(GameObject.Find("TalentsSharedPanel").transform);
            holder.GetComponentInChildren<Text>().text = shared;

            //mark them differently if they are already learned
            if (CheckIfTalentLearned(shared))
            {
                holder.GetComponent<Image>().color = Color.blue;
            }
        }


    }
}
