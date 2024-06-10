/*using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemigoLoot : MonoBehaviour
{
    [Header("Exp")] 
    [SerializeField] private float expGanada;
    
    [Header("Loot")] 
    [SerializeField] private DropItem[] lootDisponible;

    private List<DropItem> lootSeleccionado = new List<DropItem>();
    public List<DropItem> LootSeleccionado => lootSeleccionado;
    public float ExpGanada => expGanada;

    private void Start()
    {
        SeleccionarLoot();
    }

    private void SeleccionarLoot()
    {
        foreach (DropItem item in lootDisponible)
        {
            float probabilidad = Random.Range(0, 100);
            if (probabilidad <= item.PorcentajeDrop)
            {
                lootSeleccionado.Add(item);
            }
        }
    }
}*/

using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemigoLoot : MonoBehaviour
{
    [Header("Exp")] 
    [SerializeField] private float expGanada;
    
    [Header("Loot")] 
    [SerializeField] private DropItem[] lootDisponible;

    [Header("Referencias")] 
    [SerializeField] private GameObject rastros;  // Referencia al objeto "rastros"

    private List<DropItem> lootSeleccionado = new List<DropItem>();
    public List<DropItem> LootSeleccionado => lootSeleccionado;
    public float ExpGanada => expGanada;

    private void Start()
    {
        SeleccionarLoot();
    }

    private void SeleccionarLoot()
    {
        foreach (DropItem item in lootDisponible)
        {
            float probabilidad = Random.Range(0, 100);
            if (probabilidad <= item.PorcentajeDrop)
            {
                lootSeleccionado.Add(item);
            }
        }

        // Si no hay loot seleccionado, desactivar rastros
        if (lootSeleccionado.Count == 0)
        {
            DesactivarRastros();
        }
    }

    public void DesactivarRastros()
    {
        if (rastros != null)
        {
            rastros.SetActive(false);
        }
    }

    public void ComprobarYDesactivarRastros()
    {
        if (lootSeleccionado.Count == 0 || TodosItemsRecogidos())
        {
            DesactivarRastros();
        }
    }

    private bool TodosItemsRecogidos()
    {
        foreach (var item in lootSeleccionado)
        {
            if (!item.ItemRecogido)
            {
                return false;
            }
        }
        return true;
    }
}
