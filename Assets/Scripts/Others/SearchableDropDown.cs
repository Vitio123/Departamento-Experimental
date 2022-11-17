using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
// using UnityEngine.EventSystems;
using UnityEngine.UI;
// using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
// using Image = UnityEngine.UI.Image;

public class SearchableDropDown : MonoBehaviour
{
    [SerializeField] private Button blockerButton;
    [SerializeField] private GameObject buttonsPrefab = null;
    [SerializeField] private int maxScrollRectSize = 180;
    [SerializeField] private List<string> avlOptions = new List<string>();


    private Button ddButton = null;
    private TMP_InputField inputField = null;
    private ScrollRect scrollRect = null;
    private Transform content = null;
    private RectTransform scrollRectTrans;
    private bool isContentHidden = true;
    private List<Button> initializedButtons = new List<Button>();

    public delegate void OnValueChangedDel(string val);
    public OnValueChangedDel OnValueChangedEvt;
    char[] spearator = {'-'};

    void Start()
    {
        Init();
    }

    /// <summary>
    /// Initilize all the Fields
    /// </summary>
    private void Init()
    {
        ddButton = this.GetComponentInChildren<Button>();
        scrollRect = this.GetComponentInChildren<ScrollRect>();
        inputField = this.GetComponentInChildren<TMP_InputField>();
        scrollRectTrans = scrollRect.GetComponent<RectTransform>();
        content = scrollRect.content;

        //blocker is a button added and scaled it to screen size so that we can close the dd on clicking outside
        blockerButton.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        blockerButton.gameObject.SetActive(false);
        blockerButton.transform.SetParent(this.GetComponentInParent<Canvas>().transform);

        blockerButton.onClick.AddListener(OnBlockerButtClick);
        ddButton.onClick.AddListener(OnDDButtonClick);
        scrollRect.onValueChanged.AddListener(OnScrollRectvalueChange);
        inputField.onValueChanged.AddListener(OnInputvalueChange);
        inputField.onEndEdit.AddListener(OnEndEditing);

        AddItemToScrollRect(avlOptions);

    }

    /// <summary>
    /// public method to get the selected value
    /// </summary>
    /// <returns></returns>
    public string GetValue()
    {
        return inputField.text;
    }

    public void ResetDropDown()
    {
        inputField.text = string.Empty;
        
    }

    //call this to Add items to Drop down
    public void AddItemToScrollRect(List<string> options)
    {
        foreach (var option in options)
        {
            var buttObj = Instantiate(buttonsPrefab, content);
            string[] strlist = option.Split(spearator);
            buttObj.GetComponentInChildren<TMP_Text>().text = option;
            buttObj.GetComponentInChildren<TMP_Text>().color = AgregarColor(Int32.Parse(strlist[0]));
            buttObj.name = option;
            buttObj.SetActive(true);
            var butt = buttObj.GetComponent<Button>();
            butt.onClick.AddListener(delegate { OnItemSelected(buttObj); });
            initializedButtons.Add(butt);
        }
        ResizeScrollRect();
        scrollRect.gameObject.SetActive(false);
    }


    /// <summary>
    /// listner To Input Field End Editing
    /// </summary>
    /// <param name="arg"></param>
    private void OnEndEditing(string arg)
    {
        if (string.IsNullOrEmpty(arg))
        {
            Debug.Log("no value entered ");
            return;
        }
        StartCoroutine(CheckIfValidInput(arg));
    }

