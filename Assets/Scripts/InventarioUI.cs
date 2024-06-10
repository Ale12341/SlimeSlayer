using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventarioUI : Singleton<InventarioUI>
{

  public InventarioSlots slotSeleccionado { get; private set; }
  [SerializeField] InventarioSlots inventarioSlots; // Este es el prefab de mi ranura/casilla/slot/....
  [SerializeField] private Transform Slot;

  List<InventarioSlots> slotsDisponibles = new List<InventarioSlots>();

  [SerializeField] private GameObject InventarioInfo;
  [SerializeField] private Image icono;
  [SerializeField] private TextMeshProUGUI nombre;
  [SerializeField] private TextMeshProUGUI texto;



  private void RespuestaEvento(KEQW tipo, int index)
  {
    if (tipo == KEQW.Clikear)
    {
      ActualizarInventarioInfo(index);
    }
  }

  private void ActualizarInventarioInfo(int index)
  {
    if (Inventario.Instance.inventarioItemsCopia[index] != null) // Si tenemos un item en ese lugar / 
    {
      icono.sprite = Inventario.Instance.inventarioItemsCopia[index].Icono;//Le pongo su icono 
      nombre.text = Inventario.Instance.inventarioItemsCopia[index].Nombre;//Le pongo su Nombre 
      texto.text = Inventario.Instance.inventarioItemsCopia[index].Descripcion;//Le pongo su info 
      InventarioInfo.SetActive(true);
    }
    else
    {
      InventarioInfo.SetActive(false);

    }
  }

  private void OnEnable()
  {
    InventarioSlots.EventoInventario += RespuestaEvento;
  }

  private void OnDisable()
  {
    InventarioSlots.EventoInventario -= RespuestaEvento;
  }

  void Start()
  {
    InicializarInventario();
  }


  void Update()
  {
    //(Eres retrasado??) Recordatorio para un retrasado tu codigo esta bien no te sueles equivocar pero mira un poco donde haces las llamadas para luego no tener que estar como un bobo media hora buscando





    SlotSeleccionado();
  }

  private void InicializarInventario()
  {
    for (int i = 0; i < Inventario.Instance.NumeroDeSlots; i++)
    {
      //Ponemos el prefact en el recuadro que hemos delimitado para el y lo añadimos a la lista.
      InventarioSlots nuevo = Instantiate(inventarioSlots, Slot);
      nuevo.Index = i;
      slotsDisponibles.Add(nuevo);
    }
  }


  public void AñadirItem(InventarioItem item, int cantidad, int index)//Con este metodo relleneamos la interfaz del inventario
  {
    InventarioSlots inventarioSlots = slotsDisponibles[index];
    if (item != null)
    {
      inventarioSlots.ONUI(true);
      inventarioSlots.Actualizar(item, cantidad);
    }
    else
    {
      inventarioSlots.ONUI(false);
    }
  }


  private void SlotSeleccionado()
  {
    GameObject gameObject = EventSystem.current.currentSelectedGameObject; //Esto me regresa el objeto que esta seleccionado actualmente
    if (gameObject == null)//Si esta vacio como siempre no me interesa continuar y lo paro aki
    {
      return;
    }

    //Si el objeto es un InventarioSlots, asigna ese componente a la variable slotSeleccionado.
    InventarioSlots slots = gameObject.GetComponent<InventarioSlots>();

    if (slots != null)
    {
      slotSeleccionado = slots;
    }



  }


  public void UtilizarItem()
  {
    if (slotSeleccionado != null) //Si el slot no esta vacio podemos usarlo
    {
      slotSeleccionado.UTilizarItem();
      slotSeleccionado.SlotSeleccionado();
    }
  }


  public void EquiparItem()
  {
    if (slotSeleccionado != null)
    {
      slotSeleccionado.EquiparItem();
      slotSeleccionado.SlotSeleccionado();
    }
  }

   public void DespojarItem()
  {
    if (slotSeleccionado != null)
    {
      slotSeleccionado.DespojarItem();
      slotSeleccionado.SlotSeleccionado();
    }
  }

}
