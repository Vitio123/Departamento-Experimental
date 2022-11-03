using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonesSubirPiso : MonoBehaviour
{

    //Solicitar GameObjects Piso1
    [SerializeField]
    private GameObject piso1;
    [SerializeField]
    private GameObject muros1;
    [SerializeField]
    private GameObject techo1;

    //Solicitar GameObjects Piso1
    [SerializeField]
    private GameObject Piso2;
    [SerializeField]
    private GameObject techo2;


    public void ActivarPiso1(){
        piso1.SetActive(true);
        muros1.SetActive(true);
        techo1.SetActive(false);

        Piso2.SetActive(false);
        techo2.SetActive(false);
    }


    public void ActivarPiso2(){
        piso1.SetActive(true);
        muros1.SetActive(true);
        techo1.SetActive(true);
        
        Piso2.SetActive(true);
        techo2.SetActive(false);
    }

    public void ActivarPiso3(){
        piso1.SetActive(true);
        muros1.SetActive(true);
        techo1.SetActive(true);
        
        Piso2.SetActive(true);
        techo2.SetActive(true);
    }

}
