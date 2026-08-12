using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class WorkpieceCuttingSurface : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public Material revealShaderMaterial;
    private Coroutine revealCoroutine;
    private Sprite targetSprite;
    private float revealDuration = 5f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ApplyCutting(Sprite cuttingSprite, float duration)
    {
        if (cuttingSprite == null)
        {
            Debug.LogWarning("Нет спрайта для резки!");
            return;
        }

        targetSprite = cuttingSprite;
        revealDuration = duration;

        StartRevealEffect();
    }

    private void StartRevealEffect()
    {
        if (revealCoroutine != null)
            StopCoroutine(revealCoroutine);

        if (revealShaderMaterial == null)
        {
            Debug.LogError("Материал шейдера резки не назначен!");
            return;
        }

        Material instanceMat = new Material(revealShaderMaterial);
        instanceMat.SetFloat("_Cutoff", 1f);
        spriteRenderer.material = instanceMat;

        revealCoroutine = StartCoroutine(RevealCoroutine(instanceMat));
    }

    private IEnumerator RevealCoroutine(Material mat)
    {
        spriteRenderer.sprite = targetSprite;

        float time = 0f;

        while (time < revealDuration)
        {
            float t = time / revealDuration;
            mat.SetFloat("_Cutoff", 1f - t);
            time += Time.deltaTime;
            yield return null;
        }

        mat.SetFloat("_Cutoff", 0f);
        spriteRenderer.material = new Material(Shader.Find("Sprites/Default"));
    }
}
