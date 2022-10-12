using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class SetObjetivoNavegacion : MonoBehaviour
{
    //Crear Variables
    [SerializeField]
    private TMP_Dropdown navegacionObjetivoDropDrown;
    [SerializeField]
    private List<Objetivo> navObjObjetos = new List<Objetivo>();
    /* Una variable que se usa para cambiar la altura de la línea.*/
    [SerializeField]
    private Slider navegacionYOffset;

    private NavMeshPath camino;
    private LineRenderer linea;
    private Vector3 objetivoPosicion = Vector3.zero;

    private int actualPiso = 1;

    private bool lineaPalanca = false;

    //----------Método Start----------
    private void Start()
    {
        camino = new NavMeshPath();
        linea = transform.GetComponent<LineRenderer>();
        linea.enabled = lineaPalanca;
    }

    //----------Método Update----------
    private void Update()
    {
        ///Dibujar la ruta del jugador al objetivo seleccionado.*/
        if(lineaPalanca && objetivoPosicion != Vector3.zero){
            NavMesh.CalculatePath(transform.position, objetivoPosicion, NavMesh.AllAreas, camino);
            linea.positionCount = camino.corners.Length;
            Vector3[] calcularCaminoyOffset = AgregarLineaApagado();
            linea.SetPositions(camino.corners);
        }
    }


    ///Toma el valor del menú desplegable y lo usa para encontrar la posición del objeto en la escena
    public void setNavObjActual(int valorSelecionado){
        objetivoPosicion = Vector3.zero;
        string textoSelecionado = navegacionObjetivoDropDrown.options[valorSelecionado].text;
        Objetivo objetivoActual = navObjObjetos.Find(x => x.Nombre.ToLower().Equals(textoSelecionado.ToLower()));
        if (objetivoActual != null){
            if(!linea.enabled){
                AlterarVisibilidad();
            }
            objetivoPosicion = objetivoActual.PosicionObjeto.transform.position;
        }
    }

    // Cambia la visibilidad del renderizador de la línea
    public void AlterarVisibilidad(){
        lineaPalanca = !lineaPalanca;
        linea.enabled = lineaPalanca;
    }


    /// Cambia el número de piso actual y luego llama a una función para actualizar el menú desplegable
    public void CambiarActividadPiso(int pisoNumero){
        actualPiso = pisoNumero;
        EstablecerOpcionesNavegacionObjetivoDropDown(actualPiso);
    }


   /// Toma las esquinas de la ruta y agrega el valor de la compensación Y a cada esquina.
   /// El tipo de retorno es vector3 [].
    private Vector3[] AgregarLineaApagado(){
       
        if(navegacionYOffset.value == 0){
            return camino.corners;
        }

        Vector3[] lineaCalulada = new Vector3[camino.corners.Length];
        for (int i = 0; i < camino.corners.Length; i++){
            lineaCalulada[i] = camino.corners[i] + new Vector3(0, navegacionYOffset.value, 0);
        }
        return lineaCalulada;
    }


   /// Es una función que cambia las opciones de un menú desplegable dependiendo del valor de una variable.
    private void EstablecerOpcionesNavegacionObjetivoDropDown(int pisoNumero){
        navegacionObjetivoDropDrown.ClearOptions();
        navegacionObjetivoDropDrown.value = 0;

        if(linea.enabled){
            AlterarVisibilidad();
        }

        if(pisoNumero == 1){
            navegacionObjetivoDropDrown.options.Add(new TMP_Dropdown.OptionData("Comedor"));
            navegacionObjetivoDropDrown.options.Add(new TMP_Dropdown.OptionData(""));
            navegacionObjetivoDropDrown.options.Add(new TMP_Dropdown.OptionData(""));

        }
        
        if(pisoNumero == 2){
        navegacionObjetivoDropDrown.options.Add(new TMP_Dropdown.OptionData(""));
        navegacionObjetivoDropDrown.options.Add(new TMP_Dropdown.OptionData(""));
        }
   }


}
