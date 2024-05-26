using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDamageHitbox : MonoBehaviour
{
    public LayerMask desiredLayer;
    public BaseChaos baseChaos;
    private void OnTriggerEnter(Collider other)
    {
        GameObject target = other.gameObject;
        if (((1 << other.gameObject.layer) & desiredLayer) != 0)
        {
            BaseBuilding build = target.GetComponent<BaseBuilding>();
            if (build)
            {
                Debug.Log("Damage");
                build.TakeDamage(baseChaos.damage);
            }
        }
    }
}
