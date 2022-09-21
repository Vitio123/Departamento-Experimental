using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Establezca esto en un objeto de juego vacío colocado en (0,0,0) y adjunte su cámara activa.
El script solo se ejecuta en dispositivos móviles o en la aplicación remota.
*/

public class DeplazarYPell : MonoBehaviour
{
    /* solo se compilará si en la plataforma es iOS o Android.*/
#if UNITY_IOS || UNITY_ANDROID
    public Camera Camera;
    public bool Girar;
    protected Plane plano;

    private void Awake()
    {
        if(Camera == null){
            Camera = Camera.main;
        }
    }

    void Update()
    {
        //Acutalizar plano
        if(Input.touchCount >=1){
            plano.SetNormalAndPosition(transform.up, transform.position);
        }

        var delta1 = Vector3.zero;
        var delta2 = Vector3.zero;

        //Desplazar
        if(Input.touchCount >= 1){
            delta1 = PosicionPlanoDelta(Input.GetTouch(0));
            if(Input.GetTouch(0).phase == TouchPhase.Moved){
                Camera.transform.Translate(delta1, Space.World);
            }
        }

        //Pellizcar

        if(Input.touchCount >= 2){

            var pos1 = PosicionPlano(Input.GetTouch(0).position);
            var pos2 = PosicionPlano(Input.GetTouch(1).position);
            var pos1b = PosicionPlano(Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition);
            var pos2b = PosicionPlano(Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition);

            //Calcular zoom
            var zoom = Vector3.Distance(pos1, pos2) / Vector3.Distance(pos1b, pos2b);

            //Caso extremo
            if(zoom == 0 || zoom > 10){
                return;
            }

            //Mover la cantidad de la cámara el rayo medio
            Camera.transform.position = Vector3.LerpUnclamped(pos1, Camera.transform.position, 1 / zoom);

            if(Girar && pos2b != pos2){
                Camera.transform.RotateAround(pos1, plano.normal, Vector3.SignedAngle(pos2 - pos1, pos2b - pos1b, plano.normal));
            }
        }

    }

    protected Vector3 PosicionPlanoDelta(Touch tocar){
        /* Verificar si el toque no se mueve.Si no se mueve, devuelve un vector de cero. */
        if (tocar.phase != TouchPhase.Moved){
            return Vector3.zero;
        }
        //delta 
        var rayoAnterior = Camera.ScreenPointToRay(tocar.position - tocar.deltaPosition);
        var rayoAhora = Camera.ScreenPointToRay(tocar.position);
        if(plano.Raycast(rayoAnterior, out var enterAnterior) && plano.Raycast(rayoAhora, out var enterAhora)){
            return rayoAnterior.GetPoint(enterAnterior) - rayoAhora.GetPoint(enterAhora);
        }
        //No en el plano
        return Vector3.zero;
    }

    protected Vector3 PosicionPlano(Vector2 pantallaPos){
        //Posición
        /* Creando un rayo desde la cámara al punto de pantalla. */
        var rayoAhora = Camera.ScreenPointToRay(pantallaPos);

        if(plano.Raycast(rayoAhora, out var enterAhora)){
            return rayoAhora.GetPoint(enterAhora);}
        return Vector3.zero;
    }

#endif
}
