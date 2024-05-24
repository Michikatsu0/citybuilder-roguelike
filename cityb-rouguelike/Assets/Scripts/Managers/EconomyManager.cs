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

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        moneyText.text = currentAmountMoney.ToString();     
    }

    void Update()
    {
        
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
        int initialAmount = currentAmountMoney;
        
        float elapsedTime = 0f;

        if (!increase)
        {
            targetAmount = currentAmountMoney - amount;
            currentAmountMoney = targetAmount;
        }
        else
        {
            targetAmount = currentAmountMoney + amount;
            currentAmountMoney = initialAmount;
        }

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / animationDuration;
            int currentDisplayAmount = (int)Mathf.Lerp(initialAmount, targetAmount, t);
            moneyText.text = currentDisplayAmount.ToString();
            yield return null;
        }

        // Asegurarse de que el valor final sea exacto
        currentAmountMoney = targetAmount;
        moneyText.text = currentAmountMoney.ToString();
    }

}
