using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeleccionarEnemigo : MonoBehaviour
{
    private Camera camara;
    public static Action<InteraccionEnemigo> EventoEnemigoSeleccionado;
    public static Action EventoEnemigoNOSeleccionado;

    public InteraccionEnemigo EnemigoSeleccionado { get; set; }


    void Start()
    {
        camara = Camera.main;
    }


    void Update()
    {
        SeleccionarAlEnemigo();
    }

    private void SeleccionarAlEnemigo()
    {
        if (Input.GetMouseButtonDown(0))
        {//El click izquierdo es el 0 
            RaycastHit2D hit = Physics2D.Raycast(
            camara.ScreenToWorldPoint(Input.mousePosition),
            Vector2.zero, Mathf.Infinity,
            LayerMask.GetMask("Enemigo")
             );

            if (hit.collider != null)
            {

                EnemigoSeleccionado = hit.collider.GetComponent<InteraccionEnemigo>();//Hacemos la referencia al enemigo que hacemos click 
                EnemigoVida enemigoVida = EnemigoSeleccionado.GetComponent<EnemigoVida>();
                if (enemigoVida.Life > 0)
                {
                    EventoEnemigoSeleccionado?.Invoke(EnemigoSeleccionado);
                }
                else
                {
                    EnemigoLoot enemigoLoot = EnemigoSeleccionado.GetComponent<EnemigoLoot>();
                    LootManager.Instance.MostrarLoot(enemigoLoot);
                }




            }
            else
            {
                EventoEnemigoNOSeleccionado?.Invoke();//Si selecciono otra cosa que no es el enemigo lo deseleccionno
            }
        }
    }
}
