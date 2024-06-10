using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoVida : Vida
{

    [SerializeField] private EnemigoBarraVida enemigoBarraVida;
    [SerializeField] private Transform posicionBarraVida;
    private EnemigoBarraVida enemigoBarraVidaCreada;
    private EnemigoControlador enemigoControlador;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;
    private InteraccionEnemigo interaccionEnemigo;
    private SlimeMovimiento slimeMovimiento;
    private EnemigoLoot  enemigoLoot;


    [SerializeField] private GameObject rastros;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        enemigoControlador = GetComponent<EnemigoControlador>();
        interaccionEnemigo = GetComponent<InteraccionEnemigo>();
        slimeMovimiento = GetComponent<SlimeMovimiento>();
        enemigoLoot = GetComponent<EnemigoLoot>();
    }

    protected override void Start()
    {
        base.Start();
        CrearBarraVida();
    }

    void Update()
    {

    }
    private void CrearBarraVida()
    {
        enemigoBarraVidaCreada = Instantiate(enemigoBarraVida, posicionBarraVida);//De esta forma cuando muere puedo quitar la barra y demas
        ActualizarBarraVida(vidaInicial, vidaMax);
    }
    protected override void ActualizarBarraVida(float vidaActual, float vidaMax)
    {
        enemigoBarraVidaCreada.ModificarSalud(vidaActual, vidaMax);
    }
    private void DesaparecerEnemigo() //Si quiero hacer que respaunen los enemigos tengo que cambiar esto
    {
        rastros.SetActive(true);
        spriteRenderer.enabled = false;
        enemigoControlador.enabled = false;
        boxCollider2D.isTrigger = true;
        interaccionEnemigo.MostrarEnemigoSeleccionado(false);
        slimeMovimiento.enabled = false;
        enemigoBarraVidaCreada.gameObject.SetActive(false);

    }

    protected override void ChampDerrotado()
    {
        DesaparecerEnemigo();
        EventoEnemigoDerrotado?.Invoke(enemigoLoot.ExpGanada);
        QuestManager.Instance.AñadirProgreso("Mata10",1);
        QuestManager.Instance.AñadirProgreso("Mata25",1);
        QuestManager.Instance.AñadirProgreso("Mata100",1);
    }

    public static Action<float> EventoEnemigoDerrotado;

}


/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoVida : Vida
{
    [SerializeField] private EnemigoBarraVida enemigoBarraVida;
    [SerializeField] private Transform posicionBarraVida;
    private EnemigoBarraVida enemigoBarraVidaCreada;
    private EnemigoControlador enemigoControlador;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;
    private InteraccionEnemigo interaccionEnemigo;
    private SlimeMovimiento slimeMovimiento;

    [SerializeField] private GameObject enemigoPrefab; // Prefab del enemigo
    [SerializeField] private Transform respawnPoint; // Punto de reaparición


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        enemigoControlador = GetComponent<EnemigoControlador>();
        interaccionEnemigo = GetComponent<InteraccionEnemigo>();
        slimeMovimiento = GetComponent<SlimeMovimiento>();
    }

    protected override void Start()
    {
        base.Start();
        CrearBarraVida();
    }

    void Update()
    {

    }
    private void CrearBarraVida()
    {
        enemigoBarraVidaCreada = Instantiate(enemigoBarraVida, posicionBarraVida);//De esta forma cuando muere puedo quitar la barra y demas
        ActualizarBarraVida(vidaInicial, vidaMax);
    }
    protected override void ActualizarBarraVida(float vidaActual, float vidaMax)
    {
        enemigoBarraVidaCreada.ModificarSalud(vidaActual, vidaMax);
    }
    private void DesaparecerEnemigo() //Si quiero hacer que respaunen los enemigos tengo que cambiar esto
    {
        spriteRenderer.enabled = false;
        enemigoControlador.enabled = false;
        boxCollider2D.isTrigger = true;
        interaccionEnemigo.MostrarEnemigoSeleccionado(false);
        slimeMovimiento.enabled = false;
        enemigoBarraVidaCreada.gameObject.SetActive(false);
    }

    protected override void ChampDerrotado()
    {
        DesaparecerEnemigo();
        StartCoroutine(GenerarNuevoEnemigo());
    }



     private IEnumerator GenerarNuevoEnemigo()
    {
        yield return new WaitForSeconds(10);

        vidaInicial=vidaMax;
        ActualizarBarraVida(vidaInicial, vidaMax);
        spriteRenderer.enabled = true;
        enemigoControlador.enabled = true;
        boxCollider2D.isTrigger = false;
        //interaccionEnemigo.MostrarEnemigoSeleccionado(true);
        slimeMovimiento.enabled = true;
        enemigoBarraVidaCreada.gameObject.SetActive(true);
        
    }

}

*/


