using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{

    [SerializeField] private EstadoPersonaje estado;//Le he agregado EstadoPersonaje para manejar el mana maximo al subir los atributos 
    [SerializeField] private float manaInicial;
    [SerializeField] private float manaMax;
    [SerializeField] private float regeneracionMana;

    //Creamos una propiedad para guardar la informacion del mana que tenemos.
    public float ManaActual { get; private set; }

    private VidaNinja vidaNinja;

    public bool SePuedeRestaurarMana => ManaActual < manaMax;
    private void Awake()
    {
        vidaNinja = GetComponent<VidaNinja>();
    }

    void Start()
    {
        ManaActual = manaInicial;
        ActualizarBarraMana();
        InvokeRepeating(nameof(RegenerarMana), regeneracionMana, 1); //Metodo para repetit en este caso quiero repetir desde el inicio la llamada a regenerar mana
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            UsarMana(15f);
        }

        manaMax = estado.Mana;//Mirar si esto afecta mucho al rendimiento :?

    }

    public void UsarMana(float cantidad)
    {
        if (ManaActual >= cantidad) // Antes de usar mana hay que comprobar si hay suficiente de este. 
        {
            ManaActual -= cantidad;
            ActualizarBarraMana();
        }
    }

    private void ActualizarBarraMana()
    {
        UIUI.Instance.ActualizarMana(ManaActual, manaMax);
    }

    private void RegenerarMana()
    {
        if (vidaNinja.Life > 0f && ManaActual < manaMax)
        {
            ManaActual += regeneracionMana;
            if (ManaActual > manaMax)
            { // No quiero poder regenerar mas mana del que puedo tener como maximo
                ManaActual = manaMax;
            }
            ActualizarBarraMana();
        }
    }

    public void RestablecerMana()
    {
        ManaActual = manaInicial;
        ActualizarBarraMana();
    }

    public void RestaurarManaConPociones(float cantidad)
    {
        if (ManaActual >= manaMax)
        {//No se puede restaurar mas mana 
            return;
        }

        ManaActual += cantidad;
        if (ManaActual > manaMax)
        {
            ManaActual = manaMax;
        }

        UIUI.Instance.ActualizarMana(ManaActual,manaMax);

    }


}
