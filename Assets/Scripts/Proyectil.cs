using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{

    [SerializeField] private float velocidad;
    private Rigidbody2D rigidbod;
    private Vector2 direccion;
    private InteraccionEnemigo enemigo;
    public PersonajeAtaque PersonajeAtaque { get; set; }
    
    


    private void Awake()
    {
        rigidbod = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (enemigo == null)
        {
            return;
        }
        DispararProyectil();
    }

    private void DispararProyectil()
    {
        direccion = (Vector2)(enemigo.transform.position - transform.position);
        float anguloRotacion = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;//Con esto se calculaq el angulo que tiene que hacer el proyectil para impactar con el enemigo
        transform.rotation = Quaternion.AngleAxis(anguloRotacion, Vector3.forward);
        rigidbod.MovePosition(rigidbod.position + direccion.normalized * velocidad * Time.fixedDeltaTime);
    }

    public void InicializarProyectil(PersonajeAtaque ataque)
    {
     PersonajeAtaque = ataque;
     enemigo=ataque.EnemigoObjetivo;   
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemigo"))
        {
            enemigo.GetComponent<EnemigoVida>().RecibirDamage(PersonajeAtaque.ObtenerDaño());  //Matarme por favor
            StartCoroutine(DeactivateAfterDelay());

        }
    }

    private IEnumerator DeactivateAfterDelay()
    {
        yield return new WaitForSeconds(0.5f); // Esperar medio segundo
        gameObject.SetActive(false);
    }

}
