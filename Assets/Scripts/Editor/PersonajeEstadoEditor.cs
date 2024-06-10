using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EstadoPersonaje))]
public class PersonajeEstadoEditor : Editor //Esto lo hago para modificar unity y hacer mipropia herramienta para resetear los estados  GRACIAS YOUTUBE SIEMPRE CONFIE
{

    public EstadoPersonaje EstadoTarget => target as EstadoPersonaje; //Obtengo el objetivo para editarlo y lo transformo a EstadoPersonaje

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Resetear Valores")) //Con esto creo un boton llamado Resetear Valores
        {
            EstadoTarget.ResetearValores(); //LLamo a resetear valores de EstadoPersonaje para que estos vuelvan a su valor por defecto
        }
    }

}
