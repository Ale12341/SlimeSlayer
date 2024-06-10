using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorEnemigo : MonoBehaviour
{
    public static Action<InteraccionEnemigo> EventoEnemigoDetectado;
    public static Action EventoEnemigoPerdido;
    public InteraccionEnemigo EnemigoDetectado { get; private set; }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemigo"))
        {
            EnemigoDetectado = other.GetComponent<InteraccionEnemigo>();
            if (EnemigoDetectado.GetComponent<EnemigoVida>().Life > 0)
            { //Si el enemigo esta con vida 
                EventoEnemigoDetectado?.Invoke(EnemigoDetectado);
            }

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemigo"))
        {
            EventoEnemigoPerdido?.Invoke();
        }
    }
}
