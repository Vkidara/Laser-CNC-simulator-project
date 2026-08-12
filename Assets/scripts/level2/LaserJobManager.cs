using UnityEngine;

public class LaserJobManager : MonoBehaviour
{
    public static LaserJobManager Instance;

    public enum JobType { None, Engraving, Cutting }

    public JobType CurrentJob { get; private set; } = JobType.None;
    public string SelectedPattern { get; private set; } = "";

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SetCuttingJob(string patternName)
    {
        CurrentJob = JobType.Cutting;
        SelectedPattern = patternName;
        Debug.Log($"[LaserJobManager] Выбран режим: Резка, чертеж: {patternName}");
    }



    public void ResetJob()
    {
        CurrentJob = JobType.None;
        SelectedPattern = "";

    }
}

