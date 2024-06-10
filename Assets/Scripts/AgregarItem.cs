using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgregarItem : MonoBehaviour
{
   [SerializeField] private InventarioItem inventarioItem;
   [SerializeField] private int cantidad;

   private void OnTriggerEnter2D(Collider2D other) {
    if(other.CompareTag("Player")){
        Inventario.Instance.AñadirItem(inventarioItem,cantidad);
        //una vez que hemos recogido el item lo destruimos 
        Destroy(gameObject);
    }
   }
}
