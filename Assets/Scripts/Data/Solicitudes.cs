using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Librerías para la conexion y JSON
using Newtonsoft.Json;
using UnityEngine.Networking;

public class Solicitudes : MonoBehaviour
{

    private string urlLugares = "https://mercedes-app-backend.herokuapp.com/lugares";
    public static List<Lugares> listaLugaresPublico {
        private set;
        get;
    }

    [SerializeField]
    private TMP_Text texto;

    [SerializeField]
    private GameObject Panel;

    // Start is called before the first frame update
    void Start()
    {
        Panel.SetActive(true);
        texto.text = "Cargando lugares";
        if(listaLugaresPublico == null){
            StartCoroutine(HacerSolicitudLugares());
        }
        Panel.SetActive(false);
    }

    // Update is called once per frame
    // // void Update()
    // // {
        
    // // }

    IEnumerator HacerSolicitudLugares(){
        UnityWebRequest request = UnityWebRequest.Get(urlLugares);
          yield return request.SendWebRequest();

        //Motrar el resultado Request
        // List<Lugares> json = JsonConvert.DeserializeObject<List<Lugares>>(request.downloadHandler.text);
        
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
                Debug.Log("Se llama al servidor");
                listaLugaresPublico = JsonConvert.DeserializeObject<List<Lugares>>(request.downloadHandler.text);
                break;
            }
    }
}
