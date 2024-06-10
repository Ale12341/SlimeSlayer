using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Enemigo/Acciones/Activar movimiento por defecto")]
public class AccionEnemigoActivarCamino : Enemigo
{
    public override void Hacer(EnemigoControlador enemigoControlador)
    {
        if (enemigoControlador.EnemigoMovimiento == null)
        {
            return;
        }

        enemigoControlador.EnemigoMovimiento.enabled = true; //lo activo
    }
}
