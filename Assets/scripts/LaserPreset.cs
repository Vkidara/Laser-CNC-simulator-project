public enum MaterialType
{
    Wood,
    Metal
}

[System.Serializable]
public class LaserPreset
{
    public MaterialType MaterialType;
    public float MinPower;
    public float MaxPower;
    public float MinSpeed;
    public float MaxSpeed;
    public int MinPasses;
    public int MaxPasses;

    public LaserPreset(MaterialType type, float minPower, float maxPower, float minSpeed, float maxSpeed, int minPasses, int maxPasses)
    {
        MaterialType = type;
        MinPower = minPower;
        MaxPower = maxPower;
        MinSpeed = minSpeed;
        MaxSpeed = maxSpeed;
        MinPasses = minPasses;
        MaxPasses = maxPasses;
    }
}

