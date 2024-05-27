using UnityEngine;

public class FireChaos : BaseChaos
{
    public static FireChaos Instance;
    public GameObject fireParticlePrefab;

    private void Awake()
    {
        Instance = this;
    }

    public override void TriggerChaosEvent()
    {
        // L�gica espec�fica del evento de caos del fuego
        Debug.Log("Evento de Fuego desencadenado");
        BaseBuilding[] buildings = FindObjectsOfType<BaseBuilding>(); // Seleccionar aleatoriamente un edificio de la escena
        if (buildings.Length == 0) return;

        BaseBuilding targetBuilding = buildings[Random.Range(0, buildings.Length)];
        StartFireAtBuilding(targetBuilding);
    }

    private void StartFireAtBuilding(BaseBuilding building)
    {
        // Instanciar el sistema de part�culas en la posici�n del edificio seleccionado
        var fireInstance = Instantiate(fireParticlePrefab, building.transform.position, Quaternion.identity);
        fireInstance.transform.SetParent(building.transform);

        // Configurar el componente FireChaos
        Fire fireChaos = fireInstance.GetComponent<Fire>();
        fireChaos.triggerDamageHitbox.baseChaos = this;

        // Iniciar la expansi�n y contracci�n del fuego
        StartCoroutine(fireChaos.ExpandAndContractFire());
    }
}