    /// <summary>
    /// Need to wait as end inputField and On option button  Contradicted and message was poped after selection of button
    /// </summary>
    /// <param name="arg"></param>
    /// <returns></returns>
    IEnumerator CheckIfValidInput(string arg)
    {
        yield return new WaitForSeconds(1);
        if (!avlOptions.Contains(arg))
        {
           // Message msg = new Message("Invalid Input!", "Please choose from dropdown",
           //                 this.gameObject, Message.ButtonType.OK);
           //
           //             if (MessageBox.instance)
           //                 MessageBox.instance.ShowMessage(msg); 

            inputField.text = String.Empty;
        }
        //else
        //    Debug.Log("good job " );
        OnValueChangedEvt?.Invoke(inputField.text);
    }
    /// <summary>
    /// Called ever time on Drop down value is changed to resize it
    /// </summary>
    private void ResizeScrollRect()
    {
        //TODO Dont Remove this until checked on Mobile Deveice
        //var count = content.transform.Cast<Transform>().Count(child => child.gameObject.activeSelf);
        //var length = buttonsPrefab.GetComponent<RectTransform>().sizeDelta.y * count;

        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)content.transform);
        var length = content.GetComponent<RectTransform>().sizeDelta.y;

        scrollRectTrans.sizeDelta = length > maxScrollRectSize ? new Vector2(scrollRectTrans.sizeDelta.x,
            maxScrollRectSize) : new Vector2(scrollRectTrans.sizeDelta.x, length + 5);
    }

    /// <summary>
    /// listner to the InputField
    /// </summary>
    /// <param name="arg0"></param>
    private void OnInputvalueChange(string arg0)
    {
        if (!avlOptions.Contains(arg0))
        {
            FilterDropdown(arg0);
        }
    }

    /// <summary>
    /// remove the elements from the dropdown based on Filters
    /// </summary>
    /// <param name="input"></param>
    public void FilterDropdown(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            foreach (var button in initializedButtons)
                button.gameObject.SetActive(true);
            ResizeScrollRect();
            scrollRect.gameObject.SetActive(false);
            return;
        }

        var count = 0;
        foreach (var button in initializedButtons)
        {
            if (!button.name.ToLower().Contains(input.ToLower()))
            {
                button.gameObject.SetActive(false);
            }
            else
            {
                button.gameObject.SetActive(true);
                count++;
            }
        }

        SetScrollActive(count > 0);
        ResizeScrollRect();
    }

    /// <summary>
    /// Listner to Scroll rect
    /// </summary>
    /// <param name="arg0"></param>
    private void OnScrollRectvalueChange(Vector2 arg0)
    {
        //Debug.Log("scroll ");
    }

    /// <summary>
    /// Listner to option Buttons
    /// </summary>
    /// <param name="obj"></param>
    private void OnItemSelected(GameObject obj)
    {
        inputField.text = obj.name;
        foreach (var button in initializedButtons)
            button.gameObject.SetActive(true);
        isContentHidden = false;
        OnDDButtonClick();
        //OnEndEditing(obj.name);
        StopAllCoroutines();
        StartCoroutine(CheckIfValidInput(obj.name));
    }

    /// <summary>
    /// listner to arrow button on input field
    /// </summary>
    
    private void OnDDButtonClick()
    {
        if(GetActiveButtons()<=0)
            return;
        ResizeScrollRect();
        SetScrollActive(isContentHidden);
    }
    private void OnBlockerButtClick()
    {
        SetScrollActive(false);
    }

    /// <summary>
    /// respondisble to enable and disable scroll rect component 
    /// </summary>
    /// <param name="status"></param>
    private void SetScrollActive(bool status)
    {
        scrollRect.gameObject.SetActive(status);
        blockerButton.gameObject.SetActive(status);
        isContentHidden = !status;
        ddButton.transform.localScale = status ? new Vector3(1, -1, 1) : new Vector3(1, 1, 1);
    }

    /// <summary>
    /// Return numbers of active buttons in the dropdown
    /// </summary>
    /// <returns></returns>
    private float GetActiveButtons()
    {
        var count = content.transform.Cast<Transform>().Count(child => child.gameObject.activeSelf);
        var length = buttonsPrefab.GetComponent<RectTransform>().sizeDelta.y * count;
        return length;
    }

        private Color AgregarColor(int numero){
        switch (numero){
            case  < 200:
                return new Color32(244, 67, 54,255);
                break;
            case  < 300:
                return new Color32(233, 30, 99,255);
                break;
            case < 400:
                return new Color32(156, 39, 176,255);
                break;
            case < 500:
                return new Color32(63, 81, 181,255);
                break;
            case < 600:
                return new Color32(33, 150, 243,255);
                break;
            case < 700:
                return new Color32(3, 169, 244,255);
                break;
            case < 800:
                return new Color32(3, 169, 244,255);
                break;
            case < 900:
                return new Color32(0, 188, 212,255);
                break;
            case < 1000:
                return new Color32(0, 150, 136,255);
                break;
            case < 1100:
                return new Color32(76, 175, 80,255);
                break;
            case < 1200:
                return new Color32(139, 195, 74,255);
                break;
            case < 1300:
                return new Color32(205, 220, 57,255);
                break;
            case < 1400:
                return new Color32(10, 10, 10,255);
                break;
            case < 1500:
                return new Color32(255, 193, 7,255);
                break;
            case < 1600:
                return new Color32(255, 152, 0,255);
                break;
            case < 1700:
                return new Color32(255, 87, 34,255);
                break;
            case < 1800:
                return new Color32(121, 85, 72,255);
                break;
            case < 1900:
                return new Color32(158, 158, 158,255);
                break;
            case < 2000:
                return new Color32(96, 125, 139,255);
                break;
            case < 2100:
                return new Color32(221, 44, 0,255);
                break;
            case < 2200:
                return new Color32(100, 221, 23,255);
                break;
            default:
                return new Color32(244, 67, 54,255);
                break;
        }
    }

   
}
