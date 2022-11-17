using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using TMPro;

public class TargetHandler : MonoBehaviour {

    [SerializeField]
    private NavigationController navigationController;
    [SerializeField]
    private TextAsset targetModelData;
    [SerializeField]
    private TMP_Dropdown targetDataDropdown;

    [SerializeField]
    private GameObject targetObjectPrefab;
    [SerializeField]
    private Transform[] targetObjectsParentTransforms;
    [SerializeField]
    private TMP_Text textoArea;



    private List<TargetFacade> currentTargetItems = new List<TargetFacade>();

    private void Start() {
        GenerateTargetItems();
        textoArea.text = StateNameController.nombreLugar.text;
        FillDropdownWithTargetItems();
        SetSelectedTargetPositionWithDropdown(0);
        
    }

    private void GenerateTargetItems() {
        IEnumerable<Target> targets = GenerateTargetDataFromSource();
        foreach (Target target in targets) {
             if(target.Name == StateNameController.idLugar.text){
                    currentTargetItems.Add(CreateTargetFacade(target));
             }
        }

        foreach (Target target in targets) {
             if(target.Name != StateNameController.idLugar.text){
                    currentTargetItems.Add(CreateTargetFacade(target));
             }
        }
    }

    private IEnumerable<Target> GenerateTargetDataFromSource() {
        return JsonUtility.FromJson<TargetWrapper>(targetModelData.text).TargetList;
    }

    private TargetFacade CreateTargetFacade(Target target) {
        GameObject targetObject = Instantiate(targetObjectPrefab, targetObjectsParentTransforms[target.FloorNumber], false);
        
        targetObject.SetActive(true);
        targetObject.name = $"{target.FloorNumber} - {target.Name}";
        targetObject.transform.localPosition = target.Position;
        targetObject.transform.localRotation = Quaternion.Euler(target.Rotation);

        TargetFacade targetData = targetObject.GetComponent<TargetFacade>();
        targetData.Name = target.Name;
        targetData.FloorNumber = target.FloorNumber;

        return targetData;
    }

    private void FillDropdownWithTargetItems() {
        List<TMP_Dropdown.OptionData> targetFacadeOptionData =
            currentTargetItems.Select(x => new TMP_Dropdown.OptionData {
                text = $"{x.FloorNumber} - {x.Name}"
            }).ToList();

        targetDataDropdown.ClearOptions();
        targetDataDropdown.AddOptions(targetFacadeOptionData);
    }

    public void SetSelectedTargetPositionWithDropdown(int selectedValue) {
        navigationController.TargetPosition = GetCurrentlySelectedTarget(selectedValue);
    }

    private Vector3 GetCurrentlySelectedTarget(int selectedValue) {
        if (selectedValue >= currentTargetItems.Count) {
            return Vector3.zero;
        }

        return currentTargetItems[selectedValue].transform.position;
    }

    public TargetFacade GetCurrentTargetByTargetText(string targetText) {
        return currentTargetItems.Find(x =>
            x.Name.ToLower().Equals(targetText.ToLower()));
    }
}
