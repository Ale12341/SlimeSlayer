using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Items/Arma")]
public class ItemArma : InventarioItem
{
    public Armas arma;
    public override bool EquiparItem() //Solo puedo equipar un item si no hay ninguno equipado
    {
        if (UIArma.Instance.ArmaEquipada != null)
        {
            return false;
        }

        UIArma.Instance.EquiparArma(this);
        return true;
    }

    public override bool EliminarItem()
    {
       if (UIArma.Instance.ArmaEquipada == null)
        {
            return false;
        }

        UIArma.Instance.DespojarArma();
        return true;
    }
}
