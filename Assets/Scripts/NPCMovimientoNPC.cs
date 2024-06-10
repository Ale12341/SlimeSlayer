using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovimientoNPC : MovimientoNP
{
    [SerializeField] private DireccionMovimiento direccionMovimiento;

    private readonly int abajo = Animator.StringToHash("abajo");

    protected override void RotarHorizontalPersonajeNP()
    {
        if (direccionMovimiento != DireccionMovimiento.Horizontal)
        {
            return;
        }

        if (PosicionActual.x > ultimaPosicion.x)
        {
            transform.localScale = new Vector3(1, 1, 1);//Vamos a la derecha 
        }

        if (PosicionActual.x < ultimaPosicion.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);//Vamos a la izquierda 
        }
    }

    protected override void RotarVerticalPersonajeNP()
    {
        if (direccionMovimiento != DireccionMovimiento.Vertical)
        {
            return;
        }

        if (PosicionActual.y > ultimaPosicion.y) //Arriba 
        {
            animator.SetBool(abajo, false);
        }
        else
        {
            animator.SetBool(abajo, true);
        }


    }



}
