using UnityEngine;

public class FXManager : MonoBehaviour
{

    [Header("Visual Effects")]
    [SerializeField] private GameObject catchVFXPrefab;
    [SerializeField] private GameObject missVFXPrefab;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip catchSound;
    [SerializeField] private AudioClip missSound;

    public void PlayFeedback(float score, Vector3 position)
    {
        if (score > 0)
        {
            // Позитивний фідбек (спіймали)
            if (catchVFXPrefab != null) Instantiate(catchVFXPrefab, position, Quaternion.identity);
            if (audioSource != null && catchSound != null) audioSource.PlayOneShot(catchSound);
        }
        else
        {
            // Негативний фідбек (пропустили/втратили)
            if (missVFXPrefab != null) Instantiate(missVFXPrefab, position, Quaternion.identity);
            if (audioSource != null && missSound != null) audioSource.PlayOneShot(missSound);
        }
    }
}