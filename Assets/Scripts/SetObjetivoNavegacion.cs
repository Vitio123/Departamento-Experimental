using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class SetObjetivoNavegacion : MonoBehaviour
{
    //Crear Variables
    [SerializeField]
    private TMP_Dropdown navegacionObjetivoDropDrown;
    [SerializeField]
    private List<Objetivo> navObjObjetos = new List<Objetivo>();

    private NavMeshPath camino;
    private LineRenderer linea;
    private Vector3 objetivoPosicion = Vector3.zero;

    private bool lineaPalanca = false;

    //Método Start
    private void Start()
    {
        camino = new NavMeshPath();
        linea = transform.GetComponent<LineRenderer>();
        linea.enabled = lineaPalanca;
    }

    //Método Update
    private void Update()
    {
        ///Dibujar la ruta del jugador al objetivo seleccionado.*/
        if(lineaPalanca && objetivoPosicion != Vector3.zero){
            NavMesh.CalculatePath(transform.position, objetivoPosicion, NavMesh.AllAreas, camino);
            linea.positionCount = camino.corners.Length;
            linea.SetPositions(camino.corners);
        }
    }


    ///Toma el valor del menú desplegable y lo usa para encontrar la posición del objeto en la escena
    public void setNavObjActual(int valorSelecionado){
        objetivoPosicion = Vector3.zero;
        string textoSelecionado = navegacionObjetivoDropDrown.options[valorSelecionado].text;
        Objetivo objetivoActual = navObjObjetos.Find(x => x.Nombre.Equals(textoSelecionado));
        if (objetivoActual != null){
            objetivoPosicion = objetivoActual.PosicionObjeto.transform.position;
        }
    }

    // Cambia la visibilidad del renderizador de la línea
    public void AlterarVisibilidad(){
        lineaPalanca = !lineaPalanca;
        linea.enabled = lineaPalanca;
    }

}
