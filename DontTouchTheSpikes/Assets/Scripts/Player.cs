using System.Collections;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameController gameController;
    [SerializeField]
    private GameObject playerDieEffect; // 플레이어 사망 이펙트
    [SerializeField]
    private PlayerTrailSpawner playerTrailSpawner;
    [SerializeField]
    private float moveSpeed = 4.5f;      // 이동 속도
    [SerializeField]
    private float jumpForce = 12f;      // 점프 힘

    private AudioSource audioSource;    // 벽 충돌 사운드 재생을 위한 AudioSource
    private Rigidbody2D rb2D;           // 속력 제어를 위한 Rigidbody2D
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.isKinematic = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = true;
    }

    private IEnumerator Start()
    {
        float originY = transform.position.y;
        float deltaY = 0.5f;
        float moveSpeedY = 2;

        while (true)
        {
            float y = originY + deltaY * Mathf.Sin(Time.time * moveSpeedY);
            transform.position = new Vector2(transform.position.x, y);

            yield return null;
        }
    }

    public void GameStart()
    {
        rb2D.isKinematic = false;
        rb2D.velocity = new Vector2(moveSpeed, jumpForce);

        StopCoroutine("Start");
        StartCoroutine("UpdateInput");
    }

    private IEnumerator UpdateInput()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                JumpTo();

                playerTrailSpawner.OnSpawns();
            }
            yield return null;
        }
    }
    private void ReverseXDir()
    {
        float x = -Mathf.Sign(rb2D.velocity.x);
        rb2D.velocity = new Vector2(x * moveSpeed, rb2D.velocity.y);
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
    private void JumpTo()
    {
        rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            // 플레이어 x축 방향 전환
            ReverseXDir();
            // 벽과 충돌했을 때 로직 (점수 증가, 색상 변경, 가시 횔성/비활성)
            gameController.CollisionWithWall();
            // 벽과 충돌했을 때 사운드 재생
            audioSource.Play();
        }
        else if (collision.CompareTag("Spike"))
        {
            // 플레이어 사망 이펙트 생성
            Instantiate(playerDieEffect, transform.position, Quaternion.identity);
            // 게임오버 처리
            gameController.GameOver();
            // 플레이어 비활성화
            gameObject.SetActive(false);
        }
    }
}
