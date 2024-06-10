using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlayer : MonoBehaviour
{
    private Vector2 entrada;
    private Rigidbody2D rb2D;
    private Vector2 direccionMovimiento;
    //Ahora me interesa usar esta variable en otra clase asi que lo que voy a hacer es usar otra variable esta vez publica que me devuelva el valor de esta para no cambiarlo directamenteç
    //En los foros esta es la forma BUENA de hacerlo.
    public Vector2 DireccionMovimiento => direccionMovimiento; //FORMA BUENA ¿? SE SUPONE Q LA MEJOR :)


    //Quiero que esta variable sea privada pero quiero poder modificarla desde el inspector de unity por tanto le voy a poner [SerializeField] para poder hacer esto.
    [SerializeField] private float velocidadPersonaje;
    //magnitude me regresa la longitud del vector entoces si no nos movemos esta es cero y la condicion falsa pero si nos movemos esta es mayor a 0 y verdadera
    public bool EnMovimiento => direccionMovimiento.magnitude > 0f;



    private VidaNinja vidaNinja;


    //tan pronto como se crea el script se inicia Awake(Este se inicia antes que cualquier otro)
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        vidaNinja = GetComponent<VidaNinja>();
    }

    void Start()
    {

    }


    void Update()
    {

        if (vidaNinja.NinjaHaMuerto)
        {
            direccionMovimiento = Vector2.zero;//Si estoy muerto no me puedo mover
            return;
        }
        //Guardamos la entrada del player (movimiento)
        entrada = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //Control de la direccion de movimiento en X 
        if (entrada.x > 0.1f)
        {
            direccionMovimiento.x = 1f;
        }
        else if (entrada.x < 0f)
        {
            direccionMovimiento.x = -1f;
        }
        else
        {
            direccionMovimiento.x = 0f;
        }

        //Control de la direccion de movimiento en Y
        if (entrada.y > 0.1f)
        {
            direccionMovimiento.y = 1f;
        }
        else if (entrada.y < 0f)
        {
            direccionMovimiento.y = -1f;
        }
        else
        {
            direccionMovimiento.y = 0f;
        }


    }

    /*
    El método FixedUpdate() en Unity se utiliza para actualizar las funciones relacionadas con la física. Se llama automáticamente en cada frame de tiempo fijo,
     independientemente de cuántos frames por segundo esté ejecutando tu juego. Esto es diferente del método Update(), 
     que se llama una vez por frame, y puede variar dependiendo de la velocidad de los frames.

    FixedUpdate() es el lugar más común para procesar las interacciones físicas, como mover un objeto basado en la entrada del usuario o aplicar fuerzas a un Rigidbody.
     Esto se debe a que las actualizaciones de física en Unity se realizan durante el FixedUpdate().
    */
    private void FixedUpdate()
    {
        //Movimiento del personaje
        rb2D.MovePosition(rb2D.position + direccionMovimiento * velocidadPersonaje * Time.fixedDeltaTime);
    }
}
