using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Pocion Vida")]
public class ItemPocionRVida : InventarioItem //ScriptableObject como heredan de InventarioItem no necesito poner ScriptableObject ya que el padre ya lo tiene :::)))
{
    public float RestauracionVida;



    public override bool UtilizarItem()
    {
        bool seHaCurado = false;
        if (Inventario.Instance.Personaje.vidaNinja.SePuedeCurar)
        {
            Inventario.Instance.Personaje.vidaNinja.RestaurarVida(RestauracionVida);
            seHaCurado = true;
        }

        return seHaCurado;
    }

}
