using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Estado")] //Este atributo permite crear instancias de EstadoPersonaje desde el menú de Unity.
public class EstadoPersonaje : ScriptableObject  //Es una clase en Unity que permite almacenar grandes cantidades de datos independientes del script que se ejecuta en el juego.
{
    public float Poder = 5f;
    public float Nivel =0f;
    public float Defensa = 2f;
    public float Velocidad = 4f;
    public float Mana = 100f;
    public float Vida = 100f;
    //Porcentaje del Critico de 0 a 100
    [Range(0f, 100f)] public float PorcentajeCritico;



    //Borrar ?¿ Esto no lonecesito para nada??
    /* public int Inteligencia;
     public int Fe;
     public int Vigor;
     public int Aguante;
     public int Fuerza;
     public int Mente;
     //
     */



    public int PuntosDisponibles;




    public void ResetearValores() //Pongo los valores por defecto
    {
        Poder = 5f;
        Nivel = 1f;
        Defensa = 2f;
        Velocidad = 4f;
        Mana = 69f;
        Vida = 42f;
        PorcentajeCritico = 0.0f;
    }



    //Bonus de los atributos
    public void AñadirBonusXFuerza()
    {
        Poder += 2;
        PorcentajeCritico += 0.07f;
    }

    public void AñadirBonusXInteligencia()
    {
        Mana += 3;
        Defensa += 1f;
        Poder += 1f;
    }
    public void AñadirBonusXAguante()
    {
        Velocidad += 2f;
        Defensa += 3;
    }

    public void AñadirBonusXMente()
    {
        Mana += 5;
        PorcentajeCritico += 0.03f;
    }

    public void AñadirBonusXVigor()
    {
        Defensa += 6;


    }



    public void AñadirBonusXArma(Armas arma)
    {
        Poder += arma.daño;
        PorcentajeCritico += arma.critico;
    }

    public void QuitarBonusXArma(Armas arma)
    {
        Poder -= arma.daño;
        PorcentajeCritico -= arma.critico;
    }



    /*
     //Prueba no exitosa  
     Fallo usar el patron de siseño observador para subscribirme y cambiar la vida cuando cambia en estado.Vida no me ha funcionado
       // Declarar un delegado para el evento
        public delegate void VidaCambiada(float nuevaVida);
        // Declarar el evento
        public event VidaCambiada OnVidaCambiada;

        private float _vida;

        public float VidaC
        {
            get { return _vida; }
            set
            {
                _vida = value;
                // Disparar el evento cuando Vida cambia
                OnVidaCambiada?.Invoke(_vida);
            }
        }

    */

}
