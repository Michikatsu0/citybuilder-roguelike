using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public Transform center;

    public float initialRadius = 0f;
    public float maxRadius = 5f;
    public float expansionDuration = 5f; // Duration in seconds for the fire to reach max radius
    public float contractionDuration = 5f; // Duration in seconds for the fire to shrink to zero

    public SphereCollider sphereCollider;
    public TriggerDamageHitbox triggerDamageHitbox;
    public GameObject fireParticlePrefab;

    void Start()
    {
        if (center == null) // Ensure the center is set
            center = transform;
    }

    public IEnumerator ExpandAndContractFire()
    {

        float elapsedTime = 0f;
        // Expansión del fuego
        while (elapsedTime < expansionDuration)
        {
            if (BuilderManager.Instance.selectionBuild) yield return null;
            //if (fireInstance == null) yield break;

            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / expansionDuration;
            sphereCollider.radius = 2 * Mathf.Lerp(initialRadius, maxRadius, progress); // Expandir el radio del SphereCollider
            fireParticlePrefab.transform.localScale = Vector3.one * sphereCollider.radius; // Sincronizar el tamaño del sistema de partículas

            yield return null;
        }
        // Asegurarse de que el radio final esté configurado correctamente

        sphereCollider.radius = 2 * maxRadius;
        fireParticlePrefab.transform.localScale = Vector3.one * sphereCollider.radius;

        // Contracción del fuego
        elapsedTime = 0f;

        while (elapsedTime < contractionDuration)
        {
            if (BuilderManager.Instance.selectionBuild) yield return null;
            //if (fireInstance == null) yield break;

            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / contractionDuration;
            sphereCollider.radius = 2 * Mathf.Lerp(maxRadius, initialRadius, progress); // Contraer el radio del SphereCollider
            fireParticlePrefab.transform.localScale = Vector3.one * sphereCollider.radius; // Sincronizar el tamaño del sistema de partículas

            yield return null;
        }

        // Asegurarse de que el radio final esté configurado a cero y destruir el objeto de fuego
        sphereCollider.radius = 0;
        fireParticlePrefab.transform.localScale = Vector3.zero;
        Destroy(gameObject);
    }


    //private void OnDrawGizmos()
    //{
    //    if (center != null)
    //        NaturalChaosManager.DrawWireDisc(Color.red, center.position, center.up, maxRadius);
    //}
}
