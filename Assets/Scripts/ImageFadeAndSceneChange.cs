using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ImageFadeAndSceneChange : MonoBehaviour
{
    public Image imageToFade; // Referencia a la imagen que se desvanecerá
    public float fadeSpeed = 1.0f; // Velocidad de desvanecimiento
    public string sceneToLoad; // Nombre de la escena a cargar después del desvanecimiento

    private float currentAlpha = 1.0f;
    private bool isFading = false;

    void Start()
    {
        // Inicia el desvanecimiento
        FadeImage();
    }

    void Update()
    {
        if (isFading)
        {
            // Reduce gradualmente la opacidad de la imagen
            currentAlpha -= fadeSpeed * Time.deltaTime;
            imageToFade.color = new Color(imageToFade.color.r, imageToFade.color.g, imageToFade.color.b, currentAlpha);

            // Cuando la opacidad llega a cero, carga la nueva escena
            if (currentAlpha <= 0.0f)
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }

    void FadeImage()
    {
        // Comienza el desvanecimiento estableciendo isFading en verdadero
        isFading = true;
    }
}
