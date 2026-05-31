using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Referencias de texto")]
    public TextMeshProUGUI textoScore;
    public TextMeshProUGUI textoTimer;

    public void ActualizarScore(int puntos, int total)
    {
        textoScore.text = "Score: " + puntos + " / " + total;
    }

    public void ActualizarTimer(float tiempo)
    {
        // Mostrar segundos enteros, sin decimales
        int segundos = Mathf.CeilToInt(tiempo);
        textoTimer.text = "Tiempo: " + segundos;
    }
}