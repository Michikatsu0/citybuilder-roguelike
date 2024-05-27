using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TriggerDamageHitbox : MonoBehaviour
{
    public LayerMask desiredLayer;
    public float variancePercentage = 0.2f; // Variabilidad del 20%
    public BaseChaos baseChaos;

    private void Start()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (!NaturalChaosManager.Instance.isGameActive) return;
        GameObject target = other.gameObject;
        if (((1 << target.layer) & desiredLayer) != 0)
        {
            
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!NaturalChaosManager.Instance.isGameActive) return;
        GameObject target = other.gameObject;
        if (((1 << target.layer) & desiredLayer) != 0)
        {
            BaseBuilding build = target.GetComponent<BaseBuilding>();
            if (build)
            {
                int damage = CalculateDamage(baseChaos.damage, variancePercentage);
                build.TakeDamage(damage);
                Debug.Log("Damage: " + damage);
            }
        }
    }

    private int CalculateDamage(int baseDamage, float variance)
    {
        float minDamage = baseDamage * (1 - variance);
        float maxDamage = baseDamage * (1 + variance);
        return Mathf.RoundToInt(Random.Range(minDamage, maxDamage));
    }
}