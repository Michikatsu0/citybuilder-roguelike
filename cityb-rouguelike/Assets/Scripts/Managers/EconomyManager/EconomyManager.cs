using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager Instance;
    public float animationDuration = 1.0f; // Duración de la animación en segundos
    public int currentAmountMoney = 1000;
    public TMP_Text moneyText;
    public Animator animator;
    public int hashAnimDenegated = Animator.StringToHash("Denegate");
    private int targetAmount;
    private bool isAnimating = false;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        moneyText.text = currentAmountMoney.ToString();     
    }

    public bool SpendMoneyCheck(int amount)
    {
        if (currentAmountMoney >= amount)
            return true;
        else
        {
            animator.SetTrigger(hashAnimDenegated);
            return false;
        }
    }

    public void AddMoney(int amount)
    {
        StartCoroutine(AnimateMoney(true, amount));
    }

    public IEnumerator AnimateMoney(bool increase, int amount)
    {
        if (isAnimating)
        {
            targetAmount += (increase) ? amount : -amount;
            yield break;
        }

        isAnimating = true;
        int initialAmount = currentAmountMoney;
        targetAmount = (increase) ? currentAmountMoney + amount : currentAmountMoney - amount;
        float elapsedTime = 0f;     

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / animationDuration;
            int currentDisplayAmount = (int)Mathf.Lerp(initialAmount, targetAmount, t);
            currentAmountMoney = currentDisplayAmount;
            if (currentAmountMoney <= 0)
                moneyText.text = 0.ToString();
            else
                moneyText.text = currentAmountMoney.ToString();
            yield return null;
        }

        // Asegurarse de que el valor final sea exacto
        currentAmountMoney = targetAmount;
        if (currentAmountMoney <= 0)
            moneyText.text = 0.ToString();
        else
            moneyText.text = currentAmountMoney.ToString();
        isAnimating = false;
    }

}
