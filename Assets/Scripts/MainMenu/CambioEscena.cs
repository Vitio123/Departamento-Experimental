using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscena : MonoBehaviour
{
    public string nombreEscena;

    public void CargarEscena(){
        SceneManager.LoadScene(nombreEscena);
    }

}
