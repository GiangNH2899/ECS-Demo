using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public partial class RotateSystem : SystemBase
{
    protected override void OnCreate()
    {
        var random = new Unity.Mathematics.Random(1);

        SpawnerSystem.OnSpawnEntities += () =>
        {
            foreach (var (rotateData, entity) in SystemAPI
                         .Query<RefRW<RotateData>>().WithEntityAccess())
            {
                var spriteRender = SystemAPI.ManagedAPI.GetComponent<SpriteRenderer>(entity);
                spriteRender.color = Random.ColorHSV();
                rotateData.ValueRW.speed = random.NextFloat(rotateData.ValueRW.minSpeed, rotateData.ValueRW.maxSpeed);
            }
        };
    }

    [BurstCompile]
    protected override void OnUpdate()
    {
        if (CubeModeManager.Mode == CubeMode.ECS)
        {
            foreach (var (localTransform, rotateSpeed) in SystemAPI
                         .Query<RefRW<LocalTransform>, RefRO<RotateData>>())
            {
                if (CubeModeManager.EnableDummyCode)
                {
                    var dummy = 0;
                    for (int i = 0; i < 1000; i++)
                    {
                        dummy++;
                    }
                }

                localTransform.ValueRW =
                    localTransform.ValueRW.RotateZ(
                        rotateSpeed.ValueRO.speed *
                        SystemAPI.Time.DeltaTime * 0.05f);
            }
        }
        else if (CubeModeManager.Mode == CubeMode.EcsJobBurst)
        {
            var rotateEntityCubeJob = new RotateEntityCubeJob
            {
                isEnableDummy = CubeModeManager.EnableDummyCode,
                deltaTime = SystemAPI.Time.DeltaTime
            };
            rotateEntityCubeJob.ScheduleParallel();
        }
    }
}