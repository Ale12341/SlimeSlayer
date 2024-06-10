using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPChablarC : MonoBehaviour
{
    [SerializeField] private GameObject btnHablar;
    [SerializeField] private NPCConversacion conversacionNPC;

    public NPCConversacion conversacion => conversacionNPC;

    void Start()
    {

    }

    void Update()
    {

    }

    //Cara culo esto es para saber si el personaje esta en rango para hablae se activa la imagen para que presiones la tecla e y hables con la cosa
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            btnHablar.SetActive(true);
            Conversacion.Instance.nPChablar = this; //Ascociamos al npc con el que hemos entrado en contacto para cargarlo en la propiedad nPChablar
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            btnHablar.SetActive(false);
            Conversacion.Instance.nPChablar = null; // Cuando esta fuera de rango lo tengo q poner a null porq si no por defeco esta el que se ha cargado y sigue estando aunque no este en rango 
            Conversacion.Instance.AbrirCerrarPConversacion(false);
        }
    }
}
