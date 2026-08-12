using UnityEngine;

public class WorkpieceCuttingEffect : MonoBehaviour
{
    public GameObject smokeEffect;
    public GameObject sparksEffect;

    public void PlayEffect(float power, float speed, int passes)
    {
        if (smokeEffect != null)
            smokeEffect.SetActive(true);

        if (sparksEffect != null)
            sparksEffect.SetActive(true);

        // В дальнейшем можно варьировать интенсивность по параметрам power/speed
    }

    public void StopEffect()
    {
        if (smokeEffect != null)
            smokeEffect.SetActive(false);

        if (sparksEffect != null)
            sparksEffect.SetActive(false);
    }
}

