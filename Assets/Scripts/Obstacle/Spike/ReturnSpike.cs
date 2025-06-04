using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//현재 스크립트에서는 내가 원하는 방향이 아니라 위에서 아래로만 떨어지도록 구현이되어 있다
public class ReturnSpike : Obstacle
{
    public SpriteRenderer sr;
    public float _moveSpeed = 10f; // 스파이크가 떨어지는 속도

    [Header("스파이크 이동 설정")] // 인스펙터에 헤더 추가 (선택 사항)
    public Vector2 moveDirection;   //방향값
    public float moveDistance = 5f; //카메라 화면 밖까지 이동 하게 //추후 특정 y좌표까지 떨어지게 구성

    [SerializeField] private LayerMask targetLayer;     //player

    private bool isMoving = false;
    private Vector3 initialPosition; // 초기 위치 저장
    private Vector3 targetPosition; // 이동 목표 위치
    private enum SpikeState { Idle, MovingDown, MovingUp } // 스파이크 상태 정의
    private SpikeState currentState = SpikeState.Idle;


    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        initialPosition = transform.position;
    }
    void FixedUpdate()
    {
        //TODO - 방향 수정 필요 
        Debug.DrawRay(transform.position, moveDirection * moveDistance, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection.normalized, 10f, targetLayer);

        if(hit && !isMoving)
        {
            StartMovingDown();
        }

        if(targetPosition == transform.position)
        {
            StartMovingUp();
        }

        //문제하나 더 발견 collider 이 안변함 -> localscale을 변경해주어야함
        switch (currentState)
        {
            case SpikeState.Idle:
                // 아무것도 하지 않음
                break;

            case SpikeState.MovingDown:
                if(!isMoving)
                    isMoving = true;
                // 목표 위치로 이동
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, _moveSpeed * Time.fixedDeltaTime);
                
                // 목표 위치에 도달했으면 Idle 상태로 전환
                if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
                {
                    transform.position = targetPosition; // 정확히 목표 위치로 설정
                    //sr.flipY = !sr.flipY;
                    transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
                    currentState = SpikeState.Idle; // 상태를 Idle로 변경
                    isMoving = false;
                    Debug.Log(gameObject.name + " 스파이크가 목표 지점에 도달했습니다.");
                }
                break;

            case SpikeState.MovingUp:
                // 초기 위치로 올라갑니다.
                if (!isMoving)
                    isMoving = true;
                transform.position = Vector3.MoveTowards(transform.position, initialPosition, _moveSpeed * Time.fixedDeltaTime);

                // 초기 위치에 거의 도달했으면 Idle 상태로 전환
                if (Vector3.Distance(transform.position, initialPosition) < 0.01f)
                {
                    transform.position = initialPosition; // 정확히 초기 위치로 설정
                    currentState = SpikeState.Idle; // 상태를 Idle로 변경
                    //filpY를 변경
                    //sr.flipY = !sr.flipY;
                    transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
                    isMoving = false;
                    Debug.Log(gameObject.name + " 스파이크가 초기 위치로 돌아왔습니다.");
                }
                break;
        }
    }

    // 스파이크를 떨어뜨리는 외부 호출 함수
    public void StartMovingDown()
    {
        if (currentState != SpikeState.MovingDown)
        {
            currentState = SpikeState.MovingDown;
            targetPosition = initialPosition + (Vector3)moveDirection.normalized * moveDistance;
            Debug.Log(gameObject.name + " 스파이크가 " + moveDirection + " 방향으로 움직이기 시작합니다!");
        }
    }

    // 스파이크를 초기 위치로 되돌리는 외부 호출 함수
    public void StartMovingUp()
    {
        if (currentState != SpikeState.MovingUp)
        {
            currentState = SpikeState.MovingUp;
            Debug.Log(gameObject.name + " 스파이크가 초기 위치로 돌아가기 시작합니다.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.gameObject.GetComponent<PlayerController2>().Die();
    }
}

    /*public override void SetCollider()
    {
        gameObject.AddComponent<TriangleCollider>();
        collider = GetComponent<PolygonCollider2D>();
        collider.isTrigger = true;
    }
    */
