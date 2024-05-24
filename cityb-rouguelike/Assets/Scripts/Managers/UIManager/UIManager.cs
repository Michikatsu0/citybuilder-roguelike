using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    
    public List<TMP_Text> text;
    void Start()
    {
        
    }

    //IEnumerator StartCountdown()
    //{
    //    while (true) // Mantenemos la corrutina ejecut�ndose para siempre
    //    {
    //        if (!paused)
    //        {
    //            countdownTimer -= Time.deltaTime;
    //            UpdateCountdownText(countdownTimer);
    //            if (countdownTimer <= 0.0f)
    //            {
    //                regresiveCounterText.text = string.Format("{0:00}:{1:00}:{2:000}", 0f, 0f, 0f);
    //                BadEndGame();

    //                paused = true;
    //            }
    //        }
    //        yield return null;
    //    }
    //}

    // Update is called once per frame

    void Update()
    {
        
    }
}
