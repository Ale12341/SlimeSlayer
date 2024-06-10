using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUI : Singleton<UIUI>
{



    //Imagen y texto de la UI
    [SerializeField] private UnityEngine.UI.Image vidaPlayer;
    [SerializeField] private UnityEngine.UI.Image manaPlayer;
    [SerializeField] private UnityEngine.UI.Image expPlayer;
    [SerializeField] private TextMeshProUGUI vidaTexto;
    [SerializeField] private TextMeshProUGUI manaTexto;


    //Estados
    [SerializeField] private TextMeshProUGUI estadoPoderTxt;
    [SerializeField] private TextMeshProUGUI estadoDefensaTxt;
    [SerializeField] private TextMeshProUGUI estadoCriticoTxt;
    [SerializeField] private TextMeshProUGUI estadoVelocidadTxt;
    [SerializeField] private TextMeshProUGUI estadoNivelTxt;
    [SerializeField] private TextMeshProUGUI estadoVidaTxt;
    [SerializeField] private TextMeshProUGUI estadoManaTxt;


    [SerializeField] private TextMeshProUGUI atributosDisponiblesTxt;



    [SerializeField] private GameObject panelInventario;
    [SerializeField] private GameObject panelMisiones;
    [SerializeField] private GameObject panelMisionesPersonaje;
    [SerializeField] private GameObject panelEstado;
    [SerializeField] private EstadoPersonaje estado;
    [SerializeField] private GameObject panelConfig;



    [SerializeField] private TextMeshProUGUI moneda;





    private float vidaActual;
    private float vidaMax;

    private float manaActual;
    private float manaMax;

    private float expActual;
    private float expNewLevel;


    void Update()
    {
        ActualizarUI();
        ActualizarPanelEstados();


        //Cerrar las UI de una manera muchisimo mas comoda para el usuario
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            panelEstado.SetActive(false);
            panelInventario.SetActive(false);
            panelConfig.SetActive(false);
            panelMisionesPersonaje.SetActive(false);
            panelMisiones.SetActive(false);
        }

    }

    //Metodo para actualizar vida :))
    public void ActualizarVida(float vActual, float vMax)
    {
        vidaActual = vActual;
        vidaMax = vMax;
    }

    private void ActualizarUI()
    {
        vidaPlayer.fillAmount = Mathf.Lerp(vidaPlayer.fillAmount, vidaActual / vidaMax, 10f * Time.deltaTime);

        vidaTexto.text = $"{vidaActual}/{vidaMax}";
        //vidaTexto.text =vidaActual+"/"+vidaMax; No queta tan perrona 



        manaPlayer.fillAmount = Mathf.Lerp(manaPlayer.fillAmount, manaActual / manaMax, 10f * Time.deltaTime);

        manaTexto.text = $"{manaActual}/{manaMax}";


        expPlayer.fillAmount = Mathf.Lerp(expPlayer.fillAmount, expActual / expNewLevel, 10f * Time.deltaTime);

        moneda.text = MonedasManager.Instance.MonedasTotales.ToString();

    }

    public void ActualizarMana(float mActual, float mMax)
    {
        manaActual = mActual;
        manaMax = mMax;
    }



    public void ActualizarExp(float expA, float expR)
    {
        expActual = expA;
        expNewLevel = expR;
    }


    private void ActualizarPanelEstados()
    {
        if (panelEstado.activeSelf == false)//Compuebo si la ventana de estados esta activa o no (Si se esta viendo o no)
        {
            return; //Si el panel no esta activo es decir Estados no esta visible no hacemos nada
        }

        estadoPoderTxt.text = estado.Poder.ToString();
        estadoDefensaTxt.text = estado.Defensa.ToString();

        estadoCriticoTxt.text = $"{estado.PorcentajeCritico}%";  // Este es un procentaje 

        estadoVelocidadTxt.text = estado.Velocidad.ToString();
        estadoManaTxt.text = estado.Mana.ToString();
        estadoVidaTxt.text = estado.Vida.ToString();
        estadoNivelTxt.text = estado.Nivel.ToString();
        atributosDisponiblesTxt.text = estado.PuntosDisponibles.ToString(); //Actualizamos los puntos disponobles que tenemos 
    }





    public void AbrirCerrarPanelEstadisticas()
    {
        panelEstado.SetActive(!panelEstado.activeSelf);
    }

    public void AbrirCerrarPanelInventario()
    {
        panelInventario.SetActive(!panelInventario.activeSelf);
    }



    public void AbrirCerrarPanelMisiones()
    {
        panelMisiones.SetActive(!panelMisiones.activeSelf);
    }

    public void AbrirPanelInteraccion(InteraccionImportante tipoInteraccion)
    {
        switch (tipoInteraccion)
        {
            case InteraccionImportante.Misiones:
                AbrirCerrarPanelMisiones();
                break;
            case InteraccionImportante.Tienda:
                break;
        }
    }

    public void AbrirCerrarPanelMisionesPersonaje()
    {
        panelMisionesPersonaje.SetActive(!panelMisionesPersonaje.activeSelf);
    }

    public void AbrirCerrarPanelConfig()
    {
        panelConfig.SetActive(!panelConfig.activeSelf);
    }

}
