using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SelectionBuild : MonoBehaviour
{
    public List<MeshRenderer> renderers = new List<MeshRenderer>();
    public List<Material[]> materials = new List<Material[]>();

    void Start()
    {
        foreach (var renderer in renderers)
            materials.Add(renderer.materials);
    }

    public void SetState(SelectionBuild selectionBuild)
    {
        switch (BuilderManager.Instance.selectionType)
        {
            case BuilderManager.SelectionTypes.Red:
                SetColor(BuilderManager.Instance.selectMaterials[0]);
                break;
            case BuilderManager.SelectionTypes.Yellow:
                SetColor(BuilderManager.Instance.selectMaterials[1]);
                break;
            case BuilderManager.SelectionTypes.Green:
                SetColor(BuilderManager.Instance.selectMaterials[2]);
                break;
        }
    }

    private void SetColor(Material selectMaterial)
    {
        foreach (var renderer in renderers)
        {
            foreach (var mats in materials)
            {
                for (var i = 0; i < mats.Length; i++)
                    mats[i] = selectMaterial;

                renderer.materials = mats;
            }
        }
    }
}
