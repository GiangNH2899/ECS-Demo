using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSwitchCubeMode : MonoBehaviour
{
    [SerializeField] private Button btnSwitchMode;
    [SerializeField] private TextMeshProUGUI txtMode;

    private void Start()
    {
        btnSwitchMode.onClick.AddListener(() =>
        {
            if (CubeModeManager.Mode == CubeMode.EcsJobBurst)
            {
                CubeModeManager.Mode = CubeMode.Normal;
            }
            else
            {
                CubeModeManager.Mode += 1;
            }

            txtMode.text = CubeModeManager.Mode switch
            {
                CubeMode.Normal => "Normal",
                CubeMode.ECS => "ECS",
                CubeMode.Job => "Job",
                CubeMode.JobBurst => "Job + Burst",
                CubeMode.EcsJobBurst => "ECS + Job + Burst",
                _ => throw new ArgumentOutOfRangeException()
            };
        });
    }
}