using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUnavez : MonoBehaviour
{

    public static int numero = 0;

    public GameObject tutorial;

    public void desaparecer(){
        numero = 1;
    }

    void Start() {
        if(numero == 0){
            tutorial.SetActive(true);
        }else{
            tutorial.SetActive(false);
        }
    }
   
}
