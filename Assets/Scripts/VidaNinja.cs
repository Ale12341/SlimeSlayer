using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaNinja : Vida  //Asi de facil se hace la herencia
{


    public bool SePuedeCurar => Life < vidaMax;
    public bool NinjaHaMuerto { get; private set; }

    private BoxCollider2D boxCollider2D;


    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    //Sobreescribimos el metodo ActualizarBarraVida
    protected override void ActualizarBarraVida(float vidaActual, float vidaMax)
    {
        //Usando el PATRON SINGLETON uso la clase UIUI para Actualizar la vida 
        UIUI.Instance.ActualizarVida(vidaActual, vidaMax);
    }


    //Lo que hace este metodo es restaurar la vida si esta esta por debajo del maximo y ademas actualiza la barra de vida
    public void RestaurarVida(float cantidad)
    {
        if (NinjaHaMuerto)
        { //Esto lo hago para que cuando el personaje a muero no se pueda curar (devuelvo nada y no pasa a la siguiente parte del codigo goood)
            return;
        }


        if (SePuedeCurar)
        {
            Life += cantidad;
            if (Life > vidaMax)
            {
                Life = vidaMax;
            }
            ActualizarBarraVida(Life, vidaMax);
        }
    }



    //PRUEBA FUNCIONAMIENTO QUITAR VIDA

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            RecibirDamage(20);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            RestaurarVida(20);
        }

    }


    //Evento para controlar la muerte del ninjabu
    public static Action EventoLooser;


    //Sobreescribimos el metodo de ChampDerrotado de la clase padre
    protected override void ChampDerrotado()
    {
        NinjaHaMuerto = true;
        //Desabilitamos las colisiones del personaje una vez a muerto
        boxCollider2D.enabled = false;

        if (EventoLooser != null)
        {
            EventoLooser.Invoke();
        }

    }

    //NO ESTOY SEGURO DE SI QUIERO MANTENERLO PERO ME GUSTARIA TENERLO PARA OTRO TIPO DE PERSONAJES [NICROMANTES O ALGO ASI] SE LO PUEDO PONER A ENEMIGOS GEGE
    public void RevivirKrilin()
    { //No existe mejor nombre para expresar lo que hace.
        NinjaHaMuerto = false;
        boxCollider2D.enabled = true;
        Life = vidaInicial;
        ActualizarBarraVida(Life, vidaMax);
    }


    protected override void Start()
    {
        base.Start(); // Esto se mantiene porq si lo borro ya no hace lo que esta programado en el metodo start de la clase padre
        ActualizarBarraVida(Life, vidaMax);
    }

}
