using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualizarPersonaje : MonoBehaviour
{
    public GameObject personaje;

    // Update is called once per frame
    void Update()
    {
        StateNameController2.posicionX = personaje.transform.position.x;
        StateNameController2.posicionZ = personaje.transform.position.z;
    }
}
