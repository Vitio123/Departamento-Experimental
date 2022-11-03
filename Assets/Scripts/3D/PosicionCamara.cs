using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PosicionCamara : MonoBehaviour
{

    //public Transform[] views;
    public float transitionSpeed;
    Transform currentView;

    private Transform temporal;

    [SerializeField]
    private TMP_InputField mainInput;

    [SerializeField]
    private TMP_Text texto;

    public GameObject objectoParaEncontrar;

    // Start is called before the first frame update
    void Start()
    {
        currentView = transform;
    }

    // Update is called once per frame
    void Update()
    {
        //  texto.text = mainInput.text;
        string str = mainInput.text;

        char[] spearator = {'-'};

        string[] strlist = str.Split(spearator);

        for (int i = 0; i < objectoParaEncontrar.transform.childCount; i++){
                if(objectoParaEncontrar.transform.GetChild(i).name.Equals(strlist[0])){
                currentView.position = objectoParaEncontrar.transform.GetChild(i).position + new Vector3(12.0f,50.0f,0.0f);
                mainInput.text = "";
            }
        }
    }


    private void LateUpdate()
    {
        if(mainInput.text != ""){
            transform.position = Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * transitionSpeed);}
    }

}
