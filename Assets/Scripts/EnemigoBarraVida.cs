using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemigoBarraVida : MonoBehaviour
{

    [SerializeField] private Image barraVida;
    private float vidaActual;
    private float vidaMax;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        barraVida.fillAmount = Mathf.Lerp(barraVida.fillAmount, vidaActual / vidaMax, 10f * Time.deltaTime);
    }

    public void ModificarSalud(float pVidaActual, float pVidaMax)
    {
        vidaActual = pVidaActual;
        vidaMax = pVidaMax;
    }
}
