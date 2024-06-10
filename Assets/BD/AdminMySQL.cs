/*using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using UnityEngine;

public class AdminMySQL : MonoBehaviour
{

    public string username;
    public string password;
    public string nombreBaseDatos;
    public string servidorBaseDatos;
   


    private string datosConexion;
    private MySqlConnection conexion;
    void Start()
    {
        datosConexion = "Server=" + servidorBaseDatos
                 + ";Database=" + nombreBaseDatos
                 + ";Uid=" + username
                 + ";Pwd=" + password
                 + ";";

//Con esto me conecto a la base de datos.
        ConectarServidorBD();
        EjecutarConsultaDePrueba();
    }


    private void ConectarServidorBD()
    {
        conexion = new MySqlConnection(datosConexion);
        try
        {
            conexion.Open();
            Debug.Log("Conexion con BD Exitosa");
        }
        catch (MySqlException e)
        {
            Debug.Log("Conexion con BD Fallida" + e.Message);
        }

    }
*private void EjecutarConsultaDePrueba1234()
    {
        if (conexion == null || conexion.State != System.Data.ConnectionState.Open)
        {
            Debug.Log("No hay conexión a la base de datos.");
            return;
        }

        string consulta = "SELECT usuario FROM usuario;"; // Cambia 'tu_tabla' por el nombre de tu tabla

        MySqlCommand cmd = new MySqlCommand(consulta, conexion);

        try
        {
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                // Asume que tu tabla tiene columnas llamadas 'columna1', 'columna2', etc.
                string columna1 = reader["usuario"].ToString();
                //string columna2 = reader["columna2"].ToString();

                Debug.Log("Columna 1: " + columna1);
               // Debug.Log("Columna 2: " + columna2);
            }
            reader.Close();
        }
        catch (MySqlException e)
        {
            Debug.Log("Error ejecutando consulta: " + e.Message);
        }
    }

      private void EjecutarConsultaDePrueba()
    {
        if (conexion == null || conexion.State != System.Data.ConnectionState.Open)
        {
            Debug.Log("No hay conexión a la base de datos.");
            return;
        }

        string consulta = "SELECT usuario FROM usuario;"; // Cambia 'usuario' por el nombre de tu tabla y columna

        MySqlCommand cmd = new MySqlCommand(consulta, conexion);
        List<string> usuarios = new List<string>();

        try
        {
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string usuario = reader["usuario"].ToString();
                usuarios.Add(usuario);
            }
            reader.Close();

            // Mostrar los resultados en el Debug
            foreach (string usuario in usuarios)
            {
                Debug.Log("Usuario: " + usuario);
                 Debug.Log("Manoli que esto fufa");
            }
        }
        catch (MySqlException e)
        {
            Debug.Log("Error ejecutando consulta: " + e.Message);
        }
    }



}*/



using System.Collections.Generic;
using MySql.Data.MySqlClient;
using UnityEngine;


public class AdminMySQL : MonoBehaviour
{
    public string username;
    public string password;
    public string nombreBaseDatos;
    public string servidorBaseDatos;

    private string datosConexion;
    private MySqlConnection conexion;

    private InventarioSlots inventarioSlots;

    public bool ConexionExitosa { get; private set; }

    void Start()
    {
        inventarioSlots = GetComponent<InventarioSlots>();
        datosConexion = "Server=" + servidorBaseDatos
                        + ";Database=" + nombreBaseDatos
                        + ";Uid=" + username
                        + ";Pwd=" + password + ";";

        ConectarServidorBD();
        CargarEstadoPersonaje();
      
    }

    private void ConectarServidorBD()
    {
        conexion = new MySqlConnection(datosConexion);
        try
        {
            conexion.Open();
            ConexionExitosa = true;
            Debug.Log("Conexion con BD Exitosa");
        }
        catch (MySqlException e)
        {
            ConexionExitosa = false;
            Debug.Log("Conexion con BD Fallida: " + e.Message);
        }
    }

   /*
    public void GuardarInventario(InventarioItem[] inventarioItems)
    {
        if (!ConexionExitosa)
        {
            Debug.Log("No hay conexión a la base de datos.");
            return;
        }
       


        string query = "INSERT INTO Items (ID, Cantidad) VALUES (@ID, @Cantidad)";

        foreach (var item in inventarioItems)
        {
            if (item != null)
            {
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@ID", item.ID);
                cmd.Parameters.AddWithValue("@Cantidad", item.Cantidad);

                try
                {
                    cmd.ExecuteNonQuery();
                     Debug.Log("Se ha guardado correctamente: ");
                }
                catch (MySqlException e)
                {
                    Debug.Log("Error ejecutando consulta: " + e.Message);
                }
            }
        }
    }
*/


