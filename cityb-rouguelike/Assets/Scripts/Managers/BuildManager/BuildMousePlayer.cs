using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMousePlayer : MonoBehaviour
{
    public static BuildMousePlayer Instance; 
    public LayerMask layerMask;
    public bool gizmoActivated;
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
        if (GetRay(out hit))
        {
            mousePosition.transform.position = hit.point;

            CellBuilding cellBuilding = hit.collider.gameObject.GetComponent<CellBuilding>();

            if (cellBuilding)
            {
                if (BuilderManager.Instance.selectionBuild) return;
                if (Input.GetMouseButton(0) && !cellBuilding.bought) // buy
                {
                    cellBuilding.bought = true;
                    cellBuilding.StartFade();
                }
                if (gizmoActivated && Input.GetMouseButton(0) && !cellBuilding.building) //build
                {
                    
                }
                else if (Input.GetMouseButton(0) && cellBuilding.building)
                {

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
