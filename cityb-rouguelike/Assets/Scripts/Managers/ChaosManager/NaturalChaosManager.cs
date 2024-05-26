using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChaosManager : MonoBehaviour
{
    public TMP_Text timerText; // Asigna el componente TextMeshPro desde el Inspector
    public float timeLimit = 360f; // Límite de tiempo en segundos (6 minutos)

    public float minInterval = 10f; // Intervalo mínimo en segundos
    public float maxInterval = 30f; // Intervalo máximo en segundos
    public AnimationCurve intervalCurve; // Curva de animación para la variabilidad del intervalo

    private float remainingTime;
    private bool isGameActive = true; // Estado del juego
    private bool paused = false; // Bandera para pausar el juego

    private List<BaseChaos> chaosEvents;

    void Start()
    {
        remainingTime = timeLimit;
        StartCoroutine(StartCountdown());
        StartCoroutine(GenerateChaosEvents()); // Iniciar generación de eventos de caos

        // Inicializar referencias a los componentes de caos
        chaosEvents = new List<BaseChaos>(GetComponentsInChildren<BaseChaos>());

        Debug.Log("Chaos Manager started. Countdown and chaos events coroutines started.");
    }

    private IEnumerator StartCountdown()
    {
        while (true) // Mantenemos la corrutina ejecutándose para siempre
        {
            if (!paused)
            {
                remainingTime -= Time.deltaTime; // Decrementamos el tiempo restante
                FormatTextUpdateCountdown(remainingTime); // Actualizamos el texto del temporizador

                if (remainingTime <= 0.0f)
                {
                    timerText.text = "end of turn: " + string.Format("{0:0}:{1:00}:{2:000}", 0f, 0f, 0f);
                    GameWon(); // Lógica para cuando el tiempo se acaba y el jugador gana
                    paused = true;
                }
            }
            yield return null; // Esperar un frame antes de continuar el bucle
        }
    }

    private void FormatTextUpdateCountdown(float timeRemaining)
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        int milliseconds = Mathf.FloorToInt((timeRemaining * 1000) % 1000);

        // Actualizar el texto TMP con el tiempo restante formateado
        timerText.text = "end of turn: " + string.Format("{0:0}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }

    private void GameWon()
    {
        isGameActive = false;
        timerText.text = "You Win!";
        Debug.Log("Game won. Countdown ended.");
        // Lógica adicional para cuando el jugador gana
    }

    public void GameOver()
    {
        isGameActive = false;
        paused = true; // Pausar el juego cuando el jugador muere
        timerText.text = "Game Over";
        Debug.Log("Game over. Player died.");
        // Lógica adicional para cuando el jugador muere
    }


    private IEnumerator GenerateChaosEvents()
    {
        while (isGameActive)
        {
            // Calcular un intervalo aleatorio utilizando una curva de animación para variabilidad
            float t = Random.value;
            float curveValue = intervalCurve.Evaluate(t);
            float randomInterval = minInterval + curveValue * (maxInterval - minInterval);
            Debug.Log("Waiting for " + randomInterval + " seconds before triggering next chaos event.");
            yield return new WaitForSeconds(randomInterval);

            // Elegir un evento de caos aleatorio
            int chaosEventIndex = Random.Range(0, chaosEvents.Count);
            Debug.Log("Triggering chaos event: " + chaosEvents[chaosEventIndex].GetType().Name);
            chaosEvents[chaosEventIndex].TriggerChaosEvent();
            //check life


        }
    }
}

