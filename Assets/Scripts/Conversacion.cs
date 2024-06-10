using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Conversacion : Singleton<Conversacion>
{
    [SerializeField] private GameObject conversacion;
    [SerializeField] private TextMeshProUGUI nombreNPC;
    [SerializeField] private TextMeshProUGUI textoNPC;

    public NPChablarC nPChablar { get; set; }

    private Queue<string> secuenciaConverascion; // voy  ausar Queue para la secuencia de mi conversacion 

    public bool textoAnimado;

    public bool despedida;

    void Start()
    {
        secuenciaConverascion = new Queue<string>();
        despedida=false;
    }

    // Update is called once per frame
    void Update()
    {
        if (nPChablar == null)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ConfigurarPConversacion(nPChablar.conversacion);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (despedida)
            {
                AbrirCerrarPConversacion(false);
                despedida = false;
                return;
            }

            if(nPChablar.conversacion.tieneInteraccionImportante){
                UIUI.Instance.AbrirPanelInteraccion(nPChablar.conversacion.interaccionImportante);
                AbrirCerrarPConversacion(false);
                return;
            }

            if (textoAnimado)
            {
                ContinuarConversacion();
            }
        }


    }

    /*NO me convence pero no esta mal manejarlo todo con una tecla 
        bool primeraVez = true;

        void Update()
        {
            if (nPChablar == null)
            {
                return;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (primeraVez)
                {
                    ConfigurarPConversacion(nPChablar.conversacion);
                    primeraVez = false;
                }
                else if (textoAnimado)
                {
                    ContinuarConversacion();
                }
                else
                {
                    primeraVez = true;
                }
            }
        }
    */

    public void AbrirCerrarPConversacion(bool estado)
    {
        conversacion.SetActive(estado);
    }

    private void ConfigurarPConversacion(NPCConversacion nPCConversacion)
    {

      
        AbrirCerrarPConversacion(true);
        nombreNPC.text = nPCConversacion.nombre;
        CargarConversaciones(nPCConversacion);
        MostrarTextoAnimado(nPCConversacion.principioConversacion);

    }

    private void CargarConversaciones(NPCConversacion nPCConversacion)
    {
        if (nPCConversacion.conversacion == null || nPCConversacion.conversacion.Length <= 0) return;


        for (int i = 0; i < nPCConversacion.conversacion.Length; i++)
        {
            secuenciaConverascion.Enqueue(nPCConversacion.conversacion[i].conversacion);
        }

    }


    //Esta animacion es muy facil consiste en acer un efecto de escritura poiendo letra por letra en vez de ponerlo todo dell tiron queda guapo no ??
    private IEnumerator AnimarTexto(string texto)
    {
        textoAnimado = false;
        textoNPC.text = "";
        char[] letras = texto.ToCharArray();
        for (int i = 0; i < letras.Length; i++)
        {
            textoNPC.text += letras[i];
            yield return new WaitForSeconds(0.03f);
        }

        textoAnimado = true;
    }

    private void MostrarTextoAnimado(string texto)
    {
        StartCoroutine(AnimarTexto(texto));
    }

    private void ContinuarConversacion()
    {
          if(secuenciaConverascion.Count==0)
        {
            string textodespedida= nPChablar.conversacion.finalConversacion;
              MostrarTextoAnimado(textodespedida);
              despedida=true;
              return;
        }
        string siguienteTexto = secuenciaConverascion.Dequeue();
        MostrarTextoAnimado(siguienteTexto);

    }
}
