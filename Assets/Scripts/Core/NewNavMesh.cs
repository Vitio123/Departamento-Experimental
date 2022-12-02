using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class NewNavMesh : MonoBehaviour
{
    
    //  [SerializeField]
    //  private GameObject edificios;

//    public NavMeshSurface surface;

    // public NavMeshSurface surface;
    //   [SerializeField]
    //  private NavMeshSurface navmesh;

    // Start is called before the first frame update
    void Start()
    {
            GameObject.Find(wallfackedynamics(Int32.Parse(StateNameController.idLugar.text))).SetActive(false);
            // edificios = GameObject.Find(wallfackedynamics(Int32.Parse(StateNameController.idLugar.text)));
            // edificios.SetActive(false);

            // surface.BuildNavMesh();

                // NavMeshSurface nm = GameObject.FindObjectOfType<NavMeshSurface>();
                // nm.UpdateNavMesh(nm.navMeshData);

        // Debug.Log(GameObject.Find(wallfackedynamics(Int32.Parse(StateNameController.idLugar.text))).name);
        // Debug.Log(edificios.name);
        // edificios;

        // NavMeshSurface nm = GameObject.FindObjectOfType<NavMeshSurface>();
        // nm.UpdateNavMesh(nm.navMeshData);

        // navmesh = GameObject.FindObjectOfType<NavMeshSurface>();
        //  navmesh.UpdateNavMesh(navmesh.navMeshData);

    }


   private string wallfackedynamics(int numero){
            switch (numero){
            case  < 200:
                return "Edificio (1)";
                break;
            case  < 300:
                return "Edificio (2)";
                break;
            case < 400:
                return "Edificio (3)";
                break;
            case < 500:
                return "Edificio (4)";
                break;
            case < 600:
                return "Edificio (5)";
                break;
            case < 700:
                return "Edificio (6)";
                break;
            case < 800:
                return "Edificio (7)";
                break;
            case < 900:
                return "Edificio (8)";
                break;
            case < 1000:
                return "Edificio (9)";
                break;
            case < 1100:
                return "Edificio (10)";
                break;
            case < 1200:
                return "Edificio (11)";
                break;
            case < 1300:
                return "Edificio (12)";
                break;
            case < 1400:
                return "Edificio (13)";
                break;
            case < 1500:
                return "Edificio (14)";
                break;
            case < 1600:
                return "Edificio (15)";
                break;
            case < 1700:
                return "Edificio (16)";
                break;
            case < 1800:
                return "Edificio (17)";
                break;
            // case < 1900:
            //     return "Edificio (1)";
            //     break;
            // case < 2000:
            //     return "Edificio (1)";
            //     break;
            // case < 2100:
            //     return "Edificio (1)";
            //     break;
            // case < 2200:
            //     return "Edificio (1)";
            //     break;
            default:
                return null;
                break;
            }
   }

}
