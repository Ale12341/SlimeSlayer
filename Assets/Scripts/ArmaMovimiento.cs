using UnityEngine;

public class ArmaMovimiento : MonoBehaviour
{
    public Transform personaje; // Referencia al transform del personaje
    public Vector3 offsetLocal; // Posición relativa local del arma respecto al personaje
    public float distanciaDelPersonaje = 1f; // Distancia a la que se mantiene el arma del personaje
    public float duracionAtaque = 0.2f; // Duración del movimiento de ataque
    public float anguloAtaque = 45f; // Ángulo de rotación del arma durante el ataque

    private bool atacando = false; // Flag para controlar si el arma está en proceso de ataque
    private float tiempoInicioAtaque; // Tiempo de inicio del ataque
    private Vector3 direccionMovimiento; // Dirección de movimiento del personaje

    private void Update()
    {
        SeguirPersonaje();
        OrientarHaciaRaton();

        if (Input.GetMouseButtonDown(0) && !atacando) // Si se hace clic izquierdo y no está en proceso de ataque
        {
            IniciarAtaque();
        }

        if (atacando) // Si está en proceso de ataque
        {
            ProcesarAtaque();
        }
    }

    private void SeguirPersonaje()
    {
        // Calcular la posición relativa del arma
        Vector3 offset = offsetLocal.normalized * distanciaDelPersonaje;

        // Ajustar la posición relativa según la dirección del personaje
        if (personaje.localScale.x < 0) // Si el personaje está mirando hacia la izquierda
        {
            offset.x *= -1; // Invierte la dirección en el eje X
        }
        if (personaje.localScale.y < 0) // Si el personaje está mirando hacia abajo
        {
            offset.z *= -1; // Invierte la dirección en el eje Z
        }

        // Aplica el offset a la posición del personaje para obtener la posición del arma
        transform.position = personaje.position + offset;

        // Obtener la dirección de movimiento del personaje
        direccionMovimiento = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f);
    }

    private void OrientarHaciaRaton()
    {
        Vector3 direccionHaciaRaton = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        float angulo = Mathf.Atan2(direccionHaciaRaton.y, direccionHaciaRaton.x) * Mathf.Rad2Deg;
        
        // Limita la rotación en Z a [-180, 180] para evitar problemas de cambio de lado
        angulo = Mathf.Clamp(angulo, -180f, 180f);
        
        // Si el ángulo en Z es mayor a 100, cambia el lado del arma
        if (angulo > 100f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        
        transform.rotation = Quaternion.AngleAxis(angulo, Vector3.forward);
    }

    private void IniciarAtaque()
    {
        atacando = true; // Marca que el arma está en proceso de ataque
        tiempoInicioAtaque = Time.time; // Registra el tiempo de inicio del ataque
    }

    private void ProcesarAtaque()
    {
        float tiempoTranscurrido = Time.time - tiempoInicioAtaque;
        float progreso = Mathf.Clamp01(tiempoTranscurrido / duracionAtaque);

        // Calcula el ángulo de rotación del arma durante el ataque
        float anguloActual = Mathf.Lerp(50f, -88f, progreso);

        // Aplicar la rotación al arma
        transform.localRotation = Quaternion.Euler(0f, 0f, anguloActual);

        // Si el ataque ha terminado
        if (progreso >= 1f)
        {
            atacando = false; // Restablece el estado de ataque
        }
    }
}
