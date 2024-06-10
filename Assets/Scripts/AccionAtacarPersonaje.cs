using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemigo/Acciones/Ataque Player")]
public class AccionAtacarPersonaje : Enemigo
{
    public override void Hacer(EnemigoControlador enemigoControlador)
    {
        Atacar(enemigoControlador);
    }
    private void Atacar(EnemigoControlador enemigoControlador)
    {
        if (enemigoControlador.SePuedeAtacar() == false) //eSTO FUNCIONA
        {
            return;
        }

        if (enemigoControlador.ReferenciaPersonaje.GetComponent<VidaNinja>().NinjaHaMuerto)
        {
            return;
        }

        if (enemigoControlador.JugadorEnRangoAtaque(enemigoControlador.rangoAtaqueDeterminado))
        {
            if (enemigoControlador.tipodeAtaque == TipoAtaque.embestida)
            {
                enemigoControlador.ataqueEmbestida(enemigoControlador.Daño);//Si esta en rango de ataque un clarisimo cabezazo

            }
            else
            {
                enemigoControlador.AtaqueMelee(enemigoControlador.Daño);
            }

            enemigoControlador.ActualizarTiempoAtaque();
            //Despues de cada ataque tengo que actualizar su tiempo de ataque porq si no no hago nada este no cambia no puedo volver a atacar 
        }
    }
}
