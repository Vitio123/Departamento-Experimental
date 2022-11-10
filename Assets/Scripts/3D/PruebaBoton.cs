using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaBoton : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject panel;

  public void ejecutar()
  {
        print("Hiciste un click");
        panel.SetActive(true);
    }

}
