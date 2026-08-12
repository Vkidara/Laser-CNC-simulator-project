using UnityEngine;

public class Radio : MonoBehaviour, IInteractable
{
    public AudioSource audioSource;
    public AudioClip[] tracks;
    private int currentTrackIndex = 0;
    private bool isOn = false;

    private QuestManager questManager;

    void Start()
    {
        questManager = GameObject.Find("QuestManager").GetComponent<QuestManager>();
    }

    public void ToggleRadio()
    {
        isOn = !isOn;

        if (isOn)
        {
            audioSource.clip = tracks[currentTrackIndex];
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }

        if (questManager != null)
        {
            questManager.CompleteQuest(2, isOn);
        }
    }

    public void NextTrack()
    {
        if (!isOn) return;

        currentTrackIndex = (currentTrackIndex + 1) % tracks.Length;
        audioSource.clip = tracks[currentTrackIndex];
        audioSource.Play();
    }

    public bool IsRadioOn()
    {
        return isOn;
    }

    // >>> Реализация IInteractable
    public string GetInteractionText()
    {
        return isOn ? "Выключить радио" : "Включить радио";
    }

    public void Interact()
    {
        ToggleRadio();
    }
}


