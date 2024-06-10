using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Hacerlo todo en AdminMySQL o separarlo en varias clases y hacerlo en esta las peticiones y cargas 
public class DataManager : MonoBehaviour
{
  /*  private AdminMySQL adminMySQL;

    private void Start()
    {
        adminMySQL = GetComponent<AdminMySQL>();
    }

    // Método para insertar datos en la base de datos
    public void InsertarDatos(string query)
    {
        MySqlConnection conexion = adminMySQL.ObtenerConexion();
        try
        {
            MySqlCommand cmd = new MySqlCommand(query, conexion);
            cmd.ExecuteNonQuery();
            Debug.Log("Datos insertados correctamente.");
        }
        catch (MySqlException e)
        {
            Debug.LogError("Error al insertar datos en la base de datos: " + e.Message);
        }
        finally
        {
            if (conexion != null)
                conexion.Close();
        }
    }

    // Método para obtener datos de la base de datos
    public List<string> ObtenerDatos(string query)
    {
        MySqlConnection conexion = adminMySQL.ObtenerConexion();
        List<string> resultados = new List<string>();
        try
        {
            MySqlCommand cmd = new MySqlCommand(query, conexion);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string resultado = reader.GetString(0); // Suponiendo que estamos obteniendo datos de la primera columna
                    resultados.Add(resultado);
                }
            }
            Debug.Log("Datos obtenidos correctamente.");
        }
        catch (MySqlException e)
        {
            Debug.LogError("Error al obtener datos de la base de datos: " + e.Message);
        }
        finally
        {
            if (conexion != null)
                conexion.Close();
        }
        return resultados;
    }

    // Otros métodos para actualizar, eliminar, etc.
    */
}
