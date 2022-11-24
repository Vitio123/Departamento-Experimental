using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opciones3D : MonoBehaviour
{

    public GameObject camara;
    public GameObject emergencias;

    public GameObject piso;
    public GameObject pisoOriginal;
    public GameObject paredes3d;


    public void graficos3d(){
        if(piso.activeInHierarchy){
            pisoOriginal.SetActive(true);
            paredes3d.SetActive(true);
            piso.SetActive(false);
        }else{
            pisoOriginal.SetActive(false);
            paredes3d.SetActive(false);
            piso.SetActive(true);
        }
    }

    public void orientar(){
        camara.transform.position = new Vector3(-0.4f,310.6f,-181f);
        camara.transform.rotation = Quaternion.Euler(new Vector3(58.928f, 0f, 0.02f));
    }

    public void mostrarEmergencias(){
        if(emergencias.activeInHierarchy){
            emergencias.SetActive(false);
        }else{
            emergencias.SetActive(true);
        }

    }
}
