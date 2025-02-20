using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class CeilingTransparency : MonoBehaviour
{
    public Tilemap ceiling; // 천장 타일맵 참조
    private Coroutine fadeCoroutine;
    public float fadeDuration = 0.3f; // 페이드 지속 시간 (초)

    private void Start()
    {
        SetCeilingAlpha(0.99f); // 초기 알파값 (0.95f)
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            StartFading(0f); // 천장을 부드럽게 투명하게
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!gameObject.activeInHierarchy) return; // 비활성화된 경우 실행하지 않음

        if (other.CompareTag("Player"))
        {
            StartFading(0.99f); 
        }
    }

    private void StartFading(float targetAlpha)
    {
        if (!gameObject.activeInHierarchy) return; // 비활성화된 경우 코루틴 실행하지 않음

        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeToAlpha(targetAlpha));
    }

    private IEnumerator FadeToAlpha(float targetAlpha)
    {
        float startAlpha = ceiling.color.a;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            SetCeilingAlpha(newAlpha);
            yield return null;
        }

        SetCeilingAlpha(targetAlpha);
        fadeCoroutine = null;
    }

    private void SetCeilingAlpha(float alpha)
    {
        Color color = ceiling.color;
        color.a = alpha;
        ceiling.color = color;
    }
}
