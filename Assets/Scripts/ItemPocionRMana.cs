using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Pocion Mana")]
public class ItemPocionRMana : InventarioItem
{
     public float RestauracionMana;

      public override bool UtilizarItem()
    {
        bool manaRecargado = false;
        if (Inventario.Instance.Personaje.mana.SePuedeRestaurarMana)
        {
            Inventario.Instance.Personaje.mana.RestaurarManaConPociones(RestauracionMana);
            manaRecargado = true;
        }

        return manaRecargado;
    }
}
