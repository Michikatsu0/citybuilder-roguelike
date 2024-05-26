using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BrandView : MonoBehaviour
{
    [SerializeField] private float secondsNextBrand = 1.5f;
    [SerializeField] private List<RawImage> images = new List<RawImage>();
    private Coroutine brandRoutine;
    private bool brandReady = false;
    void Start()
    {
        foreach (var image in images)
            image.gameObject.SetActive(false);
    }

    public void StartBrand()
    {
        if (brandReady) return;
        brandReady = true;
        brandRoutine = StartCoroutine(ShowBrandsCoroutine());
    }

    private IEnumerator ShowBrandsCoroutine()
    {
        foreach(var image in images)
        {
            yield return new WaitForSeconds(secondsNextBrand);
            image.gameObject.SetActive(true);
        }
    }
}
