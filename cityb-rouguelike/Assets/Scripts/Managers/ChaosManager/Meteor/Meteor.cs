using UnityEngine;
using System.Collections;

public class Meteor : MonoBehaviour
{
    private Vector3 spawnPoint;
    private Vector3 impactPoint;
    private float travelDuration;

    public void Initialize(Vector3 spawnPoint, Vector3 impactPoint, float travelDuration)
    {
        this.spawnPoint = spawnPoint;
        this.impactPoint = impactPoint;
        this.travelDuration = travelDuration;

        StartCoroutine(MoveMeteor());
    }

    private IEnumerator MoveMeteor()
    {
        float elapsedTime = 0f;

        while (elapsedTime < travelDuration)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(spawnPoint, impactPoint, elapsedTime / travelDuration);
            yield return null;
        }

        // Asegurarse de que el meteoro llegue al punto de impacto final
        transform.position = impactPoint;

        // L�gica de impacto (da�o, efectos, etc.)
        HandleImpact();
    }

    private void HandleImpact()
    {
        // A�adir la l�gica de impacto aqu� (efectos visuales, da�os, etc.)
        Debug.Log("Meteor impact!");
        Destroy(gameObject, 2f); // Destruir el meteoro despu�s de 2 segundos
    }
}