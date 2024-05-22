using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheckBoxEnableDummy : MonoBehaviour
{
    [SerializeField] private Toggle tgDummy;
    [SerializeField] private TextMeshProUGUI txtEnable;

    private void Start()
    {
        tgDummy.isOn = CubeModeManager.EnableDummyCode;
        tgDummy.onValueChanged.AddListener((isOn) =>
        {
            txtEnable.text = isOn ? "Enable Dummy Code" : "Disable Dummy Code";

            CubeModeManager.EnableDummyCode = isOn;
        });
    }
}