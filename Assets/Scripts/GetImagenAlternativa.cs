using UnityEngine;
using TMPro;
using ZXing;
using UnityEngine.XR.ARFoundation;

public class GetImagenAlternativa : MonoBehaviour
{
    //Declaramos las variables
    [SerializeField]
    private ARCameraBackground arCameraBackground;
    [SerializeField]
    private RenderTexture objetivoTexturaRender;
    [SerializeField]
    private TextMeshProUGUI qrCodigoTexto;

    private Texture2D cameraImagenTextura;
    private IBarcodeReader lector = new BarcodeReader();

    // La actualización se llama una vez por cuadro
    void Update()
    {
        Graphics.Blit(null, objetivoTexturaRender, arCameraBackground.material);
        cameraImagenTextura = new Texture2D(objetivoTexturaRender.width, objetivoTexturaRender.height, TextureFormat.RGBA32, false);
        Graphics.CopyTexture(objetivoTexturaRender, cameraImagenTextura);

        // detectar y decodificar el código de barras dentro del mapa de bits
        var resultado = lector.Decode(cameraImagenTextura.GetPixels32(), cameraImagenTextura.width, cameraImagenTextura.height);

        //Do something with the results
        if(resultado != null){
            qrCodigoTexto.text = resultado.Text;
        }

    }
}
