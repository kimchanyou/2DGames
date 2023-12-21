using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameController gameController;
    [SerializeField]
    private GameObject playerDieEffect; // 플레이어 사망 이펙트
    [SerializeField]
    private PlayerTrailSpawner playerTrailSpawner;
    [SerializeField]
    private float moveSpeed = 4.8f;      // 이동 속도
    [SerializeField]
    private float jumpForce = 7.8f;      // 점프 힘
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private AudioClip[] sounds;

    private AudioSource audioSource;    // 벽 충돌 사운드 재생을 위한 AudioSource
    private Rigidbody2D rb2D;           // 속력 제어를 위한 Rigidbody2D
    private CapsuleCollider2D col2D;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.isKinematic = true;
        col2D = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = true;
        slider.value = 1;
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
            if (Mathf.Abs(transform.position.y) > 8)
                GameOver();
            if (Input.GetMouseButtonDown(0))
            {
                JumpTo();
                audioSource.PlayOneShot(sounds[1]);
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
        col2D.offset = new Vector2(-col2D.offset.x, col2D.offset.y);
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
            audioSource.PlayOneShot(sounds[3]);

            if (gameController.currentScore % 15 == 0)
            {
                foreach (LightSpawner spawner in gameController.lightSpawners)
                {
                    if (spawner.maxValue == 2)
                        return;
                    if (spawner.minValue < 5)
                        spawner.minValue++;
                    else
                        spawner.maxValue--;
                }
            }

            if (gameController.currentScore > 99 && gameController.currentScore % 10 == 0)
            {
                if(moveSpeed < 5.7f)
                {
                    moveSpeed += 0.1f;
                }
            }

        }
        else if(collision.CompareTag("Heart"))
        {
            audioSource.PlayOneShot(sounds[0]);
            if (slider.value <= 0.8f)
                slider.value += 0.2f;
            else
                slider.value = 1;
        }
        else if (collision.CompareTag("Light"))
        {
            audioSource.PlayOneShot(sounds[2]);
            if (slider.value <= 0)
            {
                GameOver();
            }
            
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Light"))
        {
            spriteRenderer.color = Color.red;
            slider.value -= Time.deltaTime * 0.7f;
            if (slider.value <= 0)
            {
                GameOver();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Light"))
        {
            spriteRenderer.color = Color.white;
        }
    }

    private void GameOver()
    {
        // 플레이어 사망 이펙트 생성
        Instantiate(playerDieEffect, transform.position, Quaternion.identity);
        // 게임오버 처리
        gameController.GameOver();
        // 플레이어 비활성화
        gameObject.SetActive(false);
    }
}
