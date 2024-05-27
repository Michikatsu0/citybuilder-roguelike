using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorChaos : BaseChaos
{
    public Transform center;
    public float radius;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void TriggerChaosEvent()
    {
        // Lógica específica del evento de caos del huracán
        Debug.Log("Evento de Meteoro desencadenado");
        // Implementar la lógica específica del caos del huracán
    }

    private void OnDrawGizmos()
    {
        if (center != null)
            NaturalChaosManager.DrawWireDisc(Color.red, center.position, center.up, radius);
    }
}
