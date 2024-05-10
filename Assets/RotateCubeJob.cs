using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Jobs;

public struct RotateCubeJob : IJobParallelForTransform
{
    public float deltaTime;
    public NativeArray<float> cubeSpeed;

    public void Execute(int index, TransformAccess transform)
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0,
            transform.rotation.eulerAngles.z + cubeSpeed[index] * deltaTime));
    }

    Quaternion EulerToQuaternion(float3 euler) // roll (x), pitch (y), yaw (z), angles are in radians
    {
        // Abbreviations for the various angular functions

        var cr = (float)math.cos(euler.x * 0.5);
        var sr = (float)math.sin(euler.x * 0.5);
        var cp = (float)math.cos(euler.y * 0.5);
        var sp = (float)math.sin(euler.y * 0.5);
        var cy = (float)math.cos(euler.z * 0.5);
        var sy = (float)math.sin(euler.z * 0.5);
        var q = new Quaternion
        {
            w = cr * cp * cy + sr * sp * sy,
            x = sr * cp * cy - cr * sp * sy,
            y = cr * sp * cy + sr * cp * sy,
            z = cr * cp * sy - sr * sp * cy
        };

        return q;
    }
}