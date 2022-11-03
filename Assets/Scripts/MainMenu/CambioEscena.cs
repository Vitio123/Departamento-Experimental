using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscena : MonoBehaviour
{
    public string nombreEscena;

    public Animator transicion;

    public float transitionTime = 1f;

    public void CargarEscena(){
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel(){
        transicion.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(nombreEscena);
    }

}
