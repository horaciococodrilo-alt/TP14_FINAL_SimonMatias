using UnityEngine;

public class Recolector : MonoBehaviour
{
    public GameManager gameManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coleccionable"))
        {
            gameManager.SumarPunto();
            Destroy(other.gameObject);
        }
    }
}
