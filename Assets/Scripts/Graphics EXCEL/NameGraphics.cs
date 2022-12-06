using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameGraphics : MonoBehaviour
{
    // Start is called before the first frame update
    public Image[] imagesPieChart;
    public float[] values;


    void Start()
    {
        SetValues(values);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetValues (float[] valuesToSet){
        float totalValues = 0;
        for (int i = 0; i < imagesPieChart.Length; i++){
            totalValues += FindPercentage(valuesToSet, i);
        }
    }

    private float FindPercentage(float[] valuesToSet, int index){
        float totalAmount = 0;
        for (int i = 0; i < valuesToSet.Length; i++){
            totalAmount = valuesToSet[i];
        }
            return valuesToSet[index] / totalAmount;
    }
}
