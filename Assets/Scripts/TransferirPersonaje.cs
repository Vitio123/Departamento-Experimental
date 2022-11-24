using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TransferirPersonaje : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject personaje;

    public TMP_Text textox;
    public TMP_Text textoz;
    public float transitionSpeed;
    public GameObject camara;
    


    void Start()
    {
        if(StateNameController2.posicionX == 0 && StateNameController2.posicionZ == 0){
            personaje.SetActive(false);
        }else{
            personaje.SetActive(true);
            personaje.transform.position = new Vector3(StateNameController2.posicionX+3.75f,2f,StateNameController2.posicionZ-14.65f);;
            textox.text = StateNameController2.posicionX.ToString();
            textoz.text = StateNameController2.posicionZ.ToString();
            camara.transform.position = personaje.transform.position + new Vector3(0.0f,25.0f,-13.0f);
        }

    }

}
