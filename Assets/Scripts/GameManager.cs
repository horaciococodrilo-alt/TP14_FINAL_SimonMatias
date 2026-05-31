using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Configuracion")]
    public float tiempoTotal = 60f;     // segundos de la partida
    public int puntajeMaximo = 8;       // cuantas netbooks hay que juntar

    [Header("Referencias")]
    public UIManager uiManager;

    private int puntajeActual = 0;
    private float tiempoRestante;
    private bool juegoTerminado = false;

    void Start()
    {
        tiempoRestante = tiempoTotal;
        uiManager.ActualizarScore(puntajeActual, puntajeMaximo);
        uiManager.ActualizarTimer(tiempoRestante);
    }

    void Update()
    {
        if (juegoTerminado) return;

        // Restar tiempo
        tiempoRestante -= Time.deltaTime;
        uiManager.ActualizarTimer(tiempoRestante);

        // Condicion de derrota: se acabo el tiempo
        if (tiempoRestante <= 0f)
        {
            tiempoRestante = 0f;
            uiManager.ActualizarTimer(tiempoRestante);
            juegoTerminado = true;
            Debug.Log("GAME OVER - Se acabo el tiempo");
        }
    }

    // El Recolector llama a esto cuando agarra una netbook
    public void SumarPunto()
    {
        if (juegoTerminado) return;

        puntajeActual++;
        uiManager.ActualizarScore(puntajeActual, puntajeMaximo);
        Debug.Log("Punto sumado. Total: " + puntajeActual);

        // Condicion de victoria
        if (puntajeActual >= puntajeMaximo)
        {
            juegoTerminado = true;
            Debug.Log("WIN - Juntaste todas las netbooks!");
        }
    }
}