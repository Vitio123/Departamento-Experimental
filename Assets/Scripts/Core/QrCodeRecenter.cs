using System.Collections;
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
using ZXing;


public class QrCodeRecenter : MonoBehaviour {

    [SerializeField]
    private ARSession session;
    [SerializeField]
    private ARSessionOrigin sessionOrigin;
    [SerializeField]
    private ARCameraManager cameraManager;
    [SerializeField]
    private TargetHandler targetHandler;
    [SerializeField]
    private GameObject qrCodeScanningPanel;

    [SerializeField]
    private TMP_Text texto;
    [SerializeField]
    private GameObject animacion;
    [SerializeField]
    private GameObject texto_recomendacion;
    [SerializeField]
    private GameObject texto_distancia;


    private Texture2D cameraImageTexture;
    private IBarcodeReader reader = new BarcodeReader(); // create a barcode reader instance
    private bool scanningEnabled = false;


    private void OnEnable() {
        cameraManager.frameReceived += OnCameraFrameReceived;
    }

    private void OnDisable() {
        cameraManager.frameReceived -= OnCameraFrameReceived;
    }

    private void OnCameraFrameReceived(ARCameraFrameEventArgs eventArgs) {

        if (!scanningEnabled) {
            return;
        }

        if (!cameraManager.TryAcquireLatestCpuImage(out XRCpuImage image)) {
            return;
        }

        var conversionParams = new XRCpuImage.ConversionParams {
            // Obtener la imagen completa.
            inputRect = new RectInt(0, 0, image.width, image.height),

            // Reducir la muestra en 2.
            outputDimensions = new Vector2Int(image.width / 2, image.height / 2),

            // Elija el formato RGBA.
            outputFormat = TextureFormat.RGBA32,

            // Voltear a través del eje vertical (imagen de espejo).
            transformation = XRCpuImage.Transformation.MirrorY
        };

        // Vea cuántos bytes necesita para almacenar la imagen final.
        int size = image.GetConvertedDataSize(conversionParams);

        // Asignar un buffer para almacenar la imagen. 
        var buffer = new NativeArray<byte>(size, Allocator.Temp);

        // Extraer los datos de la imagen
        image.Convert(conversionParams, buffer);

        // La imagen fue convertida al formato RGBA32 y escrita en el buffer proporcionado
        // para poder disponer de la XRCpuImage. Debes hacer esto o se filtrarán recursos.
        image.Dispose();

        // En este punto, puedes procesar la imagen, pasarla a un algoritmo de visión por ordenador, etc.
        // En este ejemplo, la aplicas a una textura para visualizarla.

        // Ya tienes los datos; vamos a ponerlos en una textura para poder visualizarlos.
        cameraImageTexture = new Texture2D(
            conversionParams.outputDimensions.x,
            conversionParams.outputDimensions.y,
            conversionParams.outputFormat,
            false);

        cameraImageTexture.LoadRawTextureData(buffer);
        cameraImageTexture.Apply();

        // Terminado con sus datos temporales, para que pueda disponer de ellos.
        buffer.Dispose();

        // Detectar y decodificar el código de barras dentro del mapa de bits
        var result = reader.Decode(cameraImageTexture.GetPixels32(), cameraImageTexture.width, cameraImageTexture.height);

        // Hacer algo con el resultado
        if (result != null) {
            texto.text = "Analizando el código QR";
            scanningEnabled = false;
            StartCoroutine(SetQrCodeRecenterTarget(result.Text));
            // qrCodeScanningPanel.SetActive(false);
            texto.text = "Buscando el código QR";
        }
    }

    IEnumerator SetQrCodeRecenterTarget(string targetText) {
        TargetFacade currentTarget = targetHandler.GetCurrentTargetByTargetText(targetText);
        if (currentTarget != null) {
            yield return new WaitForSeconds(3);
            // Restablecer la posición y la rotación de ARSession
            session.Reset();
            // Añadir desplazamiento para el recentrado
            sessionOrigin.transform.position = currentTarget.transform.position;
            sessionOrigin.transform.rotation = currentTarget.transform.rotation;

            qrCodeScanningPanel.SetActive(false);
            texto_distancia.SetActive(true);
        }
    }

   
    public void ChangeActiveFloor(string floorEntrance) {
        SetQrCodeRecenterTarget(floorEntrance);
    }

    public void ToggleScanning() {
        scanningEnabled = !scanningEnabled;
        qrCodeScanningPanel.SetActive(scanningEnabled);
    }
}
