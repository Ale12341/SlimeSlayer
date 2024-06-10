using System.Collections;
using UnityEngine;

public class PersonajeDash : MonoBehaviour
{
    private MovimientoPlayer movimientoPlayer; //Ponerle particulas cada vez que hace un dash para que se aprecie mas
    private float gravedad;
    private bool puedeDash = true;
    private bool estaEnCooldown = false;

    [SerializeField] private float aumentoDeDash = 3f; // Incrementamos el aumento de dash
    [SerializeField] private float tiempoParaDash = 1f;
    [SerializeField] private float cooldown = 1f; // Tiempo de cooldown entre dases

    private void Awake()
    {
        movimientoPlayer = GetComponent<MovimientoPlayer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && puedeDash && !estaEnCooldown)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        puedeDash = false;
        estaEnCooldown = true;

        // Calculamos la posición de dash
        Vector2 posicionDeDash = (Vector2)transform.position + (movimientoPlayer.DireccionMovimiento.normalized * aumentoDeDash);

        // Teletransportamos al jugador a la posición de dash
        transform.position = posicionDeDash;

        // Esperamos un tiempo antes de permitir el dash nuevamente
        yield return new WaitForSeconds(tiempoParaDash);

        estaEnCooldown = false;

        // Iniciamos el cooldown
        StartCoroutine(StartCooldown());
    }

    private IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        puedeDash = true;
    }
}
