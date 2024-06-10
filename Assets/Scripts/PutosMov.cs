using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutosMov : MonoBehaviour
{
    [SerializeField] private Vector3[] puntos;
    public Vector3[] Puntos => puntos;
    private bool game;
    private void OnDrawGizmos()
    {

        if (!game && transform.hasChanged)
        {
            posicion = transform.position;
        }

        //Con esto voy a dibujar los puntos de movimiento que van a segir tanto mis enemigos como npc
        if (puntos == null || puntos.Length <= 0)
        {
            return;
        }


        for (int i = 0; i < puntos.Length; i++)
        {
            //Voy a usar los Gizmos para ayudarme a ver en la escena mis puntos de movimiento estos solo pueden ser usados en OnDrawGizmos
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(puntos[i] + posicion, 0.5f);
            if (i < puntos.Length - 1)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(puntos[i] + posicion, puntos[i + 1] + posicion);
            }
        }
    }

    public Vector3 posicion { get; set; }

    private void Start()
    {
        posicion = transform.position;
    }


    public Vector3 PosicionMovimiento(int index)
    {
        return posicion + puntos[index];
    }


}
