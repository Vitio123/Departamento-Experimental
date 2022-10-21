using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SubirPiso : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject piso1;
    public GameObject Muro1;
    public GameObject Techo1;
    public GameObject piso2;
    public GameObject Muro2;
    public GameObject Techo2;
    public GameObject piso3;
    public GameObject Muro3;
    public TMP_Text texto;

    public GameObject subirBoton;
    public GameObject bajarBoton;


    int contador = 1;

    void Start()
    {
        texto.text = contador.ToString();
        evaluar(contador);
    }        

    public void subir(){
        contador++;
        texto.text = contador.ToString();
    }

    
    public void bajar(){
            contador--;
            texto.text = contador.ToString();
            evaluar(contador);
    }

    public void evaluar(int contador){
    switch(contador){
    case 1: 
        Techo1.SetActive(false);
        Muro1.SetActive(true);
        piso1.SetActive(true);
        Techo2.SetActive(false);
        Muro2.SetActive(false);
        piso2.SetActive(false);
        piso3.SetActive(false);
        Muro3.SetActive(false);

        break;
    case 2: 
        Techo1.SetActive(true);
        Muro1.SetActive(true);
        piso1.SetActive(true);
        Techo2.SetActive(false);
        Muro2.SetActive(true);
        piso2.SetActive(true);
        piso3.SetActive(false);
        Muro3.SetActive(false);

        break;
    case 3:
        Techo1.SetActive(true);
        Muro1.SetActive(true);
        piso1.SetActive(true);
        Techo2.SetActive(true);
        Muro2.SetActive(true);
        piso2.SetActive(true);
        piso3.SetActive(true);
        Muro3.SetActive(true);

        break;
        }
    }


}
