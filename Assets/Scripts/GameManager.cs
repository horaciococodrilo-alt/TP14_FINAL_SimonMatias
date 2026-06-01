using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Configuracion")]
    public float tiempoTotal = 60f;
    public int puntajeMaximo = 8;

    [Header("Referencias")]
    public UIManager uiManager;

    private int puntajeActual = 0;
    private float tiempoRestante;
    private bool juegoTerminado = false;

    void Start()
    {
        // Reactivar el tiempo por si quedo congelado de una partida anterior
        Time.timeScale = 1f;

        tiempoRestante = tiempoTotal;
        uiManager.ActualizarScore(puntajeActual, puntajeMaximo);
        uiManager.ActualizarTimer(tiempoRestante);
    }

    void Update()
    {
        // Si el juego termino, solo escuchar la tecla R para reiniciar
        if (juegoTerminado)
        {
            if (Input.GetKeyDown(KeyCode.R))
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            return;
        }

        // Restar tiempo
        tiempoRestante -= Time.deltaTime;
        uiManager.ActualizarTimer(tiempoRestante);

        // Derrota: se acabo el tiempo
        if (tiempoRestante <= 0f)
        {
            tiempoRestante = 0f;
            uiManager.ActualizarTimer(tiempoRestante);
            PerderJuego();
        }
    }

    public void SumarPunto()
    {
        if (juegoTerminado) return;

        puntajeActual++;
        uiManager.ActualizarScore(puntajeActual, puntajeMaximo);
        Debug.Log("Punto sumado. Total: " + puntajeActual);

        // Victoria
        if (puntajeActual >= puntajeMaximo)
            GanarJuego();
    }

    void GanarJuego()
    {
        juegoTerminado = true;
        uiManager.MostrarPantallaWin();
        Time.timeScale = 0f;   // congelar el juego
        Debug.Log("WIN - Juntaste todas las netbooks!");
    }

    void PerderJuego()
    {
        juegoTerminado = true;
        uiManager.MostrarPantallaGameOver();
        Time.timeScale = 0f;   // congelar el juego
        Debug.Log("GAME OVER - Se acabo el tiempo");
    }
}