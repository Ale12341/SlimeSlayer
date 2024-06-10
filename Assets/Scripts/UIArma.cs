using System;

using UnityEngine;
using UnityEngine.UI;

public class UIArma : Singleton<UIArma>
{
    [SerializeField] private Image armaIcono;

    public ItemArma ArmaEquipada { get; set; }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EquiparArma(ItemArma itemArma)
    {
        ArmaEquipada = itemArma;
        armaIcono.sprite = itemArma.arma.armaIcono;
        armaIcono.gameObject.SetActive(true);
        Inventario.Instance.Personaje.personajeAtaque.EquiparArma(itemArma); //Le equipo el arma el jugador
    }

    public void DespojarArma()
    {
        armaIcono.gameObject.SetActive(false);
        ArmaEquipada=null;
        Inventario.Instance.Personaje.personajeAtaque.DespojarArma(); //Le desequipo el arma el jugador
    }
}