 public void GuardarInventario(InventarioItem[] inventarioItems)
    {
        if (!ConexionExitosa)
        {
            Debug.Log("No hay conexión a la base de datos.");
            return;
        }

        string clearQuery = "DELETE FROM Items";
        MySqlCommand clearCmd = new MySqlCommand(clearQuery, conexion);
        clearCmd.ExecuteNonQuery();

        string query = "INSERT INTO Items (ID, Cantidad) VALUES (@ID, @Cantidad)";

        foreach (var item in inventarioItems)
        {
            if (item != null)
            {
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@ID", item.ID);
                cmd.Parameters.AddWithValue("@Cantidad", item.Cantidad);

                try
                {
                    cmd.ExecuteNonQuery();
                    Debug.Log("Se ha guardado correctamente: " + item.ID);
                }
                catch (MySqlException e)
                {
                    Debug.Log("Error ejecutando consulta: " + e.Message);
                }
            }
        }
    }




    
/*
  public InventarioItem[] CargarInventario(int numeroDeSlots)
    {
        if (!ConexionExitosa)
        {
            Debug.Log("No hay conexión a la base de datos.");
            return new InventarioItem[numeroDeSlots];
        }
 Debug.Log("Entrando en CargarInventario de MYSQL");

        List<InventarioItem> items = new List<InventarioItem>();
        string query = "SELECT * FROM Items";

        MySqlCommand cmd = new MySqlCommand(query, conexion);

        try
        {
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string id = reader["ID"].ToString();
                int cantidad = int.Parse(reader["Cantidad"].ToString());

                InventarioItem item = new InventarioItem
                {
                    ID = id,
                    Cantidad = cantidad
                };

                items.Add(item);
            }
 Debug.Log("Lectura terminada de MYSQL");

            reader.Close();
        }
        catch (MySqlException e)
        {
            Debug.Log("Error ejecutando consulta: " + e.Message);
        }

        // Ajustar la longitud del array según el número de slots disponibles
        InventarioItem[] inventarioItems = new InventarioItem[numeroDeSlots];
        for (int i = 0; i < numeroDeSlots; i++)
        {
            if (i < items.Count)
            {
                inventarioItems[i] = items[i];
            }
            else
            {
                inventarioItems[i] = null;
            }
        }
 Debug.Log("Enviado de MYSQL");
        return inventarioItems;
        
    }*/


    public InventarioItem[] CargarInventario(int numeroDeSlots)
    {
        if (!ConexionExitosa)
        {
            Debug.Log("No hay conexión a la base de datos.");
            return new InventarioItem[numeroDeSlots];
        }

        Debug.Log("Entrando en CargarInventario de MYSQL");

        List<InventarioItem> items = new List<InventarioItem>();
        string query = "SELECT * FROM Items";

        MySqlCommand cmd = new MySqlCommand(query, conexion);

        try
        {
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string id = reader["ID"].ToString();
                int cantidad = int.Parse(reader["Cantidad"].ToString());

                InventarioItem item = new InventarioItem
                {
                    ID = id,
                    Cantidad = cantidad
                };

                items.Add(item);
            }

            reader.Close();
        }
        catch (MySqlException e)
        {
            Debug.Log("Error ejecutando consulta: " + e.Message);
        }

        InventarioItem[] inventarioItems = new InventarioItem[numeroDeSlots];
        for (int i = 0; i < inventarioItems.Length; i++)
        {
            if (i < items.Count)
            {
                inventarioItems[i] = items[i];
            }
            else
            {
                inventarioItems[i] = null;
            }
        }

