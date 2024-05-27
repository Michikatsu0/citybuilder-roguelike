using UnityEngine;
using System.Collections.Generic;

public class HurricaneChaos : BaseChaos
{
    public List<GameObject> hurricanePoints = new List<GameObject>(); // Lista de puntos de generaci�n
    public GameObject hurricanePrefab; // Prefab del tornado
    public float hurricaneTravelDistance = 50f;

    public override void TriggerChaosEvent()
    {
        Debug.Log("Evento de Hurac�n desencadenado");

        // Seleccionar aleatoriamente un punto de generaci�n
        if (hurricanePoints.Count == 0) return;
        GameObject spawnPoint = hurricanePoints[Random.Range(0, hurricanePoints.Count)];

        // Instanciar el tornado en el punto de generaci�n seleccionado
        GameObject hurricaneInstance = Instantiate(hurricanePrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        Hurricane hurricaneScript = hurricaneInstance.GetComponent<Hurricane>();
        TriggerDamageHitbox triggerDamageHitbox = hurricaneInstance.GetComponent<TriggerDamageHitbox>();
        triggerDamageHitbox.baseChaos = this;
        if (hurricaneScript != null)
            hurricaneScript.Initialize(Vector3.forward, hurricaneScript.travelDistance); // Inicializar con la direcci�n y distancia
    }
}