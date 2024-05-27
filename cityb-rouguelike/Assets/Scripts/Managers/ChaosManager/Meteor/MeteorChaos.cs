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
        // L�gica espec�fica del evento de caos del hurac�n
        Debug.Log("Evento de Meteoro desencadenado");
        // Implementar la l�gica espec�fica del caos del hurac�n
    }

    private void OnDrawGizmos()
    {
        if (center != null)
            NaturalChaosManager.DrawWireDisc(Color.red, center.position, center.up, radius);
    }
}
