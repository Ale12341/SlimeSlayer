using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica si el objeto que colisionó tiene la etiqueta "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
          

            // Cambiar a la escena "final"
            SceneManager.LoadScene("final");
        }
    }
}
