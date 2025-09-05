using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Main_ControlSetting : MonoBehaviour
{
    [Header("Main menu")]
    [SerializeField] private Main_Menu mainMenu; // Use only in main menu
    [SerializeField] private SettingController isoSettingController; // Use only in iso mode

    [Header("Button")]
    [SerializeField] private GameObject confirmBtn;

    [Header("Keymap setting window properties")]
    [SerializeField] TMP_Dropdown keymapDropdown;
    [SerializeField] private GameObject[] keymapDiagramArr;
    [SerializeField] private KeyMapSO[] keymapArr;
    private KeyMapSO currentKeymapSO;
    private KeyMapSO selectedKeymapSO;

    [Header("Player state controller")]
    [SerializeField] private PlayerStateController iso_PlayerStateController;
    [SerializeField] private PlayerSideScrollStateController sidescroll_PlayerStateController;
    private void OnEnable()
    {
        SettingData keymapData = SettingHandler.instance.LoadSettingData();
        if(keymapData != null)
        {
            if(keymapData.keymapSO != null)
            {
                currentKeymapSO = keymapData.keymapSO;
                keymapDropdown.value = keymapData.keymapSO.id;
                keymapDiagramArr[keymapDropdown.value].SetActive(true);
            }
        }
        else if(keymapData == null || keymapData.keymapSO == null)
        {
            currentKeymapSO = keymapArr[0];
            keymapDiagramArr[0].SetActive(true);
            keymapDropdown.value = 0;
        }
        confirmBtn.GetComponent<Button>().onClick.AddListener(() => ConfirmKeybind(confirmBtn, currentKeymapSO, selectedKeymapSO));
        if (mainMenu != null)
        {
            mainMenu.TabHighlight();
        }
        else
        {
            isoSettingController.TabHighlight();
        }
    }
    private void OnDisable()
    {
        if (currentKeymapSO != selectedKeymapSO)
        {
            keymapDropdown.value = currentKeymapSO.id;
        }
        confirmBtn.GetComponent<Button>().onClick.RemoveListener(() => ConfirmKeybind(confirmBtn, currentKeymapSO, selectedKeymapSO));
        confirmBtn.SetActive(false);
    }
    public void OnDropdownSelected(int value)
    {
        switch (value)
        {
            case 0:
                Debug.Log("Default Ver.");
                selectedKeymapSO = keymapArr[0];
                keymapDiagramArr[0].SetActive(true);
                keymapDiagramArr[1].SetActive(false);
                keymapDiagramArr[2].SetActive(false);
                confirmBtn.SetActive(true);
                break;
            case 1:
                Debug.Log("ClassicA Ver.");
                selectedKeymapSO = keymapArr[1];
                keymapDiagramArr[0].SetActive(false);
                keymapDiagramArr[1].SetActive(true);
                keymapDiagramArr[2].SetActive(false);
                confirmBtn.SetActive(true);
                break;
            case 2:
                Debug.Log("ClassicB Ver.");
                selectedKeymapSO = keymapArr[2];
                keymapDiagramArr[0].SetActive(false);
                keymapDiagramArr[1].SetActive(false);
                keymapDiagramArr[2].SetActive(true);
                confirmBtn.SetActive(true);
                break;
        }
    }
    public void ConfirmKeybind(GameObject confirmBtn, KeyMapSO oldKeymap, KeyMapSO newKeymap)
    {
        SettingHandler.instance.SaveSetting_Keymap(newKeymap);
        keymapDropdown.value = newKeymap.id;
        if(iso_PlayerStateController != null)
        {
            iso_PlayerStateController.keymapSO = newKeymap;
        }
        else if(sidescroll_PlayerStateController != null)
        {
            sidescroll_PlayerStateController.keymapSO = newKeymap;
        }
        currentKeymapSO = selectedKeymapSO;
        confirmBtn.SetActive(false);
    }
}
