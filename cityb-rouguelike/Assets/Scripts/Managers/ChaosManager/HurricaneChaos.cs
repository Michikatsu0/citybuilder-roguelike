using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurricaneChaos : BaseChaos
{
    // Start is called before the first frame update
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
        Debug.Log("Evento de Huracán desencadenado");
        // Implementar la lógica específica del caos del huracán
    }
}
