using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemigo/Estado")]
public class EnemigoEstado : ScriptableObject
{
    
    public Enemigo[] accionesEnemigo;
    public Transicion[] transiciones;



    private void RealizarAcciones(EnemigoControlador enemigoControlador)
    {
        //No quiero coninuar ejecutando el metodo si tengo esta mierda vacia
        if (accionesEnemigo == null || accionesEnemigo.Length <= 0)
        {
            return;
        }


        for (int i = 0; i < accionesEnemigo.Length; i++)
        {
            accionesEnemigo[i].Hacer(enemigoControlador);
        }



    }
    //Etse metodo se describe con el nombre es para realizar las transiciones del enemigo
    private void RealizarTransiones(EnemigoControlador enemigoControlador)
    {

        if (accionesEnemigo == null || accionesEnemigo.Length <= 0)
        {
            return;
        }

        for (int i = 0; i < transiciones.Length; i++)
        {
            bool decision = transiciones[i].decisiones.Decision(enemigoControlador);
            //Cambiamos el estado del enemigo segun la decision
            if (decision)
            {
                enemigoControlador.CambiarEstado(transiciones[i].enemigoEstadoVerdadero);
            }
            else
            {
                enemigoControlador.CambiarEstado(transiciones[i].enemigoEstadoFalso);

            }
        }

    }

    public void EjecutarEstado(EnemigoControlador enemigoControlador)
    {
        RealizarAcciones(enemigoControlador);
        RealizarTransiones(enemigoControlador);


    }
    

}
