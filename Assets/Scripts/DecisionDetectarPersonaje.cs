using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemigo/Decisiones/DetectarPersonaje")]
public class DecisionDetectarPersonaje : EnemigoDecisiones
{
    public override bool Decision(EnemigoControlador enemigoControlador)
    {
        return DetectarPersonaje(enemigoControlador);
    }

    private bool DetectarPersonaje(EnemigoControlador enemigoControlador)
    {
        bool detectado = false;
        Collider2D personajeDetectado = Physics2D.OverlapCircle(enemigoControlador.transform.position,
         enemigoControlador.rangoDeteccion,
         enemigoControlador.personajeLayerMask);

        if (personajeDetectado != null)
        {
            enemigoControlador.ReferenciaPersonaje = personajeDetectado.transform;
            detectado = true;
        }
        
        return detectado;
    }
}
