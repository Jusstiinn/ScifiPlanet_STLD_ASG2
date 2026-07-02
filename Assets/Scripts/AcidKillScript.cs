using System.Collections;
using UnityEngine;

public class AcidKillScript : MonoBehaviour
{
    public Transform teleportPoint;
    public CanvasGroup blackScreen;
    public float fadeDuration = 1f;

    //triggers when player touches the trigger.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TeleportRoutine(other));
        }
    }

    IEnumerator TeleportRoutine(Collider other)
    {
        // Fade to black
        yield return StartCoroutine(Fade(0f, 1f));

        // Get and disable character controller
        CharacterController cc = other.GetComponent<CharacterController>();
        if (cc != null)
            cc.enabled = false;
            
        // teleport
        other.transform.position = teleportPoint.position;
        other.transform.rotation = teleportPoint.rotation;

        if (cc != null)
            cc.enabled = true;

        // Fade back in
        yield return StartCoroutine(Fade(1f, 0f));
    }

    IEnumerator Fade(float start, float end)
    {
        float elapsed = 0f;
        // smoothly fade the black screen in or out
        while (elapsed < fadeDuration)
        {
            // to give it a time value 
            elapsed += Time.deltaTime;
            //to smoothly transition alpha values
            blackScreen.alpha = Mathf.Lerp(start, end, elapsed / fadeDuration);
            yield return null;
        }

        blackScreen.alpha = end;
    }
}
