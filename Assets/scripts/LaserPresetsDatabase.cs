public static class LaserPresetsDatabase
{
    public static LaserPreset GetPreset(MaterialType type)
    {
        switch (type)
        {
            case MaterialType.Wood:
                return new LaserPreset(
                    MaterialType.Wood,
                    minPower: 10f,
                    maxPower: 50f,
                    minSpeed: 50f,
                    maxSpeed: 500f,
                    minPasses: 1,
                    maxPasses: 3
                );

            case MaterialType.Metal:
                return new LaserPreset(
                    MaterialType.Metal,
                    minPower: 50f,
                    maxPower: 100f,
                    minSpeed: 10f,
                    maxSpeed: 200f,
                    minPasses: 2,
                    maxPasses: 5
                );

            default:
                return null;
        }
    }
}

