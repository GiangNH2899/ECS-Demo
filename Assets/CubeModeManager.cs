public static class CubeModeManager
{
    public static CubeMode Mode = CubeMode.Normal;
}

public enum CubeMode
{
    Normal,
    AsyncAwait,
    Job,
    JobBurst,
    EcsJobBurst
}