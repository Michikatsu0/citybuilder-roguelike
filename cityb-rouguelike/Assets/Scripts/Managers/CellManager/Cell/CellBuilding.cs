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
    public float timeDuration, curveValue, fadeValue;
    public bool building = false, bought = false, recollect = false, doOnce = true;
    public int priceSlot = 2000;
    public Vector3 position;
    public Slider buildProgress;
    public Material[] materials;
    public BaseBuilding currentBuilding;
    public List<Image> imagesFade = new List<Image>();
    public List<TMP_Text> textsFade = new List<TMP_Text>();
    public AnimationCurve animationFadeCurve, animationFadeCurve1;

    private MeshRenderer meshRendererCell;
    
    private float timer;
    private bool isFading = false;

    void Start()
    {
        position = transform.position;
        meshRendererCell = GetComponent<MeshRenderer>();
        FadeUIManager();
        StartFade();
    }

    public void StartFade()
    {
        isFading = true;
        timer = 0f;
    }

    void Update()
    {
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
               
                
                if (!recollect && doOnce)
                {
                    Fade(animationFadeCurve, out float curveValue1);
                    ApplyAlphaFadeImage(curveValue1, 5);
                    ApplyAlphaFadeImage(curveValue1, 6);
                    ApplyAlphaFadeText(curveValue1, 3);

                }
                if (!recollect)
                {

                    Fade(animationFadeCurve, out float curveValue);
                    ApplyAlphaFadeImage(curveValue, 0);
                    ApplyAlphaFadeText(curveValue, 0);
                    ApplyAlphaFadeImage(curveValue, 4);

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
                
                //ApplyAlphaFadeImage(curveValue, 4);
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
        currentBuilding.cellBuilding = this;
        building = true;
    }

    public void DestroyBuild(BaseBuilding buildPosition)
    {
        StartCoroutine(DestroyEffect(buildPosition));
    }

    private IEnumerator DestroyEffect(BaseBuilding buildPosition)
    {
        bought = false;
        // Effect
        yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(0.5f);
        Destroy(buildPosition.gameObject);
    }
}