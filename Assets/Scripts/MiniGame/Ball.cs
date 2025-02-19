using UnityEngine;

public class Ball : MonoBehaviour
{
    public float minBounceHeight = 5f; // 최소 튀는 높이
    public float maxBounceHeight = 8f; // 최대 튀는 높이
    public float destroyHeight = -10f; // 이 높이 이하로 떨어지면 삭제

    private Rigidbody2D rb;
    private float bounceHeight; // 각 공의 튀는 높이

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 각 공이 랜덤한 높이로 튀도록 설정
        bounceHeight = Random.Range(minBounceHeight, maxBounceHeight);
        
        // 초기 점프
        rb.velocity = new Vector2(rb.velocity.x, bounceHeight);
    }

    private void Update()
    {
        // Y축이 -10 이하로 내려가면 삭제
        if (transform.position.y < destroyHeight)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 바닥에 닿으면 위로 튀기기
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.velocity = new Vector2(rb.velocity.x, bounceHeight);
        }
    }
}
