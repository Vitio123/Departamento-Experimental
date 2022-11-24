using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MedirDistancia : MonoBehaviour
{
    public GameObject objeto1;
    public GameObject objeto2;
    private float distancia;
    public TMP_Text textDistancia;

    // Update is called once per frame
    void Update()
    {
        distancia = Vector3.Distance(objeto1.transform.position, objeto2.transform.position);
        textDistancia.text = "Distancia: " + distancia.ToString("0.##") + "m";

    }


    void setDistancia(){

    }
}
