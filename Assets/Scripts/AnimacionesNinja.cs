using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionesNinja : MonoBehaviour
{
    private Animator animator;

    //Acceso a la clase MovimientoPlayer
    private MovimientoPlayer movimientoPlayer;

    //Para evitar errores al escribir los nombres que he puesto en unity voy a usar readonly para solo tener que poner estos una vez y reducir los posibles errores.
    //StringToHash es una función de Unity que convierte una cadena de texto, en este caso “X”, en un identificador entero único.
    private readonly int direccionX = Animator.StringToHash("X");
    private readonly int direccionY = Animator.StringToHash("Y");
    private readonly int looser = Animator.StringToHash("Looser");
    [SerializeField] private string layerIdle;
    [SerializeField] private string layerCaminar;
    [SerializeField] private string layerAtacar;
    private PersonajeAtaque personajeAtaque;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        movimientoPlayer = GetComponent<MovimientoPlayer>();
        personajeAtaque = GetComponent<PersonajeAtaque>();
    }

    void Start()
    {

    }


    void Update()
    {

        //Llamamos al metodo actualizarLayer para que se actualize constantemente
        ActualizarLayer();




        //Si nuestro personaje no se esta moviendo no ejecutamos las animaciones ya que lo impedimos con el return.  
        /*
           if (movimientoPlayer.EnMovimiento == false){
            return;
           }
        */








        //Si el personaje esta en movimiento ejecutamos las animaciones
        //Con esto logramos que ele personaje no vuelva a su animacion por defecto que es caminarAbajo si no que se quede en la ultima animacion que hemos ejecutado.
        if (movimientoPlayer.EnMovimiento)
        {
            //Cambiamos la animacion del Player (Ninja) segun la direccion de moviento esto es posible por la configuración que he hecho en los blend tree
            //direccionX e Y son los nombres que le he dado a las variables de los blend tree
            animator.SetFloat(direccionX, movimientoPlayer.DireccionMovimiento.x);
            animator.SetFloat(direccionY, movimientoPlayer.DireccionMovimiento.y);

        }

    }

    private void ActivarLayer(string nombreLayer)
    {

        //Desactivamos todos los layer
        for (int i = 0; i < animator.layerCount; i++)
        {
            //Los layer por asi decirlo tienen como un peso un orden donde 0 es que estan desactivados y 1 estan activados
            animator.SetLayerWeight(i, 0);
        }
        //De esta forma solo activamos el layer cuyo nombre pasamos como parametro
        animator.SetLayerWeight(animator.GetLayerIndex(nombreLayer), 1);
    }


    private void ActualizarLayer()
    {
        if (personajeAtaque.EstaAtacando)
        {
            ActivarLayer(layerAtacar);
        }
        //Si esta en movimiento activamos el layer de caminar para que se vean las animaciones caminando
        else if (movimientoPlayer.EnMovimiento)
        {
            ActivarLayer(layerCaminar);


            //En caso contrario si este no se mueve lo que tenemos que hacer es activar el layer inactivo es decir nuestro layerIdle    
        }
        else
        {
            ActivarLayer(layerIdle);
        }
    }





    private void NinjaLooserRest()
    {
        //Comprobamos si layerIdle esta activado 
        if (animator.GetLayerWeight(animator.GetLayerIndex(layerIdle)) == 1)
        {
            //Cambiamos el valor del parametro para que salte la animacion de muerte que esta en layerIdle
            animator.SetBool(looser, true);
        }else{
            ActivarLayer(layerIdle);
            animator.SetBool(looser, true);
        }
    }

    //Para poder tratar el evento que he hecho de EventoLooser ahora tengo que usar los metodos onEnable y onDisable

    //Este metodo se llama cuando la clase es activada 
    private void OnEnable()
    {
        VidaNinja.EventoLooser += NinjaLooserRest;
    }

    //Este metodo se llama cuando la clase es desactivada 
    private void OnDisable()
    {
        VidaNinja.EventoLooser -= NinjaLooserRest;
    }


    //Hay un problema y es que cuando revive el personaje revive con la animacion de muerto y eso hay que arreglarlo para esto voy a hacer el metodo RevivirKrilin para resetear la animacion y que vuelva a la normalidad
    public void RevivirKrilin()
    {
        ActivarLayer(layerIdle);
        animator.SetBool(looser, false);
    }

}
