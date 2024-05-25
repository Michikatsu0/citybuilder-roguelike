using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDamage : MonoBehaviour
{
    public LayerMask desiredLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject target = other.gameObject;
        if (((1 << other.gameObject.layer) & desiredLayer) != 0)
        {
            BaseBuilding build = target.GetComponent<BaseBuilding>();
            if (build)
            {
                Debug.Log("Damage");
            }
        }
    }
}
