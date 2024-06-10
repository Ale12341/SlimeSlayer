using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Experiencia : MonoBehaviour
{
    [SerializeField] private int lvlMax;
    [SerializeField] private int expBase;
    [SerializeField] private int valorIncremental;

    [SerializeField] private EstadoPersonaje estadoPersonaje;

   

    private float expActualTemp;
    private float expXlvl;

    void Start()
    {
        //estadoPersonaje.Nivel=1;
        expXlvl = expBase;
        ActualizarBarraExperiencia();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            AñadirExp(11f);
        } 
    }

    public void AñadirExp(float cantidad)
    {
        // Comprueba si la cantidad de experiencia a añadir es mayor que 0
        if (cantidad > 0f)
        {
            // Calcula cuánta experiencia se necesita para alcanzar el siguiente nivel
            float expParaSiguienteLevel = expXlvl - expActualTemp;

            // Comprueba si la cantidad de experiencia a añadir es suficiente para alcanzar el siguiente nivel
            if (cantidad >= expParaSiguienteLevel)
            {
                // Si es así, resta la cantidad de experiencia necesaria para alcanzar el siguiente nivel de la cantidad total de experiencia a añadir
                cantidad -= expParaSiguienteLevel;

                ActualizarLevel();

                //Lamada recursiva Para que la experiencia al subir de nivel no se pierda 
                AñadirExp(cantidad);
            }
            else  //Con esto manejo la experiencia que gano con la cual no he subido de nivel 
            {
                expActualTemp += cantidad;
                if (expActualTemp == expXlvl)
                {
                    ActualizarLevel();
                }
            }
        }

        ActualizarBarraExperiencia();
    }


    private void ActualizarLevel()
    {
        if (estadoPersonaje.Nivel < lvlMax)
        {
            estadoPersonaje.Nivel++;
            expActualTemp = 0f;
            expXlvl *= valorIncremental; //Incremento la experienza por nivel 

            //Ponemos aki el incremento de puntos de atributo para que se incremente cada vez que se sube de nivel.
            estadoPersonaje.PuntosDisponibles +=2; //Regular por si esta muy roto
        }
    }

    public void ActualizarBarraExperiencia(){
        UIUI.Instance.ActualizarExp(expActualTemp,expXlvl);
    }

    
  private void OnEnable()
    {
        EnemigoVida.EventoEnemigoDerrotado += RespuestaEnemigoDerrotado;
    }

    private void OnDisable()
    {
        EnemigoVida.EventoEnemigoDerrotado -= RespuestaEnemigoDerrotado;
    }

       private void RespuestaEnemigoDerrotado(float exp)
    {
        AñadirExp(exp);
    }

}
