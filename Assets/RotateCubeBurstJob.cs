using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

public struct RotateCubeBurstJob : IJob
{
    public float deltaTime;
    public float currentRotation;
    public float cubeSpeed;

    public NativeArray<float> newRotate;

    [BurstCompile]
    public void Execute()
    {
        newRotate[0] = currentRotation + cubeSpeed * deltaTime;
    }
}