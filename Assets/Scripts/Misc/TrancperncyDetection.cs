using System.Collections;
using UnityEngine;

public class TrancperncyDetection : MonoBehaviour
{
    private const float FULL_NON_TRANSPERENT = 1.0f;

    [Range(0.0f, 1.0f)]
    [SerializeField] private float transperencyAmount = 0.8f;
    [SerializeField] private float fadeTime = 0.5f;

    SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<Player>())
        {
            if(collider is CapsuleCollider2D)
                StartCoroutine(FadeRoutine(_spriteRenderer, fadeTime, _spriteRenderer.color.a, transperencyAmount));
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<Player>())
        {
            if (collider is CapsuleCollider2D)
                StartCoroutine(FadeRoutine(_spriteRenderer, fadeTime, _spriteRenderer.color.a, FULL_NON_TRANSPERENT));
        }
    }

    private IEnumerator FadeRoutine(SpriteRenderer spriteRenderer,
        float fadeTime, float startTransperencyAmount, float targetTransperencyAmount)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpfa = Mathf.Lerp(startTransperencyAmount, targetTransperencyAmount, elapsedTime / fadeTime);
            _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, newAlpfa);

            yield return null;
        }
    }
}
