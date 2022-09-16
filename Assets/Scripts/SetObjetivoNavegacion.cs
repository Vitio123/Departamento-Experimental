using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SetObjetivoNavegacion : MonoBehaviour
{
    /* Una forma de hacer una variable privada visible en el inspector.*/
    [SerializeField]
    private Camera camaraSuperior;
    [SerializeField]
    private GameObject objetivo;


    private NavMeshPath camino;
    private LineRenderer linea;
    
    private bool lineaAlternativa = false;

    private void Start()
    {
        camino = new NavMeshPath();
        linea = transform.GetComponent<LineRenderer>();
    }

    private void Update()
    {
       /* Comprobación de si el usuario ha tocado la pantalla y, de ser así, está cambiando el valor del
        Variable Lineaalternativa.*/
        if((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began)){
            lineaAlternativa = !lineaAlternativa;
        }

        if (lineaAlternativa){
            /* Calculating the path between the two points. */
            NavMesh.CalculatePath(transform.position, objetivo.transform.position, NavMesh.AllAreas, camino);
            /* Establecer el número de posiciones en el renderizador de línea al número de esquinas en el
            sendero.*/
            linea.positionCount = camino.corners.Length;
            /* Establecer las posiciones del renderizador de línea a las esquinas de la ruta.*/
            linea.SetPositions(camino.corners);
            linea.enabled = true;
        }
    }

}
