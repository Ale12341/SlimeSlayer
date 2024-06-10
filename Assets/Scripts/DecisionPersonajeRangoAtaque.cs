using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemigo/Decisiones/Jugadore en rango de ataque")]
public class DecisionPersonajeRangoAtaque : EnemigoDecisiones
{
    public override bool Decision(EnemigoControlador enemigoControlador)
    {
        return EnRangoAtaque(enemigoControlador);
    }

    private bool EnRangoAtaque(EnemigoControlador enemigoControlador)
    {
        if (enemigoControlador.ReferenciaPersonaje == null)
        {
            return false;
        }
        float distancia = (enemigoControlador.ReferenciaPersonaje.position - enemigoControlador.transform.position).sqrMagnitude;
        if (distancia < Mathf.Pow(enemigoControlador.rangoAtaqueDeterminado, 2))
        {
            return true;
        }
        return false;
    }
}
