using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� ��ũ��Ʈ������ ���� ���ϴ� ������ �ƴ϶� ������ �Ʒ��θ� ���������� �����̵Ǿ� ����

public class FallSpike : Obstacle
{
    public float fallSpeed = 5f; // ������ũ�� �������� �ӵ�
    public bool isFalling = false;
    public Rigidbody2D rb;
    private PolygonCollider2D collider;

    public Vector2 moveDirection = Vector2.down;
    public float moveDistance = 10f;

    [SerializeField] private LayerMask targetLayer;     //player

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true; // ó������ ���� ������ ���� �ʵ��� Kinematic���� ����
        rb.gravityScale = 0f; // �߷� ���⵵ 0���� ����
    }
    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, moveDirection * moveDistance, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 10f, targetLayer);
        if(hit.collider != null)
        {
            isFalling = true;
        }

        if (isFalling)
        {
            // isKinematic�� true�� �� ���� transform.position�� �����ϰų�
            // rb.velocity�� ����Ͽ� ����߸� �� �ֽ��ϴ�.
            // ���⼭�� �߷��� �Ѵ� ������� �����մϴ�.
            if (rb.isKinematic)
            {
                rb.isKinematic = false;
                rb.gravityScale = 10f; // �߷� Ȱ��ȭ
            }
        }
    }

    // ������ũ�� ����߸��� �ܺ� ȣ�� �Լ�
    public void StartFalling()
    {
        if (!isFalling) // �̹� �������� ���� �ʴٸ�
        {
            isFalling = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.gameObject.GetComponent<PlayerController1>().Die();
    }
}

    /*public override void SetCollider()
    {
        gameObject.AddComponent<TriangleCollider>();
        collider = GetComponent<PolygonCollider2D>();
        collider.isTrigger = true;
    }
    */
