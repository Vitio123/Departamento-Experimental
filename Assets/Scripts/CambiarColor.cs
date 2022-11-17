using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CambiarColor : MonoBehaviour
{
    public Button boton;
    public Image imagen;

    public void cambiarColor(){
        if(boton.image.color.Equals(new Color32(0, 121, 125, 255))){
            boton.image.color = new Color32(255,255,255,255);
            imagen.color = new Color32(0, 121, 125, 255);
        }else{
            boton.image.color = new Color32(0, 121, 125, 255);
            imagen.color = new Color32(255,255,255,255);
        }
    }
}
