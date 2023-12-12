using System.Collections;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameController gameController;
    [SerializeField]
    private GameObject playerDieEffect; // �÷��̾� ��� ����Ʈ
    [SerializeField]
    private PlayerTrailSpawner playerTrailSpawner;
    [SerializeField]
    private float moveSpeed = 4.5f;      // �̵� �ӵ�
    [SerializeField]
    private float jumpForce = 12f;      // ���� ��

    private AudioSource audioSource;    // �� �浹 ���� ����� ���� AudioSource
    private Rigidbody2D rb2D;           // �ӷ� ��� ���� Rigidbody2D
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
            // �÷��̾� x�� ���� ��ȯ
            ReverseXDir();
            // ���� �浹���� �� ���� (���� ����, ���� ����, ���� Ȼ��/��Ȱ��)
            gameController.CollisionWithWall();
            // ���� �浹���� �� ���� ���
            audioSource.Play();
        }
        else if (collision.CompareTag("Spike"))
        {
            // �÷��̾� ��� ����Ʈ ����
            Instantiate(playerDieEffect, transform.position, Quaternion.identity);
            // ���ӿ��� ó��
            gameController.GameOver();
            // �÷��̾� ��Ȱ��ȭ
            gameObject.SetActive(false);
        }
    }
}
