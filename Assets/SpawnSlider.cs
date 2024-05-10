using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI txtSpawnedObject;
    public static Action<int> OnSpawnNumberChanged;

    private void OnValidate()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        slider.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(float value)
    {
        OnSpawnNumberChanged.Invoke((int)value);
        txtSpawnedObject.text = $"Objects: {value}";
    }
}