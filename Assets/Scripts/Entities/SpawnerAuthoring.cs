using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class SpawnerAuthoring: MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private Vector2 spawnRangeX, spawnRangeY;
    
    private class SpawnerBaker : Baker<SpawnerAuthoring>
    {
        public override void Bake(SpawnerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new Spawner
            {
                // By default, each authoring GameObject turns into an Entity.
                // Given a GameObject (or authoring component), GetEntity looks up the resulting Entity.
                Prefab = GetEntity(authoring.cubePrefab, TransformUsageFlags.Dynamic),
                spawnRangeX = authoring.spawnRangeX,
                spawnRangeY = authoring.spawnRangeY,
            });
        }
    }
}

public struct Spawner : IComponentData
{
    public Entity Prefab;
    public float2 spawnRangeX, spawnRangeY;
}