        Debug.Log("Enviado de MYSQL");
        return inventarioItems;
    }




 public EstadoPersonaje estadoPersonaje;



  public void GuardarEstadoPersonaje()
    {
        if (!ConexionExitosa)
        {
            Debug.Log("No hay conexión a la base de datos.");
            return;
        }

        string deleteQuery = "DELETE FROM EstadoPersonaje";
        MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, conexion);
        deleteCmd.ExecuteNonQuery();

        string insertQuery = "INSERT INTO EstadoPersonaje (Poder, Nivel, Defensa, Velocidad, Mana, Vida, PorcentajeCritico, PuntosDisponibles) " +
                             "VALUES (@Poder, @Nivel, @Defensa, @Velocidad, @Mana, @Vida, @PorcentajeCritico, @PuntosDisponibles)";
        MySqlCommand insertCmd = new MySqlCommand(insertQuery, conexion);
        insertCmd.Parameters.AddWithValue("@Poder", estadoPersonaje.Poder);
        insertCmd.Parameters.AddWithValue("@Nivel", estadoPersonaje.Nivel);
        insertCmd.Parameters.AddWithValue("@Defensa", estadoPersonaje.Defensa);
        insertCmd.Parameters.AddWithValue("@Velocidad", estadoPersonaje.Velocidad);
        insertCmd.Parameters.AddWithValue("@Mana", estadoPersonaje.Mana);
        insertCmd.Parameters.AddWithValue("@Vida", estadoPersonaje.Vida);
        insertCmd.Parameters.AddWithValue("@PorcentajeCritico", estadoPersonaje.PorcentajeCritico);
        insertCmd.Parameters.AddWithValue("@PuntosDisponibles", estadoPersonaje.PuntosDisponibles);

        try
        {
            insertCmd.ExecuteNonQuery();
            Debug.Log("Se ha guardado la información del personaje correctamente.");
        }
        catch (MySqlException e)
        {
            Debug.Log("Error ejecutando consulta: " + e.Message);
        }
    }

  /*  public void CargarEstadoPersonaje()
    {
        if (!ConexionExitosa)
        {
            Debug.Log("No hay conexión a la base de datos.");
            return;
        }

        string query = "SELECT * FROM EstadoPersonaje";
        MySqlCommand cmd = new MySqlCommand(query, conexion);

        try
        {
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                estadoPersonaje.Poder = float.Parse(reader["Poder"].ToString());
                estadoPersonaje.Nivel = float.Parse(reader["Nivel"].ToString());
                estadoPersonaje.Defensa = float.Parse(reader["Defensa"].ToString());
                estadoPersonaje.Velocidad = float.Parse(reader["Velocidad"].ToString());
                estadoPersonaje.Mana = float.Parse(reader["Mana"].ToString());
                estadoPersonaje.Vida = float.Parse(reader["Vida"].ToString());
                estadoPersonaje.PorcentajeCritico = float.Parse(reader["PorcentajeCritico"].ToString());
                estadoPersonaje.PuntosDisponibles = int.Parse(reader["PuntosDisponibles"].ToString());

                reader.Close();
                Debug.Log("Datos del personaje cargados correctamente.");
            }
            else
            {
                reader.Close();
                Debug.Log("No se encontraron datos del personaje.");
            }
        }
        catch (MySqlException e)
        {
            Debug.Log("Error ejecutando consulta: " + e.Message);
        }
    }*/

       public void CargarEstadoPersonaje()
    {
        if (!ConexionExitosa)
        {
            Debug.Log("No hay conexión a la base de datos.");
            return;
        }

        string query = "SELECT * FROM EstadoPersonaje";
        MySqlCommand cmd = new MySqlCommand(query, conexion);

        try
        {
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                estadoPersonaje.Poder = float.Parse(reader["Poder"].ToString());
                estadoPersonaje.Nivel = float.Parse(reader["Nivel"].ToString());
                estadoPersonaje.Defensa = float.Parse(reader["Defensa"].ToString());
                estadoPersonaje.Velocidad = float.Parse(reader["Velocidad"].ToString());
                estadoPersonaje.Mana = float.Parse(reader["Mana"].ToString());
                estadoPersonaje.Vida = float.Parse(reader["Vida"].ToString());
                estadoPersonaje.PorcentajeCritico = float.Parse(reader["PorcentajeCritico"].ToString());
                estadoPersonaje.PuntosDisponibles = int.Parse(reader["PuntosDisponibles"].ToString());

                reader.Close();
                Debug.Log("Datos del personaje cargados correctamente.");
                 

            }
            else
            {
                reader.Close();
                Debug.Log("No se encontraron datos del personaje.");
            }
        }
        catch (MySqlException e)
        {
            Debug.Log("Error ejecutando consulta: " + e.Message);
        }
    }

      public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego...");

        #if UNITY_EDITOR
        // Detiene el juego en el editor de Unity
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // Cierra la aplicación en una build
        Application.Quit();
        #endif
    }

//Si no va volver a llamar para que se cargue los datos vacios luego de borrarlos .)
  public void NuevaPartida()
{
    if (!ConexionExitosa)
    {
        Debug.Log("No hay conexión a la base de datos.");
        return;
    }

    try
    {
        // Borra los datos de la tabla EstadoPersonaje
        string borrarEstadoPersonaje = "DELETE FROM EstadoPersonaje";
        MySqlCommand cmdEstadoPersonaje = new MySqlCommand(borrarEstadoPersonaje, conexion);
        cmdEstadoPersonaje.ExecuteNonQuery();
        Debug.Log("Datos de EstadoPersonaje borrados correctamente.");

        // Borra los datos de la tabla Items
        string borrarItems = "DELETE FROM Items";
        MySqlCommand cmdItems = new MySqlCommand(borrarItems, conexion);
        cmdItems.ExecuteNonQuery();
        Debug.Log("Datos de Items borrados correctamente.");
        
        // Inicializar datos del personaje y el inventario
        estadoPersonaje = new EstadoPersonaje();  // Asegúrate de tener un constructor por defecto en EstadoPersonaje
        GuardarEstadoPersonaje();
        InventarioItem[] inventarioVacio = new InventarioItem[10];  // Ajusta el tamaño según sea necesario
        GuardarInventario(inventarioVacio);
    }
    catch (MySqlException e)
    {
        Debug.Log("Error ejecutando consulta: " + e.Message);
    }
}



   

}



