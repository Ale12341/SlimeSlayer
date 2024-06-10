/*using UnityEngine;

public class TextoCayendo : MonoBehaviour
{
    public float velocidadCaida = 100f; // Velocidad a la que caerá el texto
    private RectTransform rectTransform; // Referencia al RectTransform del objeto de texto

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>(); // Obtenemos la referencia al RectTransform
    }

    void Update()
    {
        // Calculamos la nueva posición del objeto de texto para simular la caída
        float newYPosition = rectTransform.anchoredPosition.y - velocidadCaida * Time.deltaTime;

        // Asignamos la nueva posición al RectTransform
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, newYPosition);

        // Si el objeto de texto ha llegado al final de la pantalla, lo desactivamos
        if (newYPosition < -Screen.height)
        {
            gameObject.SetActive(false);
        }
    }
}
*/

using UnityEngine;

public class TextoCayendo : MonoBehaviour
{
    public float velocidadCaida = 100f; // Velocidad a la que caerá el texto
    private RectTransform rectTransform; // Referencia al RectTransform del objeto de texto

    private float tiempoTranscurrido = 0f; // Tiempo transcurrido desde el inicio

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>(); // Obtenemos la referencia al RectTransform
    }

    void Update()
    {
        // Calculamos la nueva posición del objeto de texto para simular la caída
        float newYPosition = rectTransform.anchoredPosition.y - velocidadCaida * Time.deltaTime;

        // Asignamos la nueva posición al RectTransform
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, newYPosition);

        // Si el objeto de texto ha llegado al final de la pantalla, lo desactivamos
        if (newYPosition < -Screen.height)
        {
            gameObject.SetActive(false);
        }

        // Actualizamos el tiempo transcurrido
        tiempoTranscurrido += Time.deltaTime;

        // Verificamos si han pasado 40 segundos o si se presionó la tecla de espacio
        if (tiempoTranscurrido >= 40f || Input.GetKeyDown(KeyCode.Space))
        {
            SalirDelJuego();
        }
    }

    // Método para salir del juego
    public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego...");

        #if UNITY_EDITOR
        // Detiene el juego en el editor de Unity
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // Cierra la aplicación en una build
        Application.Quit();
        #endif
    }
}
