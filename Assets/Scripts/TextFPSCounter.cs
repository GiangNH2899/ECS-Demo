using TMPro;
using UnityEngine;

public class TextFPSCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtFpsCounter;

    private void Update()
    {
        txtFpsCounter.text = $"FPS: {(int)(1.0f / Time.smoothDeltaTime)}";
    }
}