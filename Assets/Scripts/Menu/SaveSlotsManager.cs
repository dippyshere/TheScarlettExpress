using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSlotsManager : MonoBehaviour
{
    [Tooltip("Singleton instance of the SaveSlotsManager")]
    public static SaveSlotsManager Instance;
    [SerializeField, Tooltip("The list of save slot controllers")]
    List<SaveSlotController> saveSlotControllers;
    Canvas _saveSlotsCanvas;
    
    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        _saveSlotsCanvas = GetComponent<Canvas>();
        foreach (SaveSlotController saveSlotController in saveSlotControllers)
        {
            saveSlotController.PopulateData();
        }
    }
    
    public void ShowSaveSlots()
    {
        _saveSlotsCanvas.enabled = true;
    }
    
    public void HideSaveSlots()
    {
        MenuManager.Instance.ShowTitle();
        _saveSlotsCanvas.enabled = false;
    }
}
