using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Personaje : MonoBehaviour
{

    public VidaNinja vidaNinja { get; private set; }
    public AnimacionesNinja animacionesNinja { get; private set; } //Lo de private es para que solo se pueda modificar en esta clase. 
    public Mana mana { get; private set; }
    public PersonajeAtaque personajeAtaque{ get; set; }
    public Experiencia PersonajeExperiencia { get; set; }
    
    

    private void Awake()
    {
        vidaNinja = GetComponent<VidaNinja>();
        animacionesNinja = GetComponent<AnimacionesNinja>();
        mana = GetComponent<Mana>();
        personajeAtaque=GetComponent<PersonajeAtaque>();
    }

    public void RestaurarPersonaje()
    {
        vidaNinja.RevivirKrilin();
        animacionesNinja.RevivirKrilin();
        mana.RestablecerMana(); //(Amanaza al codigo)Pedazzo de cabron no te borres solo que te mato


    }



    //Escuchamos el evento del boton de los atributos con enable y disable

    private void OnEnable()
    {
        AtributoBoton.EventoAgregarAtributo += AtributoRespuesta;
    }

    private void OnDisable()
    {

        AtributoBoton.EventoAgregarAtributo -= AtributoRespuesta;
    }



    //Variable referenciada a EstadoPersonaje
    [SerializeField] private EstadoPersonaje estadoPersonaje;


    private void AtributoRespuesta(TipoAtributo tipoAtributo) //Lo que voy a hacer en este metodo es gestionar los diferentes casos
    {
        //Solo puedo aumentar los atributos si tengo puntos suficientes.  
        if(estadoPersonaje.PuntosDisponibles<=0){
            return; //No hago nada ya que no tenemos puntos
        }

        switch(tipoAtributo)
        {
            case TipoAtributo.Fuerza:
            estadoPersonaje.AñadirBonusXFuerza();
            break;

              case TipoAtributo.Inteligencia:
               estadoPersonaje.AñadirBonusXInteligencia();
            break;

              case TipoAtributo.Mente:
               estadoPersonaje.AñadirBonusXMente();
            break;

              case TipoAtributo.Fe:
               //Quiero que este sea un atributo meme y que no sirva para nada o hacerlo comico y que te de todo (Pensar que hacer con el)
            break;

              case TipoAtributo.Aguante:
               estadoPersonaje.AñadirBonusXAguante();
            break;

              case TipoAtributo.Vigor:
               estadoPersonaje.AñadirBonusXVigor();
            break;
        }

        estadoPersonaje.PuntosDisponibles--;//Quito un punto cada vez que lo uso.
    }

}
