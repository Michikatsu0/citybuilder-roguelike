using UnityEngine;

public class Hurricane : MonoBehaviour
{
    private Vector3 direction;
    public float travelDistance;
    public float speed = 100f; // Velocidad del tornado
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (BuilderManager.Instance.selectionBuild) return;
        // Mover el tornado en la dirección especificada
        transform.Translate(direction * speed * Time.deltaTime);

        // Verificar la distancia recorrida
        if (Vector3.Distance(startPosition, transform.position) >= travelDistance)
        {
            Destroy(gameObject); // Destruir el tornado cuando alcance la distancia especificada
        }
    }

    // Método para inicializar la dirección y la distancia de viaje
    public void Initialize(Vector3 direction, float travelDistance)
    {
        this.direction = direction;
        this.travelDistance = travelDistance;
    }
}