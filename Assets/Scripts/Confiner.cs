using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Confiner : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCamera camara;


    //Voy a manejar las colisiones entre los Confiner para saber cuando entra y sale el personaje y habilitar o desabilitar las distintas CinemachineVirtualCamera
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            camara.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            camara.gameObject.SetActive(false);
        }
    }

}
