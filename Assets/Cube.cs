using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : MonoBehaviour
{
    private Transform _cacheTransform;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private SpriteRenderer srCube;

    private void Awake()
    {
        _cacheTransform = transform;
    }

    private void FixedUpdate()
    {
        _cacheTransform.eulerAngles =
            new Vector3(0, 0, _cacheTransform.eulerAngles.z + rotateSpeed * Time.fixedDeltaTime);
    }

    public void SetRandomColor()
    {
        srCube.color = Random.ColorHSV();
    }
}