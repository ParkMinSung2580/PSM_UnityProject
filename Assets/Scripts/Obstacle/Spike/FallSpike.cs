using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//현재 스크립트에서는 내가 원하는 방향이 아니라 위에서 아래로만 떨어지도록 구현이되어 있음

public class FallSpike : Obstacle
{
    public float fallSpeed = 5f; // 스파이크가 떨어지는 속도
    public bool isFalling = false;
    public Rigidbody2D rb;
    private PolygonCollider2D collider;

    public Vector2 moveDirection = Vector2.down;
    public float moveDistance = 10f;

    [SerializeField] private LayerMask targetLayer;     //player

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true; // 처음에는 물리 영향을 받지 않도록 Kinematic으로 설정
        rb.gravityScale = 0f; // 중력 영향도 0으로 설정
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
            // isKinematic이 true일 때 직접 transform.position을 변경하거나
            // rb.velocity를 사용하여 떨어뜨릴 수 있습니다.
            // 여기서는 중력을 켜는 방식으로 구현합니다.
            if (rb.isKinematic)
            {
                rb.isKinematic = false;
                rb.gravityScale = 10f; // 중력 활성화
            }
        }
    }

    // 스파이크를 떨어뜨리는 외부 호출 함수
    public void StartFalling()
    {
        if (!isFalling) // 이미 떨어지고 있지 않다면
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
