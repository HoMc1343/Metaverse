using System.Collections;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab; // 생성할 공 프리팹
    public Transform leftSpawnPoint; // 왼쪽 스폰 위치
    public Transform rightSpawnPoint; // 오른쪽 스폰 위치
    public float spawnInterval = 2f; // 공 생성 간격
    public float minBallSize = 0.5f; // 최소 공 크기
    public float maxBallSize = 1.5f; // 최대 공 크기
    public float ballSpeed = 3f; // 공 이동 속도

    private void Start()
    {
        StartCoroutine(SpawnBalls());
    }

    private IEnumerator SpawnBalls()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnBall();
        }
    }

    private void SpawnBall()
    {
        // 왼쪽(0) 또는 오른쪽(1)에서 랜덤으로 생성
        bool spawnLeft = Random.Range(0, 2) == 0;
        Transform spawnPoint = spawnLeft ? leftSpawnPoint : rightSpawnPoint;

        // 공 생성
        GameObject ball = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);

        // 공의 크기 설정 (랜덤)
        float size = Random.Range(minBallSize, maxBallSize);
        ball.transform.localScale = new Vector3(size, size, 1f);

        // 공의 색상 설정 (랜덤)
        SpriteRenderer ballRenderer = ball.GetComponent<SpriteRenderer>();
        ballRenderer.color = new Color(Random.value, Random.value, Random.value);

        // 공 이동 방향 설정 (왼쪽에서 생성되면 오른쪽으로, 오른쪽에서 생성되면 왼쪽으로)
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        Vector2 direction = spawnLeft ? Vector2.right : Vector2.left;
        rb.velocity = direction * ballSpeed;
    }
}
