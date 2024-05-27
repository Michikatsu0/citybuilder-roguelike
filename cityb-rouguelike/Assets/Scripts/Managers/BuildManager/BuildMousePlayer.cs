using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMousePlayer : MonoBehaviour
{
    public static BuildMousePlayer Instance; 
    public LayerMask layerMask;
    public GameObject mousePosition;
    private RaycastHit hit;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
    }

    private void Update()
    {
        if (!NaturalChaosManager.Instance.isGameActive) return;
        if (GetRay(out hit))
        {
            mousePosition.transform.position = hit.point;

            CellBuilding cellBuilding = hit.collider.gameObject.GetComponent<CellBuilding>();

            if (cellBuilding)
            {
                if (BuilderManager.Instance.selectionBuild) return;
                if (Input.GetMouseButton(0) && !cellBuilding.bought && !cellBuilding.building && !cellBuilding.recollect) // buy
                {
                    if (EconomyManager.Instance.SpendMoneyCheck(cellBuilding.priceSlot))
                    {
                        StartCoroutine(EconomyManager.Instance.AnimateMoney(false, cellBuilding.priceSlot));
                        cellBuilding.bought = true;
                        cellBuilding.StartFade();
                    }
                }
                else if (Input.GetMouseButtonDown(0) && cellBuilding.bought && cellBuilding.building && cellBuilding.recollect)
                {
                    if (cellBuilding.currentBuilding.CanCollectMoney())
                    {
                        EconomyManager.Instance.AddMoney(cellBuilding.currentBuilding.CollectMoney());
                        cellBuilding.recollect = false; // Reset recollection state
                    }
                }
            }
        }
    }

    public bool GetRay(out RaycastHit hit)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit, float.MaxValue, layerMask);
    }

}
