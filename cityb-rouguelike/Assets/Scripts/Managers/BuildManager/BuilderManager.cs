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

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {

    }

    void Update()
    {
        if (!selectionBuild) return;

        CalculateGizmo();
        selectionBuild.SetState(selectionBuild);
    }

    public void CreateGizmo(int prefabBuildIndex)
    {
        if (prefabBuildIndex == 0 || prefabBuildIndex == 1)
            prefabBuildIndex = Random.Range(0, 2);
        var building = Instantiate(buildingPrefabs[prefabBuildIndex], BuildMousePlayer.Instance.mousePosition.transform.position, Quaternion.identity);
        selectionBuild = building.GetComponent<SelectionBuild>();
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
                if (cellBuilding.bought)
                    selectionType = SelectionTypes.Green;
                else 
                    selectionType = SelectionTypes.Red;
            }
            else
                selectionType = SelectionTypes.Yellow;
        }
        else
            selectionType = SelectionTypes.Yellow;
    }

    
}