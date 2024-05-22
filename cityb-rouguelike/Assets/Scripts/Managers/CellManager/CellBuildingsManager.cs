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
        for (int i = 0; i < 2; i++)
            cells[Random.Range(1, 5)].bought = true;
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
}
