public static class CubeModeManager
{
    public static CubeMode Mode = CubeMode.Normal;
    public static bool EnableDummyCode = false;
}

public enum CubeMode
{
    Normal,
    Job,
    JobBurst,
    ECS,
    EcsJobBurst,
}