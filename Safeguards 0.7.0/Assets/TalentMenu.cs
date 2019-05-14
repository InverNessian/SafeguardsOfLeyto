using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentMenu : MonoBehaviour
{
    public GameObject[] menuItems;
    public GameObject tagPanel;
    public StatsManager person;

    public void UpdateInfoDisplay(string tname)
    {
        Talent talent = ImportController.GetTalentInfo(tname);

        menuItems[0].GetComponent<Text>().text = talent.TalentName;
        menuItems[1].GetComponent<Text>().text = talent.Description;
        foreach(string note in talent.Notes)
        {
            menuItems[2].GetComponent<Text>().text += note;
        }
        foreach (string tag in talent.Tags)
        {
            //RectTransformUtility.ScreenPointToLocalPointInRectangle(menuItems[3].transform as RectTransform, new Vector2(0, 0), null, out Vector2 vector2);
            GameObject temp = Instantiate(tagPanel, new Vector3(0, 0, 0), Quaternion.identity);
            temp.transform.SetParent(menuItems[3].transform);
            temp.GetComponentInChildren<Text>().text = tag;
        }
    }

    public void UpdateTalentLists(string s)
    {
        
    }
}
