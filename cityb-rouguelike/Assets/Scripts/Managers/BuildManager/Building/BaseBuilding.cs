using System.Collections;
using UnityEngine;

public class BaseBuilding : MonoBehaviour
{
    public int healthPoints = 100;
    public int moneyGeneratedPerMinute = 1000;
    public int buildingCost = 1000;
    
    private float generatedMoney = 0;
    private bool isGeneratingMoney = true;
    private int threshold = 200; // Example threshold for collection
    private float moneyPerSecond;
    public CellBuilding cellBuilding;

    void Start()
    {
        moneyPerSecond = moneyGeneratedPerMinute / 60f; // Corrigiendo el cálculo del dinero por segundo
        StartCoroutine(GenerateMoney());
    }

    void Update()
    {
        if (CanCollectMoney())
            cellBuilding.recollect = true;
        else
            cellBuilding.recollect = false;
    }

    private IEnumerator GenerateMoney()
    {
        while (isGeneratingMoney)
        {
            if (generatedMoney < moneyGeneratedPerMinute)
            {
                generatedMoney += (moneyPerSecond * Time.deltaTime); // Convertimos el dinero generado por frame correctamente
                int tempMoney = (int)generatedMoney;
                cellBuilding.textsFade[3].text = tempMoney.ToString();
            }
            else
                isGeneratingMoney = false;

            yield return null;
        }
    }

    public bool CanCollectMoney()
    {
        return generatedMoney >= threshold;
    }

    public int CollectMoney()
    {
        Debug.Log("holactemoneda");
        int moneyToCollect = (int)generatedMoney;
        generatedMoney = 0;
        isGeneratingMoney = true; // Restart generation after collection
        cellBuilding.doOnce = true;

        cellBuilding.StartFade();
        StartCoroutine(GenerateMoney()); // Reiniciamos la generación de dinero
        return moneyToCollect;
    }

    public int GetGeneratedMoney()
    {
        return (int)generatedMoney;
    }
}
