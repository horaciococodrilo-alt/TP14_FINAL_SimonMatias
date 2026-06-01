using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Referencias de texto")]
    public TextMeshProUGUI textoScore;
    public TextMeshProUGUI textoTimer;

    [Header("Pantallas de fin de juego")]
    public GameObject panelWin;
    public GameObject panelGameOver;

    public void ActualizarScore(int puntos, int total)
    {
        textoScore.text = "Score: " + puntos + " / " + total;
    }

    public void ActualizarTimer(float tiempo)
    {
        int segundos = Mathf.CeilToInt(tiempo);
        textoTimer.text = "Tiempo: " + segundos;
    }

    public void MostrarPantallaWin()
    {
        panelWin.SetActive(true);
    }

    public void MostrarPantallaGameOver()
    {
        panelGameOver.SetActive(true);
    }
}