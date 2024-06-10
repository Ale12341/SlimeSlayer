using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum InteraccionImportante
{
    Misiones,
    Tienda
}

[CreateAssetMenu]
public class NPCConversacion : ScriptableObject
{
    public String nombre;
    public Sprite icono;
    public bool tieneInteraccionImportante;
    public InteraccionImportante interaccionImportante;


    [TextArea] public string principioConversacion;

    [TextArea] public string finalConversacion;

    public Conversacion[] conversacion;
    void Start()
    {

    }


    void Update()
    {

    }

[Serializable]
    public class Conversacion
    {
        [TextArea] public string conversacion;
    }

}
