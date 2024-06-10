
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public enum KEQW // Esto va a aser para controlar las interaciones con el inventario
{
    Equipar,
    Utilizar,
    Despojar,
    Clikear
}
public class InventarioSlots : MonoBehaviour
{

    [SerializeField] private Image icono;//Cuidado al impoortar q se lia el cacharro
    [SerializeField] private GameObject fondocantidad;
    [SerializeField] private TextMeshProUGUI textoCantidad;
    public int Index { get; set; }

    private Button boton;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void Awake()
    {
        boton = GetComponent<Button>();
    }

    public void UTilizarItem()
    {
        if (Inventario.Instance.inventarioItemsCopia[Index] != null)
        {//Si en esa casilla tenemos un item es decir no es null 
            EventoInventario?.Invoke(KEQW.Utilizar, Index);
        }
    }

    public void EquiparItem()
    {
        if (Inventario.Instance.inventarioItemsCopia[Index] != null)
        {
            EventoInventario?.Invoke(KEQW.Equipar, Index);
        }

    }

        public void DespojarItem()
    {
        if (Inventario.Instance.inventarioItemsCopia[Index] != null)
        {
            EventoInventario?.Invoke(KEQW.Despojar, Index);
        }

    }

    public static Action<KEQW, int> EventoInventario;

    public void Actualizar(InventarioItem item, int cantidad)
    {
        icono.sprite = item.Icono;
        textoCantidad.text = cantidad.ToString();
    }

    public void ONUI(bool lentejas)
    {
        icono.gameObject.SetActive(lentejas);

        fondocantidad.gameObject.SetActive(lentejas);

    }

    public void Clik()
    {
        EventoInventario?.Invoke(KEQW.Clikear, Index);
    }

    public void SlotSeleccionado()
    {//Este metodo es solo estetico para mantener el boton de un iten seleccionado todo el tiempo para que no se vea raro al utilizarlo 
        boton.Select();
    }



}
