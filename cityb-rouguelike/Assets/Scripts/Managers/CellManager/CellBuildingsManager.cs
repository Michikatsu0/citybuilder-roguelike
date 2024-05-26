using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CellBuildingsManager : MonoBehaviour
{
    public static CellBuildingsManager Instance;

    public List<CellBuilding> cells = new List<CellBuilding>();
    public float minDistance = 0, maxDistance = 30;
    public GameObject mousePosition;
    private RaycastHit hit;

    private void Awake()
    {
        Instance = this;    
    }

    void Start()
    {
        int cellsBought = 0;

        while (cellsBought < 2)
        {
            int randomIndex = Random.Range(1, 5);
            if (!cells[randomIndex].bought)
            {
                cells[randomIndex].bought = true;
                cellsBought++;
            }
        }

        BaseBuilding baseBuilding = Instantiate(BuilderManager.Instance.buildings[6], cells[0].position, Quaternion.identity);
        cells[0].SetBuild(baseBuilding);
    }

    private void Update()
    {
        FadeAnimationValue();
    }

    private void FadeAnimationValue()
    {
        if (BuildMousePlayer.Instance.GetRay(out hit))
        {
            foreach (var cell in cells)
            {
                float distance = Vector3.Distance(mousePosition.transform.position, cell.position);
                float fadeValue = Mathf.InverseLerp(minDistance, maxDistance, distance);
                cell.curveValue = cell.animationFadeCurve.Evaluate(fadeValue);
            }
        }
    }

    public bool AreAllBuildingsDestroyed()
    {
        foreach (var cell in cells)
        {
            if (cell.currentBuilding.healthPoints > 0)
                return false;
        }
        return true;
    }
}
