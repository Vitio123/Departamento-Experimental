using System.Net.Mime;
using System;
using System.Collections;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PosicionCamara : MonoBehaviour
{

    //public Transform[] views;
    public float transitionSpeed;
    Transform currentView;

    private Transform temporal;

    [SerializeField]
    private TMP_InputField mainInput;

    [SerializeField]
    private TMP_Text texto;

    public GameObject objectoParaEncontrar;

    public GameObject informacionPanel;

    public TMP_Text idLugar;
    public TMP_Text area;
    public TMP_Text descripcion;

    [SerializeField] private Image panelInformacion;


    private string url;

    // Start is called before the first frame update
    void Start()
    {
        currentView = transform;
    }

    // Update is called once per frame
    void Update()
    {
        //  texto.text = mainInput.text;
        string str = mainInput.text;

        char[] spearator = {'-'};

        string[] strlist = str.Split(spearator);

        for (int i = 0; i < objectoParaEncontrar.transform.childCount; i++){
                if(objectoParaEncontrar.transform.GetChild(i).name.Equals(strlist[0])){
                currentView.position = objectoParaEncontrar.transform.GetChild(i).position + new Vector3(12.0f,50.0f,0.0f);
                mainInput.text = "";
                url = "https://mercedes-app-backend.herokuapp.com/lugares/?IdLugares=" + objectoParaEncontrar.transform.GetChild(i).name;
                Debug.Log(url);
                ObtenerLugar();
            }
        }
    }

    public void ObtenerLugar(){
        StartCoroutine(HacerSolicitudInformacion());

    }

    private void LateUpdate()
    {
        if(mainInput.text != ""){
            transform.position = Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * transitionSpeed);}
    }


    IEnumerator HacerSolicitudInformacion()
    {

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
                
                var informacion = JsonConvert.DeserializeObject<System.Collections.Generic.List<InformacionResponse>>(request.downloadHandler.text);
                var myinformacion = informacion[0];
                if(myinformacion != null){
                    informacionPanel.SetActive(true);
                    idLugar.text = myinformacion.IdLugares.ToString();
                    panelInformacion.color = AgregarColor(Int32.Parse(idLugar.text));
                    //panelInformacion.color = new Color32(156, 39, 176,255);
                    area.text = myinformacion.Lugar.ToString();
                    descripcion.text = myinformacion.Descripcion.ToString();
                }
                break;
            }
    }


    private Color AgregarColor(int numero){
        switch (numero){
            case  < 200:
                return new Color32(244, 67, 54,255);
                break;
            case  < 300:
                return new Color32(233, 30, 99,255);
                break;
            case < 400:
                return new Color32(156, 39, 176,255);
                break;
            case < 500:
                return new Color32(63, 81, 181,255);
                break;
            case < 600:
                return new Color32(33, 150, 243,255);
                break;
            case < 700:
                return new Color32(3, 169, 244,255);
                break;
            case < 800:
                return new Color32(3, 169, 244,255);
                break;
            case < 900:
                return new Color32(0, 188, 212,255);
                break;
            case < 1000:
                return new Color32(0, 150, 136,255);
                break;
            case < 1100:
                return new Color32(76, 175, 80,255);
                break;
            case < 1200:
                return new Color32(139, 195, 74,255);
                break;
            case < 1300:
                return new Color32(205, 220, 57,255);
                break;
            case < 1400:
                return new Color32(255, 235, 59,255);
                break;
            case < 1500:
                return new Color32(255, 193, 7,255);
                break;
            case < 1600:
                return new Color32(255, 152, 0,255);
                break;
            case < 1700:
                return new Color32(255, 87, 34,255);
                break;
            case < 1800:
                return new Color32(121, 85, 72,255);
                break;
            case < 1900:
                return new Color32(158, 158, 158,255);
                break;
            case < 2000:
                return new Color32(96, 125, 139,255);
                break;
            case < 2100:
                return new Color32(221, 44, 0,255);
                break;
            case < 2200:
                return new Color32(100, 221, 23,255);
                break;
            default:
                return new Color32(244, 67, 54,255);
                break;
        }
    }

}
