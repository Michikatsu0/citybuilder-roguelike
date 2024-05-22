using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITriggerBuildUI : MonoBehaviour
{
    public Animator animator;
    private int hashEnable = Animator.StringToHash("Enable");
    public LayerMask desiredLayer;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & desiredLayer) != 0)
            animator.SetBool(hashEnable, true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & desiredLayer) != 0)
            animator.SetBool(hashEnable, false);
    }
}
