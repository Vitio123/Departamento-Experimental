using Newtonsoft.Json;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using TMPro;

public class RequestClima : MonoBehaviour
{
    //URL OpenWeathermap - Ocultar contraseña proximamente...
    private string url = "api.openweathermap.org/data/2.5/weather?q=Chiclayo, PE&APPID=8715ac736f647c8650d3a35ca69f51de&units=metric&lang=sp";

    public TMP_Text temperatura;

    void Start()
    {
        ObtenerClima();
    }

    public void ObtenerClima(){
        StartCoroutine(HacerSolicitudClima());
    }

    IEnumerator HacerSolicitudClima (){
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        
        //Motrar el resultado Request
         switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(": Error: " + request.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(": HTTP Error: " + request.error);
                    break;
                case UnityWebRequest.Result.Success:
                var clima = JsonConvert.DeserializeObject<ClimaResponse>(request.downloadHandler.text);
                    temperatura.text = "Temperatura: "+clima.main.temp.ToString() + "° - " ;
                break;
            }
    }

}
