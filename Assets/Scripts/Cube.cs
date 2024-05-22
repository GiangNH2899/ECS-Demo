using UnityEngine;

public class Cube : MonoBehaviour
{
    private Transform _cacheTransform;
    [SerializeField] private float rotateMinSpeed;
    [SerializeField] private float rotateMaxSpeed;
    [SerializeField] private SpriteRenderer srCube;
    private float _cubeSpeed;
    public float CubeSpeed => _cubeSpeed;

    private void Awake()
    {
        _cacheTransform = transform;
        _cubeSpeed = Random.Range(rotateMinSpeed, rotateMaxSpeed);
    }

    public void Rotate()
    {
        if (CubeModeManager.EnableDummyCode)
        {
            var dummy = 0;
            for (int i = 0; i < 1000; i++)
            {
                dummy++;
            }
        }

        _cacheTransform.eulerAngles =
            new Vector3(0, 0,
                _cacheTransform.eulerAngles.z + _cubeSpeed * Time.deltaTime);
    }


    public void SetRandomColor()
    {
        srCube.color = Random.ColorHSV();
    }
}