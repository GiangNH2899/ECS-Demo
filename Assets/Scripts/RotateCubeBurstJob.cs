using Unity.Burst;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Jobs;

[BurstCompile]
public struct RotateCubeBurstJob : IJobParallelForTransform
{
    public float deltaTime;
    public NativeArray<float> cubeSpeed;
    public bool isEnableDummy;

    public void Execute(int index, TransformAccess transform)
    {
        if (isEnableDummy)
        {
            var dummy = 0;
            for (int i = 0; i < 1000; i++)
            {
                dummy++;
            }
        }

        transform.rotation = Quaternion.Euler(new Vector3(0, 0,
            transform.rotation.eulerAngles.z + cubeSpeed[index] * deltaTime));
    }
}