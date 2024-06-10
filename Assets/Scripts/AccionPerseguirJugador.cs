using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemigo/Acciones/SeguirPersonaje")]
public class AccionPerseguirJugador : Enemigo
{
    public override void Hacer(EnemigoControlador enemigoControlador)
    {
        SeguirPersonaje(enemigoControlador);
    }

    private void SeguirPersonaje(EnemigoControlador enemigoControl)
    {
        if (enemigoControl.ReferenciaPersonaje == null)
        {
            return;
        }
        if (enemigoControl.ReferenciaPersonaje.GetComponent<VidaNinja>().NinjaHaMuerto)
        {
            return;
        }

        Vector3 direccionPersonaje = enemigoControl.ReferenciaPersonaje.position - enemigoControl.transform.position;//apunta del enemigo al personaje
        float distancia = direccionPersonaje.magnitude;
        Vector3 direccion = direccionPersonaje.normalized;

        if (distancia >= 1.4f)//Ajustar para que no quede raro 
        {
            enemigoControl.transform.Translate(direccion * enemigoControl.VelocidadMovimiento * Time.deltaTime);
        }
    }
}

//Tienes q arreglar q cuando andas hacia el enemigo este se va hacia atras.