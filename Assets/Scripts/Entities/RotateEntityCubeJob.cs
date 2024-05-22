using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

[BurstCompile]
public partial struct RotateEntityCubeJob : IJobEntity
{
    public float deltaTime;
    public bool isEnableDummy;
    
    public void Execute(ref LocalTransform localTransform, in RotateData rotateData)
    {
        if (isEnableDummy)
        {
            var dummy = 0;
            for (int i = 0; i < 1000; i++)
            {
                dummy++;
            }
        }
        
        localTransform =
            localTransform.RotateZ(
                rotateData.speed *
                deltaTime * 0.05f);
    }
}