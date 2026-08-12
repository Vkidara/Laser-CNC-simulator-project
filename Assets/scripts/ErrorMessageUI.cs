using UnityEngine;
using TMPro;
using System.Collections;

public class ErrorMessageUI : MonoBehaviour
{
    public TMP_Text errorText; // —юда прив€жем текстовое поле
    public float displayDuration = 2f; // —колько секунд показывать

    private Coroutine currentCoroutine;

    public void ShowError(string message)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(DisplayMessage(message));
    }

    private IEnumerator DisplayMessage(string message)
    {
        errorText.text = message;
        errorText.gameObject.SetActive(true);

        yield return new WaitForSeconds(displayDuration);

        errorText.gameObject.SetActive(false);
    }
}
