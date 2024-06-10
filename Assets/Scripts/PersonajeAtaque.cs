using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeAtaque : MonoBehaviour
{
    public Armas ArmaEquipada { get; set; }
    private ObjectPooling objectPooling;

    [SerializeField] private EstadoPersonaje stats;

    [SerializeField] private Transform[] posicionesDisparo; //Arriba derecha abajo izquierda

    public InteraccionEnemigo EnemigoObjetivo { get; set; }

    private int indexDireccionDisparo; //lo voy a utilizar para guardar las referencias del array 

    private Mana mana;

    [SerializeField] float tiempoEntreAtaques;
    private float tiempoSiguienteAtaque;

    public bool EstaAtacando { get; set; }





    private void ObtenerDireccionDisparo()
    {
        Vector2 entrada = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));//Obtenemos la direcciond e movimiento
        if (entrada.x > 0.1)
        {
            indexDireccionDisparo = 1; //Es decir la derecha
        }
        else if (entrada.x < 0.1)
        {
            indexDireccionDisparo = 3; //Es decir la Izquierda
        }
        else if (entrada.y > 0.1)
        {
            indexDireccionDisparo = 0; //Es decir Arriba
        }
        else if (entrada.y < 0.1)
        {
            indexDireccionDisparo = 2; //Es decir Abajo
        }

    }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ObtenerDireccionDisparo();

        if (Time.time > tiempoSiguienteAtaque)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (ArmaEquipada == null || EnemigoObjetivo == null)
                {
                    return;
                }
                UsarArma();
                tiempoSiguienteAtaque = Time.time + tiempoEntreAtaques;
                StartCoroutine(IECondicionAtaque());
            }
        }
    }

    private void Awake()
    {
        objectPooling = new ObjectPooling();
        objectPooling.cantidadParaCrear = 15;//De esta forma creo 15 a la vez
        mana = GetComponent<Mana>();
    }

    public void EquiparArma(ItemArma itemArma)
    {
        ArmaEquipada = itemArma.arma;
        if (ArmaEquipada.tipoArma == ArmaType.Magia)
        {
            objectPooling.CrearPooler(ArmaEquipada.proyectil.gameObject);
        }
        stats.AñadirBonusXArma(ArmaEquipada);
    }


    public void DespojarArma()
    {
        if (ArmaEquipada.tipoArma == ArmaType.Magia)
        {
            objectPooling.DestruirPooler();
        }

        stats.QuitarBonusXArma(ArmaEquipada);


        if (ArmaEquipada != null)
        {
            ArmaEquipada = null;
        }

    }


    private void UsarArma()
    {
        if (ArmaEquipada.tipoArma == ArmaType.Magia)
        {
            if (mana.ManaActual < ArmaEquipada.mana)
            {
                return;
            }
            GameObject nuevoProyectil = objectPooling.ObtenerInstancia();
            nuevoProyectil.transform.localPosition = posicionesDisparo[indexDireccionDisparo].position;//Cambiamos la direccion de disparo a dond eme interesa

            Proyectil proyectil = nuevoProyectil.GetComponent<Proyectil>();
            proyectil.InicializarProyectil(this);

            nuevoProyectil.SetActive(true);
            mana.UsarMana(ArmaEquipada.mana);
        }

        else
        {
            EnemigoVida enemigoVida= EnemigoObjetivo.GetComponent<EnemigoVida>();
            enemigoVida.RecibirDamage(ObtenerDaño());
        }
    }

    private void EnemigoRangoSeleccionado(InteraccionEnemigo enemigoSeleccionado)
    {

        if (ArmaEquipada == null)
        {
            return;
        }

        //Tiene q ser de tipo magia y no estar ya seleccionado
        if (ArmaEquipada.tipoArma != ArmaType.Magia)
        {
            return;
        }

        if (EnemigoObjetivo == enemigoSeleccionado)
        {
            return;
        }

        EnemigoObjetivo = enemigoSeleccionado;

        EnemigoObjetivo.MostrarEnemigoSeleccionado(true);

    }

    private void EnemigoNoSeleccionado()
    {
        if (EnemigoObjetivo != null)
        {
            EnemigoObjetivo.MostrarEnemigoSeleccionado(false);
            EnemigoObjetivo = null;
        }
    }

    private void OnEnable()
    {
        SeleccionarEnemigo.EventoEnemigoSeleccionado += EnemigoRangoSeleccionado;
        SeleccionarEnemigo.EventoEnemigoNOSeleccionado += EnemigoNoSeleccionado;
        DetectorEnemigo.EventoEnemigoDetectado += EnemigoMeleeDetectado;
        DetectorEnemigo.EventoEnemigoPerdido += EnemigoMeleePerdido;
    }
    private void OnDisable()
    {
        SeleccionarEnemigo.EventoEnemigoSeleccionado -= EnemigoRangoSeleccionado;
        SeleccionarEnemigo.EventoEnemigoNOSeleccionado -= EnemigoNoSeleccionado;
        DetectorEnemigo.EventoEnemigoDetectado -= EnemigoMeleeDetectado;
        DetectorEnemigo.EventoEnemigoPerdido -= EnemigoMeleePerdido;

    }

    public float ObtenerDaño()
    {
        float cantidad = stats.Poder;
        if (UnityEngine.Random.value < stats.PorcentajeCritico / 100)
        {
            cantidad *= 2;
        }

        return cantidad;
    }

    private void EnemigoMeleeDetectado(InteraccionEnemigo enemigo)
    {
        if (ArmaEquipada == null)
        {
            return;
        }

        if (ArmaEquipada.tipoArma != ArmaType.Melee)
        {
            return;
        }

        EnemigoObjetivo = enemigo;

        EnemigoObjetivo.MostrarEnemigoSeleccionado(true);
    }
    private void EnemigoMeleePerdido()
    {
        if (ArmaEquipada == null)
        {
            return;
        }

        if (ArmaEquipada.tipoArma != ArmaType.Melee)
        {
            return;
        }

        if (EnemigoObjetivo != null)
        {
            EnemigoObjetivo.MostrarEnemigoSeleccionado(false);
            EnemigoObjetivo = null;
        }
    }


    private IEnumerator IECondicionAtaque()
    {
        EstaAtacando = true;
        yield return new WaitForSeconds(0.3f);
        EstaAtacando = false;

    }



}


