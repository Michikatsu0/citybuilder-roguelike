using System.Collections;
using UnityEngine;

public class BaseBuilding : MonoBehaviour
{
    public int healthPoints = 100;
    public int moneyGeneratedPerMinute = 1000;
    public int buildingCost = 1000;

    public float generatedMoney = 0;
    public bool isGeneratingMoney = true;
    private int threshold = 200; // Example threshold for collection
    private float moneyPerSecond;
    public CellBuilding cellBuilding;

    private Coroutine generateMoneyCoroutine, damageCoroutine;
    private MeshRenderer[] meshRenderers;

    void Start()
    {
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        moneyPerSecond = moneyGeneratedPerMinute / 60f; // Corrigiendo el cálculo del dinero por segundo
        generateMoneyCoroutine = StartCoroutine(GenerateMoney());
    }

    void Update()
    {
        if (!NaturalChaosManager.Instance.isGameActive) return;
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
            yield return null; // Esperar un frame antes de continuar el bucle
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
        isGeneratingMoney = true; // Reiniciar la generación después de la recolección
        cellBuilding.doOnce = true;
        cellBuilding.StartFadeOut();

        if (generateMoneyCoroutine != null)
            StopCoroutine(generateMoneyCoroutine);

        generateMoneyCoroutine = StartCoroutine(GenerateMoney()); // Reiniciar la generación de dinero
        return moneyToCollect;
    }

    public int GetGeneratedMoney()
    {
        return (int)generatedMoney;
    }

    public void TakeDamage(int damage)
    {
        // Cancel the previous damage animation if it's still running
        if (damageCoroutine != null)
            StopCoroutine(damageCoroutine);

        // Start the damage animation
        int targetHealth = Mathf.Max(healthPoints - damage, 0);
        damageCoroutine = StartCoroutine(AnimateHealthDecrease(healthPoints, targetHealth));
    }

    private IEnumerator AnimateHealthDecrease(int startHealth, int endHealth)
    {
        float duration = 0.5f; // Duration of the animation
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / duration;

            // Interpolate between startHealth and endHealth
            int currentHealth = Mathf.RoundToInt(Mathf.Lerp(startHealth, endHealth, progress));
            cellBuilding.textsFade[1].text = currentHealth.ToString();
            healthPoints = currentHealth;

            yield return null;
        }

        // Ensure the final health value is set correctly
        healthPoints = endHealth;
        cellBuilding.textsFade[1].text = endHealth.ToString();

        if (endHealth <= 0)
        {
            cellBuilding.textsFade[1].text = "0"; // Ensure the text shows 0 if healthPoints is <= 0
            cellBuilding.DestroyBuild(meshRenderers, this);
        }
    }
}
