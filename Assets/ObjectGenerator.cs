using System;
using System.Collections.Generic;
using GgAccel;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;
using Random = UnityEngine.Random;

public class ObjectGenerator : MonoBehaviour
{
    [SerializeField] private Cube cubePrefab;
    [SerializeField] private Vector2 spawnRangeX, spawnRangeY;
    private List<Cube> _spawnedCubes = new();
    private Transform _cacheTransform;
    private RotateCubeJob _rotateJob;
    private JobHandle _rotateHandle;
    private TransformAccessArray _cubeAccessArray;
    private NativeArray<float> _cubeSpeeds;

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

    private void Update()
    {
        if (CubeModeManager.Mode == CubeMode.Job)
        {
            _rotateJob = new RotateCubeJob
            {
                cubeSpeed = _cubeSpeeds,
                deltaTime = Time.deltaTime
            };
            _rotateHandle = _rotateJob.Schedule(_cubeAccessArray);
        }
        else
        {
            foreach (var cube in _spawnedCubes)
            {
                cube.Rotate();
            }
        }
    }

    private void LateUpdate()
    {
        if (CubeModeManager.Mode == CubeMode.Job)
        {
            _rotateHandle.Complete();
        }
    }

    private void SpawnCube(int numberOfSpawnedCube)
    {
        foreach (var cube in _spawnedCubes)
        {
            Pool.Release(cube);
        }

        _spawnedCubes.Clear();
        if (_cubeAccessArray.isCreated) _cubeAccessArray.Dispose();
        if (_cubeSpeeds.IsCreated) _cubeSpeeds.Dispose();
        _cubeAccessArray = new TransformAccessArray(numberOfSpawnedCube);
        _cubeSpeeds = new NativeArray<float>(numberOfSpawnedCube, Allocator.Persistent);

        for (var i = 0; i < numberOfSpawnedCube; i++)
        {
            var cube = Pool.Get(cubePrefab,
                new Vector3(Random.Range(spawnRangeX.x, spawnRangeX.y), Random.Range(spawnRangeY.x, spawnRangeY.y), 0),
                Quaternion.identity, _cacheTransform);
            cube.SetRandomColor();
            _spawnedCubes.Add(cube);
            _cubeAccessArray.Add(cube.transform);
            _cubeSpeeds[i] = cube.CubeSpeed;
        }
    }
}