using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum TiposItem{ //Añadir mas tipos 
    Armas,
    Pociones
    //Armaduras??
}
public class InventarioItem : ScriptableObject
{
    //Luego pasar esto con la conexion a la bd :)
  public string ID; 
  public String Nombre;
  public Sprite Icono;
 [TextArea] public String Descripcion;//[TextArea]  te permite poder escribir más es una funcionalidad muy buena y curiosa

public TiposItem tipo;
public bool SePuedeConsumir;
public bool SePuedeUsar;
public bool SePuedeEquipar;
public bool SePuedeAcumular;
public int MaxAcumulacion;


 public int Cantidad; 



//aHORA TENGO UN PROBLEMA QUE ES Q TODOS LOS ITEMS DE LOS MISNMOS EN DIFERENTES SLOTS SE REFERENCIAN ENTOCES xd ARREGLAR ESTO DUPLICANDO ESE OBJETO &6823894524  con esto voy a hacer una copia cada vez y blablavla FUNCIONA PORFIS
public InventarioItem copiar(){
    InventarioItem nueva = Instantiate(this);
    return nueva;
}

//Ahora todos estos metosos los sobreescribo en las calses hijas 
public virtual bool EliminarItem(){
    return true;
}
public virtual bool UtilizarItem(){
    return true;
}

public virtual bool EquiparItem(){
    return true;
}


}
