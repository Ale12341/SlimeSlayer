using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoAtaque //Enum de tipos de ataques 
{
    melee,
    embestida
}
public class EnemigoControlador : MonoBehaviour
{
    [SerializeField] private EnemigoEstado estadoInicial;
    [SerializeField] private EnemigoEstado estadoPorDefecto;

    public Transform ReferenciaPersonaje { get; set; }

    public float rangoDeteccion;

    public LayerMask personajeLayerMask; //Con esto puedo filtrar colisiones en función de las capas


    public EnemigoEstado estadoActual { get; set; }


    [SerializeField] private float velocidadMovimiento;
    public float VelocidadMovimiento => velocidadMovimiento;

    public SlimeMovimiento EnemigoMovimiento { get; set; }


    [SerializeField] private float daño;
    public float Daño => daño;

    [SerializeField] private float rangoAtaque;


    [SerializeField] private TipoAtaque tipoAtaque;
    public TipoAtaque tipodeAtaque => tipoAtaque;

    [SerializeField] private float cooldown = 0f;

    private float tiempoSiguienteAtaque;

    [SerializeField] private EstadoPersonaje estado;


    [SerializeField] private float rangoDeEmbestida;
    [SerializeField] private float velocidadEmbestida;

    private BoxCollider2D boxCollider2D;

    public float rangoAtaqueDeterminado => tipoAtaque == TipoAtaque.embestida ? rangoDeEmbestida : rangoAtaque;



    private void Start()
    {
        estadoActual = estadoInicial;
        EnemigoMovimiento = GetComponent<SlimeMovimiento>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public void CambiarEstado(EnemigoEstado nuevoEstadoEnemigo)
    {
        if (nuevoEstadoEnemigo != estadoPorDefecto)
        {
            estadoActual = nuevoEstadoEnemigo;
        }
    }


    public bool JugadorEnRangoAtaque(float rango)
    {
        float distanciaAPersonaje = (ReferenciaPersonaje.position - transform.position).sqrMagnitude;//De aqui saco la distancia    //////PUTO UNITYYYY   //sqrmagnitude es mucho mas liviana q magnitude usar esta por rendimiento Acuerdate pedazo de cabezon deja de usar magnitude o el juego va a ir a 2 putos fps
        if (distanciaAPersonaje < Mathf.Pow(rango, 2))
        { //Tengo q elevarlo al cuadrado ya que he querido ganar una eficiencia minima pero bueno me apetece ser GILIPOLLAS

            return true;
        }

        return false;

    }

    private void Update()
    {
        estadoActual.EjecutarEstado(this);
    }

    public void ActualizarTiempoAtaque()
    {
        tiempoSiguienteAtaque = Time.time + cooldown;
    }

    public bool SePuedeAtacar() //Te amo IA Gracias por ayudarme a depurar 
    {
        if (Time.time > tiempoSiguienteAtaque) // Compare the current time with the next attack time
        {
            return true;
        }
        return false;
    }



    //Necesito lograr una conexion con las stats del jugador para reducir el daño con la defensa 
    /*public void AplicarDañoPersonaje(float damage)
    {
        float dañoPorRealizar = 0;
        dañoPorRealizar = Mathf.Max(damage - (estado.Defensa / 100), 1f);//Mathf.Max cojo el mayor valor para que nunca pueda ser el daño menor q 1 ////Si da un problema al momento de quiotar vida por los porcentajes es aki cambiar y subir la defensa de 1 en unoo o redondear este valor Alex del futuro tu Puedes !!!!!!
        //Como me gustan hacer las cosas bien hechas ahora si merece la pena toda esta mierda :-)
        ReferenciaPersonaje.GetComponent<Vida>().RecibirDamage(dañoPorRealizar);
    }*/ 

    public void AplicarDañoPersonaje(float damage)
    {
        // Calcular el daño por realizar, asegurándose de que nunca sea menor que 1
        float dañoPorRealizar = Mathf.Max(damage - (estado.Defensa / 100), 1f);

        // Redondear el daño a un entero
        int dañoRedondeado = Mathf.RoundToInt(dañoPorRealizar);

        // Aplicar el daño redondeado al personaje
        ReferenciaPersonaje.GetComponent<Vida>().RecibirDamage(dañoRedondeado);
    }



    //Diferencio cada ataque en un clase sera mejor o mucha redundancia no se aiudaaaaa
    public void AtaqueMelee(float damage)
    {
        AplicarDañoPersonaje(damage);

    }

    private IEnumerator IEEmbestida(float cantidad)
    {
        // Aquí guardamos dónde está el personaje.
        Vector3 posicionPersonaje = ReferenciaPersonaje.position;

        // Aquí guardamos dónde estamos nosotros al inicio.
        Vector3 posicionInicial = transform.position;

        // Calculamos la dirección para llegar al personaje desde nuestra posición.
        Vector3 direccionHaciaPersonaje = (posicionPersonaje - posicionInicial).normalized;

        // Calculamos el punto al que queremos llegar, que está un poco antes del personaje.
        Vector3 posdicionAtaque = posicionPersonaje - direccionHaciaPersonaje * 0.5f;

        boxCollider2D.enabled = false;


        // Preparamos una variable para controlar el tiempo que dura el movimiento.
        float transicionAtaque = 0f;

        // Mientras no hayamos llegado al final del movimiento...
        while (transicionAtaque <= 1f)
        {
            // ...avanzamos un poco en el tiempo.
            transicionAtaque += Time.deltaTime * velocidadMovimiento;

            // Calculamos una curva para hacer el movimiento más suave.
            float curva = (-Mathf.Pow(transicionAtaque, 2) + transicionAtaque) * 4f;

            // Movemos un poco hacia el punto de ataque siguiendo la curva.
            transform.position = Vector3.Lerp(posicionInicial, posdicionAtaque, curva);

            // Esperamos al siguiente fotograma para continuar.
            yield return null;
        }
        if (ReferenciaPersonaje != null)
        {
            AplicarDañoPersonaje(cantidad);
        }

        boxCollider2D.enabled = true;
    }

    public void ataqueEmbestida(float cantidad)
    {
        StartCoroutine(IEEmbestida(cantidad));

    }


}
