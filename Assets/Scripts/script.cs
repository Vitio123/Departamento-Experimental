using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class script : MonoBehaviour
{
    [SerializeField]
    private GameObject objeto;

       [SerializeField]
    private GameObject objeto2;

    public TMP_Text textoX;
    public TMP_Text textoY;
    public TMP_Text textoZ;

    public TMP_Text textoXAR;
    public TMP_Text textoYAR;
    public TMP_Text textoZAR;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


       textoX.text = "X Posición en: " + objeto.transform.position.x.ToString();
       textoZ.text = "Z Posición en: " + objeto.transform.position.z.ToString();
       textoY.text = "Y Rotación en: " + objeto.transform.rotation.y.ToString();
       
       textoXAR.text = "X Posición en: " + objeto2.transform.position.x.ToString();
       textoZAR.text = "Z Posición en: " + objeto2.transform.position.z.ToString();
       textoYAR.text = "Y Rotación en: " + objeto2.transform.rotation.y.ToString();

    }
}
