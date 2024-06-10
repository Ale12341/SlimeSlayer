using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour   //Mejora mucho el rendimiento ???
{

    public int cantidadParaCrear;
    private List<GameObject> lista;
    public GameObject listaContenedor; //Tambien lo podria poner como una propiedad public GameObject listaContenedor { get; set; }
    
    
    

    private GameObject AñadirInstancia(GameObject gameObject)
    {
        GameObject nuevoObjeto = Instantiate(gameObject, listaContenedor.transform);
        nuevoObjeto.SetActive(false);
        return nuevoObjeto;
    }

    public void CrearPooler(GameObject gameObject)
    {
        lista = new List<GameObject>();
        listaContenedor = new GameObject($"Pool - {gameObject.name}");
        for (int i = 0; i < cantidadParaCrear; i++)
        {
            lista.Add(AñadirInstancia(gameObject));
        }
    }

    public GameObject ObtenerInstancia()
    {
        for (int i = 0; i < lista.Count; i++)
        {
            if (lista[i].activeSelf == false)
            {
                return lista[i];
            }
        }
        return null;
    }


    public void DestruirPooler(){
        Destroy(listaContenedor);
        lista.Clear();
    }
}
