using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlManager : MonoBehaviour
{
    [SerializeField] private Personaje personaje;
    [SerializeField] private Transform puntoRespawn;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.AltGr))
        {

            if (personaje.vidaNinja.NinjaHaMuerto)
            {
                personaje.transform.localPosition = puntoRespawn.position;
                personaje.RestaurarPersonaje();
            }
        }
    }
}
