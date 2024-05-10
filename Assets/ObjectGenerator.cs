using System.Collections.Generic;
using GgAccel;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    [SerializeField] private Cube cubePrefab;
    [SerializeField] private Vector2 spawnRangeX, spawnRangeY;
    private List<Cube> _spawnedCubes = new();
    private Transform _cacheTransform;

    private void Awake()
    {
        _cacheTransform = transform;
    }

    private void OnEnable()
    {
        SpawnSlider.OnSpawnNumberChanged += SpawnCube;
    }

    private void OnDisable()
    {
        SpawnSlider.OnSpawnNumberChanged -= SpawnCube;
    }

    private void SpawnCube(int numberOfSpawnedCube)
    {
        foreach (var cube in _spawnedCubes)
        {
            Pool.Release(cube);
        }

        _spawnedCubes.Clear();

        for (var i = 0; i < numberOfSpawnedCube; i++)
        {
            var cube = Pool.Get(cubePrefab,
                new Vector3(Random.Range(spawnRangeX.x, spawnRangeX.y), Random.Range(spawnRangeY.x, spawnRangeY.y), 0),
                Quaternion.identity, _cacheTransform);
            cube.SetRandomColor();
            _spawnedCubes.Add(cube);
        }
    }
}