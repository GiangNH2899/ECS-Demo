using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Random = Unity.Mathematics.Random;

[BurstCompile]
public partial class SpawnerSystem : SystemBase
{
    private Random _random;
    private NativeArray<Entity> _spawnedEntities;
    public static Action OnSpawnEntities;

    protected override void OnCreate()
    {
        base.OnCreate();
        _random = new Random(99);
        SpawnSlider.OnSpawnNumberChanged += (spawnTime) =>
        {
            if (_spawnedEntities.IsCreated)
            {
                EntityManager.DestroyEntity(_spawnedEntities);
                _spawnedEntities.Dispose();
            }

            if (CubeModeManager.Mode != CubeMode.ECS && CubeModeManager.Mode != CubeMode.EcsJobBurst) return;

            _spawnedEntities = new NativeArray<Entity>(spawnTime, Allocator.Persistent);
            foreach (RefRW<Spawner> spawner in SystemAPI.Query<RefRW<Spawner>>())
            {
                for (var i = 0; i < spawnTime; i++)
                {
                    Entity newEntity = EntityManager.Instantiate(spawner.ValueRO.Prefab);
                    EntityManager.SetComponentData(newEntity,
                        LocalTransform.FromPosition(GetRandomPos(spawner.ValueRO)));
                    EntityManager.GetComponentData<LocalTransform>(newEntity);
                    _spawnedEntities[i] = newEntity;
                }
            }
            OnSpawnEntities.Invoke();
        };
    }

    protected override void OnUpdate()
    {
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _spawnedEntities.Dispose();
    }

    private float3 GetRandomPos(in Spawner spawner)
    {
        var x = _random.NextFloat(spawner.spawnRangeX.x, spawner.spawnRangeX.y);
        var y = _random.NextFloat(spawner.spawnRangeY.x, spawner.spawnRangeY.y);
        return new float3(x, y, 0);
    }
}