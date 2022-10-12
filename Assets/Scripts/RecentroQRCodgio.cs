using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using ZXing;


public class RecentroQRCodgio : MonoBehaviour
{
    [SerializeField]
    private ARSession sesion;
    [SerializeField]
    [System.Obsolete]
    private ARSessionOrigin sesionOringen;
    [SerializeField]
    private ARCameraManager administradorCamara;
    [SerializeField]
    private List<Objetivo> objetivoNavegacionObjetos = new List<Objetivo>();

    private Texture2D cameraImagenTextura;
    private IBarcodeReader lector = new BarcodeReader();

    [System.Obsolete]
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            EstablecerQRCodigoRecentrarObjetivo("Comedor");
        }
    }

    [System.Obsolete]
    private void OnEnable(){
        administradorCamara.frameReceived += OnCameraFrameReceived;
    }

    [System.Obsolete]
    private void OnDisable()
    {
         administradorCamara.frameReceived -= OnCameraFrameReceived;
    }

    [System.Obsolete]
    private void OnCameraFrameReceived(ARCameraFrameEventArgs eventArgs)
    {
        if (!administradorCamara.TryAcquireLatestCpuImage(out XRCpuImage image))
        {
            return;
        }

        var conversionParams = new XRCpuImage.ConversionParams
        {
            //Get the entire image.
            inputRect = new RectInt(0, 0, image.width, image.height),

            //Downsample RGBA format.
            outputFormat = TextureFormat.RGBA32,

            //Flip across the vertical axis (mirror image).
            transformation = XRCpuImage.Transformation.MirrorY
        };
        //See how many bytes you need to store the final image.
        int size = image.GetConvertedDataSize(conversionParams);

        //Allocate a bujjer to store the image.
        var buffer = new NativeArray<byte>(size, Allocator.Temp);

        //Extract the image data
        image.Convert(conversionParams, buffer);

        //The image was converted to RGBA32 format and written into the provided buffer
        //So you can dispose of the XRCpuImage. You must do this or it will leak resources.
        image.Dispose();

        //At this point, you can process the image, pass it to a computer vision algorithm, etc.
        //In this example, you aplly it to a texture to visualize it.

        //You've got the data; let's put it into a texture so you can visualize it.
        cameraImagenTextura = new Texture2D(
                conversionParams.outputDimensions.x,
                conversionParams.outputDimensions.y,
                conversionParams.outputFormat,
                false);
        cameraImagenTextura.LoadRawTextureData(buffer);

        //Done with your temporary data, so you can dispose it.
        buffer.Dispose();

        //Detect and decode the barcode inside the bitmap
        var result = lector.Decode(cameraImagenTextura.GetPixels32(), cameraImagenTextura.width, cameraImagenTextura.height);

        //Do something with the result
        if(result != null){
            EstablecerQRCodigoRecentrarObjetivo(result.Text);
        }
    }

    [System.Obsolete]
    private void EstablecerQRCodigoRecentrarObjetivo(string objetivoTexto) {
        Objetivo objetivoComun = objetivoNavegacionObjetos.Find(x => x.Nombre.ToLower().Equals(objetivoTexto.ToLower()));
        if(objetivoComun != null){
            //Reset position and rotation of ArSesion
            sesion.Reset();

            //Add offset for rencentering
            sesionOringen.transform.position = objetivoComun.PosicionObjeto.transform.position;
            sesionOringen.transform.position = objetivoComun.PosicionObjeto.transform.position;

        }
    }

}