/*
public class EnemigoVida : Vida
{
    [SerializeField] private EnemigoBarraVida enemigoBarraVida;
    [SerializeField] private Transform posicionBarraVida;
    private EnemigoBarraVida enemigoBarraVidaCreada;
    private EnemigoControlador enemigoControlador;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;
    private InteraccionEnemigo interaccionEnemigo;
    private SlimeMovimiento slimeMovimiento;

    [SerializeField] private float tiempoReaparicion = 5f; // Tiempo de espera para la reaparición

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        enemigoControlador = GetComponent<EnemigoControlador>();
        interaccionEnemigo = GetComponent<InteraccionEnemigo>();
        slimeMovimiento = GetComponent<SlimeMovimiento>();
    }

    protected override void Start()
    {
        base.Start();
        CrearBarraVida();
    }

    private void CrearBarraVida()
    {
        enemigoBarraVidaCreada = Instantiate(enemigoBarraVida, posicionBarraVida);
        ActualizarBarraVida(vidaInicial, vidaMax);
    }

    protected override void ActualizarBarraVida(float vidaActual, float vidaMax)
    {
        enemigoBarraVidaCreada.ModificarSalud(vidaActual, vidaMax);
    }

    private void DesaparecerEnemigo()
    {
        spriteRenderer.enabled = false;
        enemigoControlador.enabled = false;
        boxCollider2D.enabled = false;
        interaccionEnemigo.MostrarEnemigoSeleccionado(false);
        slimeMovimiento.enabled = false;
        enemigoBarraVidaCreada.gameObject.SetActive(false);
        StartCoroutine(ReaparecerEnemigo()); // Iniciar el proceso de reaparición
    }

    private IEnumerator ReaparecerEnemigo()
    {
        yield return new WaitForSeconds(tiempoReaparicion);

        // Restaurar estado del enemigo
        vidaInicial = vidaMax;
        ActualizarBarraVida(vidaInicial, vidaMax);
        spriteRenderer.enabled = true;
        enemigoControlador.enabled = true;
        boxCollider2D.enabled = true;
        interaccionEnemigo.MostrarEnemigoSeleccionado(true);
        slimeMovimiento.enabled = true;
        enemigoBarraVidaCreada.gameObject.SetActive(true);
    }

    protected override void ChampDerrotado()
    {
        DesaparecerEnemigo();
    }
}

*/












































/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoVida : Vida
{
    [SerializeField] private EnemigoBarraVida enemigoBarraVida;
    [SerializeField] private Transform posicionBarraVida;
    [SerializeField] private float tiempoReaparecer = 15f; // Tiempo de espera antes de reaparecer
    private EnemigoBarraVida enemigoBarraVidaCreada;
    private EnemigoControlador enemigoControlador;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;
    private InteraccionEnemigo interaccionEnemigo;
    private SlimeMovimiento slimeMovimiento;

    private Vector3 posicionInicial;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        enemigoControlador = GetComponent<EnemigoControlador>();
        interaccionEnemigo = GetComponent<InteraccionEnemigo>();
        slimeMovimiento = GetComponent<SlimeMovimiento>();
        posicionInicial = transform.position; // Guardar la posición inicial
    }

    protected override void Start()
    {
        base.Start();
        CrearBarraVida();
    }

    void Update()
    {

    }
    private void CrearBarraVida()
    {
        enemigoBarraVidaCreada = Instantiate(enemigoBarraVida, posicionBarraVida);//De esta forma cuando muere puedo quitar la barra y demas
        ActualizarBarraVida(vidaInicial, vidaMax);
    }
    protected override void ActualizarBarraVida(float vidaActual, float vidaMax)
    {
        enemigoBarraVidaCreada.ModificarSalud(vidaActual, vidaMax);
    }
    private void DesaparecerEnemigo() //Si quiero hacer que respaunen los enemigos tengo que cambiar esto 
    {
        spriteRenderer.enabled = false;
        enemigoControlador.enabled = false;
        boxCollider2D.isTrigger = true;
        interaccionEnemigo.MostrarEnemigoSeleccionado(false);
        slimeMovimiento.enabled = false;
        enemigoBarraVidaCreada.gameObject.SetActive(false);

        // Iniciar la corutina para reaparecer el enemigo
        StartCoroutine(ReaparecerEnemigo());
    }

    private IEnumerator ReaparecerEnemigo()
    {
        yield return new WaitForSeconds(tiempoReaparecer);

        // Restaurar la posición inicial
        transform.position = posicionInicial;
        
        // Restaurar la vida del enemigo
        vidaInicial = vidaMax;
        ActualizarBarraVida(vidaInicial, vidaMax);

        // Reactivar componentes
        spriteRenderer.enabled = true;
        enemigoControlador.enabled = true;
        boxCollider2D.isTrigger = false;
        //interaccionEnemigo.MostrarEnemigoSeleccionado(true);
        slimeMovimiento.enabled = true;
        enemigoBarraVidaCreada.gameObject.SetActive(true);
    }

    protected override void ChampDerrotado()
    {
        DesaparecerEnemigo();
    }
}

*/