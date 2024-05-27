using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CellBuilding : MonoBehaviour
{
    public ParticleSystem effect;
    public float timeDuration, curveValue, fadeValue, delay1, delay2;
    public bool building = false, bought = false, recollect = false, doOnce = true;
    public int priceSlot = 2000, repetitions1 = 5, repetitions2 = 10;
    public Vector3 position;
    public Slider buildProgress;
    public Material[] materials;
    public BaseBuilding currentBuilding;
    public List<Image> imagesFade = new List<Image>();
    public List<TMP_Text> textsFade = new List<TMP_Text>();
    public AnimationCurve animationFadeCurve, animationFadeCurve1;

    private MeshRenderer meshRendererCell;
    
    private float timer;
    private bool isFading = false, isFadingOut = false;

    void Start()
    {
        position = transform.position;
        meshRendererCell = GetComponent<MeshRenderer>();
        FadeUIManager();
        StartFade(); 
        StartFadeOut();
    }

    public void StartFade()
    {
        isFading = true;
        timer = 0f;
    }

    public void StartFadeOut()
    {
        isFadingOut = true;
        timer = 0f;
    }

    void Update()
    {
        if (!NaturalChaosManager.Instance.isGameActive) return;
        FadeUIManager();
    }

    public void FadeUIManager()
    {
        if (BuilderManager.Instance.selectionBuild) return;

        textsFade[0].text = priceSlot.ToString();

        if (bought)
        {
            meshRendererCell.material = materials[0];
            //apagar text valor e imagen
            if (isFading)
            {
                Fade(animationFadeCurve, out float curveValue);
                ApplyAlphaFadeImage(curveValue, 0);
                ApplyAlphaFadeText(curveValue, 0);
                ApplyAlphaFadeImage(curveValue, 4);
            }

            if (isFadingOut)
            {
                if (!recollect && doOnce)
                {
                    Fade(animationFadeCurve, out float curveValue1);
                    ApplyAlphaFadeImage(curveValue1, 5);
                    ApplyAlphaFadeImage(curveValue1, 6);
                    ApplyAlphaFadeText(curveValue1, 3);
                }
            }

            ApplyAlphaFadeImage(curveValue, 1);
            ApplyAlphaFadeText(curveValue, 1);

            if (recollect)
            {
                ApplyAlphaFadeImage(curveValue, 5);
                ApplyAlphaFadeImage(curveValue, 6);
                ApplyAlphaFadeText(curveValue, 3);
            }
        }
        else
        {
            meshRendererCell.material = materials[1];

            if (isFading)
            {
                Fade(animationFadeCurve1, out float curveValue1);
                ApplyAlphaFadeImage(curveValue1, 0);
                ApplyAlphaFadeText(curveValue1, 0);
                ApplyAlphaFadeImage(curveValue1, 5);
                ApplyAlphaFadeImage(curveValue1, 6);
                ApplyAlphaFadeText(curveValue1, 3);

                Fade(animationFadeCurve, out float curveValue);
                ApplyAlphaFadeImage(curveValue, 1);
                ApplyAlphaFadeText(curveValue, 1);
            }

            ApplyAlphaFadeImage(curveValue, 0);
            ApplyAlphaFadeText(curveValue, 0);
            ApplyAlphaFadeImage(curveValue, 4);
        }
    }

    public void Fade(AnimationCurve animationCurve, out float curveValue)
    {
        timer += Time.deltaTime;
        float t = timer / timeDuration;
        curveValue = animationCurve.Evaluate(timeDuration * t);

        if (timer >= 1f / timeDuration)
        {
            isFading = false;
            doOnce = false;
        }
    } 

    public void ApplyAlphaFadeImage(float alphaValue, int index)
    {
        Color color = imagesFade[index].color;
        color.a = Mathf.Lerp(1f, 0f, alphaValue);
        imagesFade[index].color = color;
    }

    public void ApplyAlphaFadeText(float alphaValue, int index)
    {
        Color color = textsFade[index].color;
        color.a = Mathf.Lerp(1f, 0f, alphaValue);
        textsFade[index].color = color;
    }

    public void SetBuild(BaseBuilding buildPosition)
    {
        //if (bought) return;
        buildPosition.gameObject.transform.position = position;
        currentBuilding = buildPosition;
        textsFade[1].text = currentBuilding.healthPoints.ToString();
        currentBuilding.cellBuilding = this;
        building = true;
    }

    public void DestroyBuild(MeshRenderer[] meshRenderer, BaseBuilding buildPosition)
    {
        StartCoroutine(DestroyEffect(meshRenderer, buildPosition));
    }

    private IEnumerator DestroyEffect(MeshRenderer[] meshRenderers, BaseBuilding buildPosition)
    {
        // Effect
        for (int i = 0; i < repetitions1; i++)
        {
            EnableMeshRenderer(meshRenderers, false);
            yield return new WaitForSeconds(delay1);
            EnableMeshRenderer(meshRenderers, true);
            yield return new WaitForSeconds(delay1);
        }

        for (int i = 0; i < repetitions2; i++)
        {
            EnableMeshRenderer(meshRenderers, false);
            yield return new WaitForSeconds(delay2);
            EnableMeshRenderer(meshRenderers, true);
            yield return new WaitForSeconds(delay2);
        }

        EnableMeshRenderer(meshRenderers, false);

        building = false;
        bought = false;
        recollect = false;
        doOnce = true;
        currentBuilding = null;
        Destroy(buildPosition.gameObject);
    }


    private void EnableMeshRenderer(MeshRenderer[] meshRenderer, bool enable)
    {
        foreach (var mr in meshRenderer)
            mr.enabled = enable;
    }
}