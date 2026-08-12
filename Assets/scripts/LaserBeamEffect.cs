using UnityEngine;

public class LaserBeamEffect : MonoBehaviour
{
    public Transform laserHead; // точка старта луча
    public Transform laserTarget; // точка попадания (например, основание станка)
    public LineRenderer lineRenderer;

    public GameObject lightSmokePrefab;
    public GameObject heavySmokePrefab;

    private GameObject currentSmoke;
    private float currentSpeed = 1f;
    private float currentPower = 1f;

    private bool isFiring = false;
    private float animationOffset = 0f;

    public void StartLaser(float power, float speed, string effectType)
    {
        currentPower = power;
        currentSpeed = speed;
        isFiring = true;

        lineRenderer.enabled = true;

        // Создание эффекта дыма
        if (effectType == "default" && lightSmokePrefab)
            currentSmoke = Instantiate(lightSmokePrefab, laserTarget.position, Quaternion.identity, transform);
        else if (effectType == "burn" && heavySmokePrefab)
            currentSmoke = Instantiate(heavySmokePrefab, laserTarget.position, Quaternion.identity, transform);
    }

    public void StopLaser()
    {
        isFiring = false;
        lineRenderer.enabled = false;

        if (currentSmoke)
            Destroy(currentSmoke);
    }

    void Update()
    {
        if (!isFiring || laserHead == null || laserTarget == null || lineRenderer == null)
            return;

        // Анимированное дрожание луча
        animationOffset += Time.deltaTime * currentSpeed;
        Vector3 offset = new Vector3(0f, Mathf.Sin(animationOffset * 5f) * 0.01f, 0f);

        // Устанавливаем позиции луча
        lineRenderer.SetPosition(0, laserHead.position);
        lineRenderer.SetPosition(1, laserTarget.position + offset);

        // Меняем цвет в зависимости от мощности
        float intensity = Mathf.Clamp01(currentPower / 100f);
        Color color = new Color(1f, 0f, 0f, 0.3f + 0.7f * intensity); // ярче при высокой мощности
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
    }
}

