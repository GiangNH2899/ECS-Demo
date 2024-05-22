using Unity.Entities;
using UnityEngine;

public class RotateAuthoring : MonoBehaviour
{
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;

    private class Baker : Baker<RotateAuthoring>
    {
        public override void Bake(RotateAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new RotateData
            {
                minSpeed = authoring.minSpeed,
                maxSpeed = authoring.maxSpeed
            });
        }
    }
}

public struct RotateData : IComponentData
{
    public float minSpeed;
    public float maxSpeed;
    public float speed;
}