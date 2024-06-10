using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : Singleton<Inventario>
{

    [SerializeField] private int numeroSlot;
    public int NumeroDeSlots => numeroSlot;

    [SerializeField] private InventarioItem[] inventarioItems;

    public InventarioItem[] inventarioItemsCopia => inventarioItems;


    [SerializeField] private Personaje personaje;
    public Personaje Personaje => personaje;
    [SerializeField] private InventarioAlmazen inventarioAlmacen;
     private AdminMySQL adminMySQL;

    
    void Start()
{
    adminMySQL = FindObjectOfType<AdminMySQL>();
    StartCoroutine(EsperarConexionYcargarInventario());
}
IEnumerator EsperarConexionYcargarInventario()
{
    // Espera hasta que la conexión a la base de datos se haya establecido
    while (!adminMySQL.ConexionExitosa)
    {
        yield return null; // Espera un frame
    }

    // Cargar el inventario una vez que la conexión esté establecida
    CargarInventario();
}

    

/*    void Start()
    {
        inventarioItems = new InventarioItem[numeroSlot];//Hago que el inventario item sea del tamaño que hemos puesto anteriormente para que coincida con el tamaño del inventario
        adminMySQL = FindObjectOfType<AdminMySQL>();
        if (adminMySQL.ConexionExitosa)
        {
            CargarInventario();
        }

    }*/

    void Update()
    {

    }

    

    public void AñadirItem(InventarioItem item, int cantidad)
    {
        //ALERTA PARA SUBNORMALES TEN CUIDADO CON LOS RETURN QUE PONGAS PORQ SI ERES RETRASADO Y NO TE DAS CUENTA ESTAS 3 AÑOS BUSCANDO UN ERROR Q LO HACES DEBITO A TU GRAN SUBNORMALIDAD ATENTAMENTE UN SUBNORMAL.

        //Esto solo sirve si ya hemos coguido ese item y lo tenemos en el inventario
        List<int> index = ExisteItemInventario(item.ID);





        if (item.SePuedeAcumular)
        {
            if (index.Count > 0)
            {
                for (int i = 0; i < index.Count; i++)
                {
                    if (inventarioItems[index[i]].Cantidad < item.MaxAcumulacion)
                    {
                        inventarioItems[index[i]].Cantidad += cantidad;
                        if (inventarioItems[index[i]].Cantidad > item.MaxAcumulacion)
                        {
                            //Ahora en este caso regulo si me paso al añadir la cantidad de items es decir si puedo tener max 69 y añado 80 los 11 restantes meterlos en otra ranura (slots)
                            int diferencia = inventarioItems[index[i]].Cantidad - item.MaxAcumulacion;
                            inventarioItems[index[i]].Cantidad = item.MaxAcumulacion;
                            AñadirItem(item, diferencia);//Por medio de recursividad volvemos a usar AñadirItem para añadir lo que nos ha sobrado 
                        }

                        //Actualizamos la UI con el item
                        InventarioUI.Instance.AñadirItem(item, inventarioItems[index[i]].Cantidad, index[i]);
                        return;
                    }
                }
            }
        }
        //Aqui se maneja cuando se mete de mas y cuando se mete un nuevo item
        /*if (cantidad <= 0)
        {
            return;
        }*/
        if (cantidad > item.MaxAcumulacion)
        {
            AñadirItemNuevo(item, item.MaxAcumulacion);
            cantidad -= item.MaxAcumulacion;
            AñadirItem(item, cantidad);
        }
        else
        {
            AñadirItemNuevo(item, cantidad);
        }

    }

    //Este es un metodo que me va a servir para saber si el item que acabamos de coger ya esta en el inventario y asi lo añadimos en esa casilla o en una nueva
    //Esta logica es muy facil tengo 18 casillas ()()()Si en la primera casilla tengo pociones de mana las siguientes tamb se guarda en esa y si me topo con una de vida se pone en la siguiente casilla que no este ocupada FAcilito?¿
    private List<int> ExisteItemInventario(string id)
    {
        List<int> index = new List<int>();

        for (int i = 0; i < inventarioItems.Length; i++)
        {
            if (inventarioItems[i] != null)
            {
                if (inventarioItems[i].ID == id)
                {
                    index.Add(i);
                }
            }

        }
        return index;
    }

    public void AñadirItemNuevo(InventarioItem item, int cantidad) //Esto es para añadir un item por primera vez 
    {
        for (int i = 0; i < inventarioItems.Length; i++)
        {
            if (inventarioItems[i] == null)
            {
                inventarioItems[i] = item.copiar();
                inventarioItems[i].Cantidad = cantidad;


                //Actualizamos la UI con el item
                InventarioUI.Instance.AñadirItem(item, cantidad, i);


                return;//Si no ponemos esto se nos va a poner el item en todo el inventaruio

            }

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

    private void RespuestaEvento(KEQW tipo, int index)
    {
        switch (tipo)
        {
            case KEQW.Despojar:
                DespojarItem(index);
                break;
            case KEQW.Utilizar:
                UtilizarItem(index);
                break;
            case KEQW.Equipar:
                EquiparItem(index);
                break;

        }
    }


    private void UtilizarItem(int index)
    {
        if (inventarioItems[index] == null)
        {
            return;
        }
        if (inventarioItems[index].UtilizarItem())//Si se ha podido utilizar el item 
        {
            //Eliminamos el item que se usa
            EliminarItemUsado(index);

        }

    }


    private void EliminarItemUsado(int index)
    {
        inventarioItems[index].Cantidad--;
        if (inventarioItems[index].Cantidad <= 0) //Si usamos todos los items de un slot lo que pasa esq la cantida la pongo en 0 y pasa a ser nulo
        {
            inventarioItems[index].Cantidad = 0;
            inventarioItems[index] = null;
            InventarioUI.Instance.AñadirItem(null, inventarioItems[index].Cantidad, index);

        }
        else //En xaso de que no lleguen las existencias a 0 lo que hago es actualizxar la cantidad
        {
            InventarioUI.Instance.AñadirItem(inventarioItems[index], inventarioItems[index].Cantidad, index);
        }
    }

    private void EquiparItem(int index)
    {
        if (inventarioItems[index] == null)
        {
            return;
        }
        //Tengo q comprobar si es de tipo arma 
        if (inventarioItems[index].tipo != TiposItem.Armas)
        {
            return;
        }

        inventarioItems[index].EquiparItem();

    }

    private void DespojarItem(int index)
    {
        if (inventarioItems[index] == null)
        {
            return;
        }
        //Tengo q comprobar si es de tipo arma 
        if (inventarioItems[index].tipo != TiposItem.Armas)
        {
            return;
        }

        inventarioItems[index].EliminarItem();

    }



    private InventarioItem ItemExisteEnAlmacen(string ID)
    {
        for (int i = 0; i < inventarioAlmacen.Items.Length; i++)
        {
            if (inventarioAlmacen.Items[i].ID == ID)
            {
                return inventarioAlmacen.Items[i];
            }
        }

        return null;
    }

    //private InventarioData dataGuardado;
    private void GuardarInventario()
    {
        //  dataGuardado = new InventarioData();
        //  dataGuardado.ItemsDatos = new string[numeroDeSlots];
        //  dataGuardado.ItemsCantidad = new int[numeroDeSlots];

        for (int i = 0; i < 18; i++) //numeroDeSlots
        {
            if (inventarioItems[i] == null || string.IsNullOrEmpty(inventarioItems[i].ID))
            {
                //   dataGuardado.ItemsDatos[i] = null;
                //   dataGuardado.ItemsCantidad[i] = 0;
            }
            else
            {
                // dataGuardado.ItemsDatos[i] = inventarioItems[i].ID;
                // dataGuardado.ItemsCantidad[i] = inventarioItems[i].Cantidad;
            }
        }

        //SaveGame.Save(INVENTARIO_KEY, dataGuardado);
    }


 public void GuardarInventarioBD()
    {
        if (adminMySQL.ConexionExitosa)
        {
            adminMySQL.GuardarInventario(inventarioItems);
        }
        else
        {
            Debug.Log("No se puede guardar el inventario, no hay conexión a la base de datos.");
        }
    }
/*
     public void CargarInventario()
    {
        Debug.Log("He entrado en cargar inventario en inventario.");
        if (adminMySQL.ConexionExitosa)
        {
            inventarioItems = adminMySQL.CargarInventario(numeroSlot);
            // Aquí actualizar la UI del inventario, por ejemplo:
        Debug.Log("He entrado en cargar inventario en inventario y la conexion es exitosa.");
            
            for (int i = 0; i < inventarioItems.Length; i++)
            {
                if (inventarioItems[i] != null)
                {
                    InventarioUI.Instance.AñadirItem(inventarioItems[i], inventarioItems[i].Cantidad, i);
                }
            }
        }
        else
        {
            Debug.Log("No se puede cargar el inventario, no hay conexión a la base de datos.");
        }
    }

*/


/*

       public void CargarInventario()
    {
 Debug.Log("Entrando en cargar de Inventario");

        if (adminMySQL.ConexionExitosa)
        {
            InventarioItem[] itemsCargados = adminMySQL.CargarInventario(numeroSlot);

            for (int i = 0; i < itemsCargados.Length; i++)
            {
                if (itemsCargados[i] != null)
                {
                    InventarioItem itemEnAlmacen = ItemExisteEnAlmacen(itemsCargados[i].ID);
                    if (itemEnAlmacen != null)
                    {
                        inventarioItems[i] = itemEnAlmacen.copiar();
                        inventarioItems[i].Cantidad = itemsCargados[i].Cantidad;
                    }
                }
            }Debug.Log("Aquí actualizar la UI del inventarior de Inventario");

            // Aquí actualizar la UI del inventario
            for (int i = 0; i < inventarioItems.Length; i++)
            {
                if (inventarioItems[i] != null)
                {
                    InventarioUI.Instance.AñadirItem(inventarioItems[i], inventarioItems[i].Cantidad, i);
                }
            }
        }
        else
        {
            Debug.Log("No se puede cargar el inventario, no hay conexión a la base de datos.");
        }
    }
   */

    public void CargarInventario()
    {
        Debug.Log("Entrando en cargar de Inventario");

        if (inventarioItems == null || inventarioItems.Length != numeroSlot)
        {
            inventarioItems = new InventarioItem[numeroSlot];
        }

        if (adminMySQL.ConexionExitosa)
        {
            InventarioItem[] itemsCargados = adminMySQL.CargarInventario(numeroSlot);

            for (int i = 0; i < itemsCargados.Length; i++)
            {
                if (itemsCargados[i] != null)
                {
                    InventarioItem itemEnAlmacen = ItemExisteEnAlmacen(itemsCargados[i].ID);
                    if (itemEnAlmacen != null)
                    {
                        inventarioItems[i] = itemEnAlmacen.copiar();
                        inventarioItems[i].Cantidad = itemsCargados[i].Cantidad;
                    }
                }
            }

            // Actualizar la UI del inventario
            for (int i = 0; i < inventarioItems.Length; i++)
            {
                if (inventarioItems[i] != null)
                {
                    InventarioUI.Instance.AñadirItem(inventarioItems[i], inventarioItems[i].Cantidad, i);
                }
            }
        }
        else
        {
            Debug.Log("No se puede cargar el inventario, no hay conexión a la base de datos.");
        }
    }


}


