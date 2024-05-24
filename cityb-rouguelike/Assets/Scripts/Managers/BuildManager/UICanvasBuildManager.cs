using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvasBuildManager : MonoBehaviour
{
   
    public List<UIBuilding> uIBuildings  =new List<UIBuilding>();
    void Start()
    {
        foreach (var building in uIBuildings)
        {
            building.texts[0].text = building.baseBuilding.moneyGeneratedPerMinute.ToString() + " /m";
            building.texts[1].text = building.baseBuilding.buildingCost.ToString();
        }
    }
}
