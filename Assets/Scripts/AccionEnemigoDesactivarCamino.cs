using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Enemigo/Acciones/Desactivar movimiento por defecto")]
public class AccionEnemigoDesactivarCamino : Enemigo
{
       public override void Hacer(EnemigoControlador enemigoControlador)
    {
        if (enemigoControlador.EnemigoMovimiento == null)
        {
            return;
        }

        enemigoControlador.EnemigoMovimiento.enabled = false; //lo desactivo
    }
}
