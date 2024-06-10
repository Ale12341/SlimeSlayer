using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonesPrin : MonoBehaviour
{
    private AdminMySQL adminMySQL;
    // Start is called before the first frame update
    void Start()
    {
         adminMySQL = FindObjectOfType<AdminMySQL>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego...");

        #if UNITY_EDITOR
        // Detiene el juego en el editor de Unity
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // Cierra la aplicación en una build
        Application.Quit();
        #endif
    }

//Si no va volver a llamar para que se cargue los datos vacios luego de borrarlos .)
   public void NuevaPartida()
    {
       adminMySQL.NuevaPartida();
       SceneManager.LoadScene("Transicion");
    }


    public void CambiarEscena(){
        SceneManager.LoadScene("Jugo");
    }
}
