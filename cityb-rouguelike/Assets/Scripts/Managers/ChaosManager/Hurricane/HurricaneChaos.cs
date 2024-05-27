using UnityEngine;
using System.Collections.Generic;

public class HurricaneChaos : BaseChaos
{
    public List<GameObject> hurricanePoints = new List<GameObject>(); // Lista de puntos de generación
    public GameObject hurricanePrefab; // Prefab del tornado
    public float hurricaneTravelDistance = 50f;

    public override void TriggerChaosEvent()
    {
        Debug.Log("Evento de Huracán desencadenado");

        // Seleccionar aleatoriamente un punto de generación
        if (hurricanePoints.Count == 0) return;
        GameObject spawnPoint = hurricanePoints[Random.Range(0, hurricanePoints.Count)];

        // Instanciar el tornado en el punto de generación seleccionado
        GameObject hurricaneInstance = Instantiate(hurricanePrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        Hurricane hurricaneScript = hurricaneInstance.GetComponent<Hurricane>();
        TriggerDamageHitbox triggerDamageHitbox = hurricaneInstance.GetComponent<TriggerDamageHitbox>();
        triggerDamageHitbox.baseChaos = this;
        if (hurricaneScript != null)
            hurricaneScript.Initialize(Vector3.forward, hurricaneScript.travelDistance); // Inicializar con la dirección y distancia
    }
}