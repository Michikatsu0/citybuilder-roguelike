using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NaturalChaosManager : MonoBehaviour
{
    public static NaturalChaosManager Instance;

    public List<GameObject> panelsGame = new List<GameObject>();

    public TMP_Text timerText; // Asigna el componente TextMeshPro desde el Inspector
    public float timeLimit = 360f; // Límite de tiempo en segundos (6 minutos)

    public Vector2 interval;
    private float remainingTime;
    public bool isGameActive = true; // Estado del juego
    private bool paused = false; // Bandera para pausar el juego

    public List<BaseChaos> chaosEvents = new List<BaseChaos>();
    public GameObject panelUpgrades;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        remainingTime = timeLimit;
        StartCoroutine(StartCountdown());
        StartCoroutine(GenerateChaosEvents()); // Iniciar generación de eventos de caos
        StartCoroutine(PauseEveryMinute()); // Iniciar la corrutina para pausar cada minuto

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
                    isGameActive = false;
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
        panelsGame[1].SetActive(true);
        // Lógica adicional para cuando el jugador gana
    }

    public void GameOver()
    {
        isGameActive = false;
        paused = true; // Pausar el juego cuando el jugador muere
        timerText.text = "Game Over";
        panelsGame[0].SetActive(true);
        Debug.Log("Game over. Player died.");
        // Lógica adicional para cuando el jugador muere
    }

    private IEnumerator GenerateChaosEvents()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(Random.Range(interval.x, interval.y));

            if (!isGameActive)
            {
                break; // Salir de la corrutina si el juego no está activo
            }

            // Elegir un evento de caos aleatorio
            int chaosEventIndex = Random.Range(0, chaosEvents.Count);
            Debug.Log("Triggering chaos event: " + chaosEvents[chaosEventIndex].GetType().Name);
            chaosEvents[chaosEventIndex].TriggerChaosEvent();

            // Verificar si todas las CellBuildings están destruidas
            if (CellBuildingsManager.Instance.AreAllBuildingsDestroyed())
            {
                GameOver();
                break;
            }
        }
    }

    private IEnumerator PauseEveryMinute()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(60f);
            if (isGameActive)
            {
                paused = true;
                isGameActive = false;
                panelUpgrades.SetActive(true); // Mostrar el panel de mejoras
                yield return new WaitUntil(() => !paused); // Esperar hasta que el juego sea reanudado
            }
        }
    }

    public void ResumeGame()
    {
        paused = false;
        isGameActive = true;
        panelUpgrades.SetActive(false); // Ocultar el panel de mejoras
        StartCoroutine(GenerateChaosEvents()); // Reanudar generación de eventos de caos
    }

    //public static void DrawWireDisc(Color color, Vector3 center, Vector3 normal, float radius)
    //{
    //    UnityEditor.Handles.color = color;
    //    UnityEditor.Handles.DrawWireDisc(center, normal, radius);
    //}
}