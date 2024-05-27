using UnityEngine;
using System.Collections;

public class Meteor : MonoBehaviour
{
    public ParticleSystem fireParticles;
    public MeshRenderer[] meshRenderers;
    public float repetitions1, repetitions2, delay1, delay2;
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
        if (!NaturalChaosManager.Instance.isGameActive) yield return null;

        float elapsedTime = 0f;

        while (elapsedTime < travelDuration)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(spawnPoint, impactPoint, elapsedTime / travelDuration);
            yield return null;
        }

        // Asegurarse de que el meteoro llegue al punto de impacto final
        transform.position = impactPoint;

        // Lógica de impacto (daño, efectos, etc.)
        HandleImpact();
    }

    private void HandleImpact()
    {
        // Añadir la lógica de impacto aquí (efectos visuales, daños, etc.)
        Debug.Log("Meteor impact!");
        fireParticles.Play();
        StartCoroutine(DestroyEffect(meshRenderers));
    }

    private IEnumerator DestroyEffect(MeshRenderer[] meshRenderers)
    {
        // Effect
        for (int i = 0; i < repetitions1; i++)
        {
            EnableMeshRenderer(meshRenderers, false);
            yield return new WaitForSeconds(delay1);
            EnableMeshRenderer(meshRenderers, true);
            yield return new WaitForSeconds(delay1);
        }

        for (int i = 0; i < repetitions2; i++)
        {
            EnableMeshRenderer(meshRenderers, false);
            yield return new WaitForSeconds(delay2);
            EnableMeshRenderer(meshRenderers, true);
            yield return new WaitForSeconds(delay2);
        }

        EnableMeshRenderer(meshRenderers, false);
        Destroy(gameObject);
    }


    private void EnableMeshRenderer(MeshRenderer[] meshRenderer, bool enable)
    {
        foreach (var mr in meshRenderer)
            mr.enabled = enable;
    }
}