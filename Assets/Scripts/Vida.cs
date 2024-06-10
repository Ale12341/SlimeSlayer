using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{

    //[SerializeField] private EstadoPersonaje estado;//Le he agregado EstadoPersonaje para manejar la vida maxima al subir los atributos () Por ahora lo he quitado no he podido hacerlo NO QUITAR LAS PRUEBAS NO EXITOSAS PARA LLEVAR UN CONTROL
    [SerializeField] protected float vidaInicial;
    [SerializeField] protected float vidaMax;



    //Aparte de devolver un valor tambien puede ser modificado (prop)
    public float Life { get; set; }

/*
    //Prueba no exitosa 
    //No se actualiza en medio del juego
    //Para actualizar la vida cuando se suben los atributos lo que he tenido que hacer es convertir vidaMax en una propiedad que devuelve estado.Vida  para que tenga siempre el valor mas reciente
      public float vidaMax
    {
        get
        {
            return estado.Vida;
        }
    }
*/

//Hago esto para poder sobreescribir la clase en una clase hija (en este caso para la clase VidaNinja y futuras....)
    protected virtual void Start()
    {
        Life = vidaInicial;


        /*//Prueba no exitosa 
        Fallo usar el patron de siseño observador para subscribirme y cambiar la vida cuando cambia en estado.Vida no me ha funcionado
        vidaMax = estado.Vida;
        // Suscribirse al evento OnVidaCambiada de estado
        estado.OnVidaCambiada += ActualizarVidaMax;
        */

    }

/*
   //Prueba no exitosa  Fallo usar el patron de siseño observador para subscribirme y cambiar la vida cuando cambia en estado.Vida no me ha funcionado
    // Este método se llamará cuando el evento OnVidaCambiada se dispare
    private void ActualizarVidaMax(float nuevaVida)
    {
        vidaMax = nuevaVida;
    }
*/


   void Update()
    {


    }




    public void RecibirDamage(float cantidad)
    {
        if (Life >= 0f)
        {
            //Actualizamos la vida que tenemos y actualizamos la barra de vida
            Life -= cantidad;
            ActualizarBarraVida(Life, vidaMax);
            if (Life <= 0f)
            {
                Life=0f;//Esto es necesario para no tener vida negativa 
                ActualizarBarraVida(Life, vidaMax);
                ChampDerrotado();
            }
        }

    }

    //Actualizamos la barra de vida usando este metodo 
    protected virtual void ActualizarBarraVida(float vidaActual, float vidaMax)
    {

    }

    protected virtual void ChampDerrotado()
    {

    }

    public static implicit operator Vida(float v)
    {
        throw new NotImplementedException();
    }
}
