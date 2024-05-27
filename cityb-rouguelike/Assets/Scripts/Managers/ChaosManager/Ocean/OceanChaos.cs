using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanChaos : BaseChaos
{
    public Animator animator;

    public override void TriggerChaosEvent()
    {
        Debug.Log("Evento de Océano desencadenado");
        float tempRandom = Random.Range(0f, 1f);
        animator.SetFloat("RandomAttack", tempRandom);
        animator.SetTrigger("Attack");
    }
}
