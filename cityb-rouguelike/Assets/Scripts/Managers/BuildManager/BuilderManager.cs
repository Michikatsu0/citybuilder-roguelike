using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class BuilderManager : MonoBehaviour
{ 
    public static BuilderManager Instance;
    public enum SelectionTypes { Red,  Yellow, Green }
    public SelectionTypes selectionType = SelectionTypes.Yellow;
    public List<Material> selectMaterials = new List<Material>();
    public List<BaseBuilding> buildings = new List<BaseBuilding>();
    public List<SelectionBuild> buildingPrefabs = new List<SelectionBuild>();
    public SelectionBuild selectionBuild;
    public int prefabIndex;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (!selectionBuild) return;

        CalculateGizmo();
        selectionBuild.SetState(selectionBuild);
    }

    public void CreateGizmo(int prefabBuildIndex)
    {
        if (EconomyManager.Instance.SpendMoneyCheck(buildings[prefabBuildIndex].buildingCost))
        {
            if (selectionBuild)
                Destroy(selectionBuild.gameObject);

            prefabIndex = prefabBuildIndex;
            if (prefabIndex == 0 || prefabIndex == 1)
                prefabIndex = Random.Range(0, 2);
            var building = Instantiate(buildingPrefabs[prefabIndex], BuildMousePlayer.Instance.mousePosition.transform.position, Quaternion.identity);
            selectionBuild = building.GetComponent<SelectionBuild>();
        }

    }

    private void CalculateGizmo()
    {
        selectionBuild.transform.position = BuildMousePlayer.Instance.mousePosition.transform.position;
        if (BuildMousePlayer.Instance.GetRay(out RaycastHit hit))
        {
            CellBuilding cellBuilding = hit.collider.gameObject.GetComponent<CellBuilding>();

            if (cellBuilding)
            {
                selectionBuild.transform.position = cellBuilding.transform.position;
                if (cellBuilding.bought && !cellBuilding.building)
                {
                    selectionType = SelectionTypes.Green;
                    if (Input.GetMouseButton(0))
                    {
                        cellBuilding.building = true;
                        selectionBuild?.gameObject.SetActive(false);


                        if (EconomyManager.Instance.SpendMoneyCheck(buildings[prefabIndex].buildingCost))
                        {
                            StartCoroutine(EconomyManager.Instance.AnimateMoney(false, buildings[prefabIndex].buildingCost));
                            BaseBuilding prefabBuild = Instantiate(buildings[prefabIndex], selectionBuild.transform.position, Quaternion.identity);
                            cellBuilding.SetBuild(prefabBuild);
                        }

                        StartCoroutine(DelayDestroy());
                    }
                }
                else
                    selectionType = SelectionTypes.Red;
            }
            else
                selectionType = SelectionTypes.Yellow;
        }
        else
            selectionType = SelectionTypes.Yellow;

        if (Input.GetMouseButton(0))
        {
            selectionBuild?.gameObject.SetActive(false);
            StartCoroutine(DelayDestroy());
        }
    }


    private IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        // Check if selectionBuild is not null and its gameObject is not destroyed
        if (selectionBuild != null && selectionBuild.gameObject != null)
            Destroy(selectionBuild.gameObject);
    }
}