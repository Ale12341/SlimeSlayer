using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Personaje/Arma")]
public class Armas : ScriptableObject
{
    public Sprite armaIcono;
    //public Sprite IconoHabilidad;
    public ArmaType tipoArma;
    public float daño;
    public float mana;//Para poner una cantidad que necesitamos de mana para lanzar ataques magicos
    public float critico;
    public Proyectil proyectil;


}


public enum ArmaType
{
    Melee,
    Rango,
    Magia
}