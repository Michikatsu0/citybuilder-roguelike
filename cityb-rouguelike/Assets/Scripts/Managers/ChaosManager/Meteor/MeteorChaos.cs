using UnityEngine;

public class MeteorChaos : BaseChaos
{
    public Transform center;
    public float radius;
    public GameObject meteorPrefab;
    public float spawnDistance = 50f; // Distancia segura para spawnear el meteoro
    public float impactAngleX = 45f;
    public float impactAngleY = 135f;

    void Start()
    {
        if (center == null) // Ensure the center is set
            center = transform;
    }

    public override void TriggerChaosEvent()
    {
        // Lógica específica del evento de caos del meteorito
        Debug.Log("Evento de Meteoro desencadenado");
        Vector3 impactPoint = GetRandomPointInRadius(center.position, radius);
        Vector3 spawnPoint = CalculateSpawnPoint(impactPoint);

        // Instanciar el meteorito y hacer que caiga
        SpawnAndMoveMeteor(spawnPoint, impactPoint);
    }

    private Vector3 GetRandomPointInRadius(Vector3 center, float radius)
    {
        float angle = Random.Range(0f, Mathf.PI * 2);
        float distance = Random.Range(0f, radius);
        return new Vector3(
            center.x + distance * Mathf.Cos(angle),
            center.y,
            center.z + distance * Mathf.Sin(angle)
        );
    }

    private Vector3 CalculateSpawnPoint(Vector3 impactPoint)
    {
        // Convertir ángulos a radianes
        float angleXRad = impactAngleX * Mathf.Deg2Rad;
        float angleYRad = impactAngleY * Mathf.Deg2Rad;

        // Calcular la dirección del spawn basado en los ángulos dados
        Vector3 direction = new Vector3(Mathf.Cos(angleXRad), Mathf.Sin(angleYRad), Mathf.Sin(angleXRad));

        // Calcular el punto de spawn a la distancia segura
        return impactPoint + direction.normalized * spawnDistance;
    }

    private void SpawnAndMoveMeteor(Vector3 spawnPoint, Vector3 impactPoint)
    {
        GameObject meteorInstance = Instantiate(meteorPrefab, spawnPoint, Quaternion.identity);
        Meteor meteorScript = meteorInstance.GetComponent<Meteor>();

        if (meteorScript != null)
        {
            meteorScript.Initialize(spawnPoint, impactPoint, 2f); // Pasar el tiempo de viaje deseado
        }
    }

    private void OnDrawGizmos()
    {
        if (center != null)
            NaturalChaosManager.DrawWireDisc(Color.red, center.position, center.up, radius);
    }
}