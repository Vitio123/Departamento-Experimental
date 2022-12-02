using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Collections.Generic;


public class RequestLugares : MonoBehaviour
{

    private string url = "https://mercedes-app-backend.herokuapp.com/lugares";

    // public LugaresList LugaresList = new LugaresList();
    void Start()
    {
        // ObtenerLugares();
    }

    public void ObtenerLugares(){
        StartCoroutine(HacerSolicitud());
    }

    IEnumerator HacerSolicitud (){
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

                List<Lugares> json = JsonConvert.DeserializeObject<List<Lugares>>(request.downloadHandler.text);
                foreach(Lugares i in json){
                    Debug.Log(i.IdLugares);
                }
                break;
            }
    }
}
