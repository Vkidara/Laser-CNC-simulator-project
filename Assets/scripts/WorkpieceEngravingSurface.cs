using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class WorkpieceEngravingSurface : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public SpriteRenderer burnEffectRenderer;
    public GameObject smokeEffect;
    public Material revealShaderMaterial;

    private Coroutine revealCoroutine;
    private Sprite targetSprite;
    private Color targetColor;
    private string currentEffectType = "default";

    private float burnTargetAlpha = 0f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ApplyEngraving(Sprite engraving, string effectType = "default", float revealDuration = 5f)
    {
        if (engraving == null)
        {
            Debug.LogWarning("Нет спрайта для гравировки!");
            return;
        }

        targetSprite = engraving;
        currentEffectType = effectType;

        if (smokeEffect != null)
            smokeEffect.SetActive(false);

        // Устанавливаем видимость для разных эффектов
        switch (effectType)
        {
            case "none":
                targetColor = new Color(1f, 1f, 1f, 0.10f); // почти незаметный, но есть
                break;
            case "faint":
                targetColor = new Color(1f, 1f, 1f, 0.15f); // слабая гравировка
                break;
            case "default":
                targetColor = new Color(1f, 1f, 1f, 1f); // нормальное изображение
                break;
            case "burn":
                targetColor = new Color(1f, 1f, 1f, 1f); // немного затемнённое основание
                burnTargetAlpha = 0.7f; // наложение копоти
                break;
            case "overburn":
                targetColor = new Color(1f, 1f, 1f, 1f); // заметно потемневшее основание
                burnTargetAlpha = 0.95f; // сильно затемнённая копоть
                break;
            default:
                targetColor = Color.white;
                break;
        }

        spriteRenderer.sprite = engraving;
        spriteRenderer.color = targetColor;

        if (burnEffectRenderer != null)
        {
            if (effectType == "burn" || effectType == "overburn")
            {
                burnEffectRenderer.enabled = true;
                burnEffectRenderer.sprite = engraving;
                burnEffectRenderer.color = new Color(0f, 0f, 0f, 0f);
            }
            else
            {
                burnEffectRenderer.enabled = false;
            }
        }

        StartRevealEffect(revealDuration);

        if (effectType == "default" || effectType == "burn" || effectType == "overburn")
        {
            PlaySmokeEffect();
            Invoke(nameof(StopSmokeEffect), 7f);
        }
    }

    public void StartRevealEffect(float duration)
    {
        if (revealCoroutine != null)
            StopCoroutine(revealCoroutine);

        if (revealShaderMaterial == null)
        {
            Debug.LogError("Материал шейдера проявления не назначен!");
            return;
        }

        Material instanceMat = new Material(revealShaderMaterial);
        instanceMat.SetFloat("_Cutoff", 1f);

        spriteRenderer.material = instanceMat;

        revealCoroutine = StartCoroutine(RevealCoroutine(instanceMat, duration));
    }

    private IEnumerator RevealCoroutine(Material mat, float duration)
    {
        spriteRenderer.sprite = targetSprite;
        spriteRenderer.color = targetColor;

        float time = 0f;
        bool hasBurn = burnEffectRenderer != null && burnEffectRenderer.enabled;

        while (time < duration)
        {
            float t = time / duration;
            mat.SetFloat("_Cutoff", 1f - t);

            if (hasBurn)
            {
                float currentAlpha = Mathf.Lerp(0f, burnTargetAlpha, t);
                burnEffectRenderer.color = new Color(0f, 0f, 0f, currentAlpha);
            }

            time += Time.deltaTime;
            yield return null;
        }

        mat.SetFloat("_Cutoff", 0f);

        if (hasBurn)
        {
            burnEffectRenderer.color = new Color(0f, 0f, 0f, burnTargetAlpha);
        }

        spriteRenderer.material = new Material(Shader.Find("Sprites/Default"));
    }

    public void ClearEngraving()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = null;
            spriteRenderer.material = null;
        }

        if (burnEffectRenderer != null)
            burnEffectRenderer.enabled = false;
    }

    public void PlaySmokeEffect()
    {
        if (smokeEffect != null)
        {
            var ps = smokeEffect.GetComponent<ParticleSystem>();
            if (ps != null)
                ps.Play();
            else
                smokeEffect.SetActive(true);
        }
    }

    public void StopSmokeEffect()
    {
        if (smokeEffect != null)
        {
            var ps = smokeEffect.GetComponent<ParticleSystem>();
            if (ps != null)
                ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            else
                smokeEffect.SetActive(false);
        }
    }
}
