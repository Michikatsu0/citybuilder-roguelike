using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanChaos : BaseChaos
{
    private Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();  
    }

    void Update()
    {
                  
    }

    public override void TriggerChaosEvent()
    {
        Debug.Log("Evento de Océano desencadenado");
        var tempRandom = Random.Range(0, 1);
        animator.SetFloat("RandomAttack", tempRandom);
        animator.SetTrigger("Attack");

    }
}
