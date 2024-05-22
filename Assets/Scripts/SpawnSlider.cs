using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI txtSpawnedObject;
    public static Action<int> OnSpawnNumberChanged;
    private Coroutine _changeCubesNumberCoroutine;

    private void OnValidate()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        slider.onValueChanged.AddListener((value) =>
        {
            if (_changeCubesNumberCoroutine != null) StopCoroutine(_changeCubesNumberCoroutine);
            _changeCubesNumberCoroutine = StartCoroutine(OnValueChanged(value));
        });
    }

    private IEnumerator OnValueChanged(float value)
    {
        yield return new WaitForSeconds(1f);
        OnSpawnNumberChanged.Invoke((int)value);
        txtSpawnedObject.text = $"Objects: {value}";
    }
}