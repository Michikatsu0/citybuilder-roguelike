using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class test : MonoBehaviour
{
    private float money = 0;
    public float limit = 200;
    public bool isGeneratingMoney = true;

    void Update()
    {
        if (isGeneratingMoney)
        {
            if (money < limit)
            {
                money +=  20 * Time.deltaTime;
                Debug.Log("Dinero acumulado: " + money);
            }
            else
            {
                isGeneratingMoney = false;
                Debug.Log("Generación de dinero detenida. Límite alcanzado: " + money);
            }
        }
    }
}