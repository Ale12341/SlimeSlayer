using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Lo primero que voy a hacer es una enum para guardar los tipos de atributos 
public enum TipoAtributo
{
    Inteligencia,
    Fe,
    Vigor,
    Mente,
    Fuerza,
    Aguante
}
public class AtributoBoton : MonoBehaviour
{

    //Ahora hago un evento 
    public static Action<TipoAtributo> EventoAgregarAtributo;
    [SerializeField] private TipoAtributo tipo;

    public void AgregarAtributo()
    {
        EventoAgregarAtributo?.Invoke(tipo);
    }




}
