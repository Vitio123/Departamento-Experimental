using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetUITexto : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textoCampo;
    [SerializeField]
    private string fijoTexto;


    public void EnSliderValoresCambiados(float numeroValor){
        textoCampo.text = $"{fijoTexto} : {numeroValor}";
    }
}
