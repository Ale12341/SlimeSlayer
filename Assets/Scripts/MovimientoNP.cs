using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DireccionMovimiento
{
    Vertical,
    Horizontal
}
public class MovimientoNP : MonoBehaviour
{
    //[SerializeField] private DireccionMovimiento direccionMovimiento;
    [SerializeField] protected float velocidad;


    protected int indexPuntoActual;
    protected PutosMov putosMov;

    protected Vector3 ultimaPosicion;

    protected Animator animator;

    public Vector3 PosicionActual => putosMov.PosicionMovimiento(indexPuntoActual);
    private void Start()
    {
        putosMov = GetComponent<PutosMov>();
        indexPuntoActual = 0;
        animator = GetComponent<Animator>(); 
    }

    private void MoverPersonajeNP()
    {
        transform.position = Vector3.MoveTowards(transform.position, PosicionActual, velocidad * Time.deltaTime);
    }

    private void Update()
    {
        MoverPersonajeNP();
        RotarHorizontalPersonajeNP();
        RotarVerticalPersonajeNP();
        if (HaLlegadoAlPunto())
        {
            ActualizarIndex();
        }
    }

    private bool HaLlegadoAlPunto()
    {
        bool heLlegao = false;
        float distaciaPunto = (transform.position - PosicionActual).magnitude; //magnitude es la longitud es decir la distacia cateto ignorante de verdad como no sabes que es la magnitud
        if (distaciaPunto < 0.1f)
        {
            ultimaPosicion = transform.position;
            heLlegao = true;
        }
        return heLlegao;
    }

    private void ActualizarIndex()
    {
        if (indexPuntoActual == putosMov.Puntos.Length - 1)
        {//Si el punto actual llega al ultimo lo reseteo a cero LOKETE
            indexPuntoActual = 0;
        }
        else
        {
            if (indexPuntoActual < putosMov.Puntos.Length - 1)
            {
                indexPuntoActual++;
            }

        }
    }

    protected virtual void RotarHorizontalPersonajeNP()
    {
    
    }


     protected virtual void RotarVerticalPersonajeNP()
    {
     
    }

}
