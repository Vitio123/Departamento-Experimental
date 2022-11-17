using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TransferirIdLugar : MonoBehaviour
{
    public string nombreEscena;

    public Animator transicion;

    public float transitionTime = 1f;

    public TMP_Text idLugar;
    public TMP_Text nombreLugar;

    public void CargarEscena(){
        StateNameController.idLugar = idLugar;
        StateNameController.nombreLugar = nombreLugar;
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel(){
        transicion.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(nombreEscena);
    }

}
