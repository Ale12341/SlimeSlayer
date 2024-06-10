using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InteraccionEnemigo : MonoBehaviour
{
    [SerializeField] private GameObject seleccion;
    public void MostrarEnemigoSeleccionado(bool estado)
    {
        seleccion.SetActive(estado);
    }
}
