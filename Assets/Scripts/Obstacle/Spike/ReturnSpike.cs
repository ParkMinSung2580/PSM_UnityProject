using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� ��ũ��Ʈ������ ���� ���ϴ� ������ �ƴ϶� ������ �Ʒ��θ� ���������� �����̵Ǿ� �ִ�
public class ReturnSpike : Obstacle
{
    public SpriteRenderer sr;
    public float _moveSpeed = 10f; // ������ũ�� �������� �ӵ�

    [Header("������ũ �̵� ����")] // �ν����Ϳ� ��� �߰� (���� ����)
    public Vector2 moveDirection;   //���Ⱚ
    public float moveDistance = 5f; //ī�޶� ȭ�� �۱��� �̵� �ϰ� //���� Ư�� y��ǥ���� �������� ����

    [SerializeField] private LayerMask targetLayer;     //player

    private bool isMoving = false;
    private Vector3 initialPosition; // �ʱ� ��ġ ����
    private Vector3 targetPosition; // �̵� ��ǥ ��ġ
    private enum SpikeState { Idle, MovingDown, MovingUp } // ������ũ ���� ����
    private SpikeState currentState = SpikeState.Idle;


    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        initialPosition = transform.position;
    }
    void FixedUpdate()
    {
        //TODO - ���� ���� �ʿ� 
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

        //�����ϳ� �� �߰� collider �� �Ⱥ��� -> localscale�� �������־����
        switch (currentState)
        {
            case SpikeState.Idle:
                // �ƹ��͵� ���� ����
                break;

            case SpikeState.MovingDown:
                if(!isMoving)
                    isMoving = true;
                // ��ǥ ��ġ�� �̵�
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, _moveSpeed * Time.fixedDeltaTime);
                
                // ��ǥ ��ġ�� ���������� Idle ���·� ��ȯ
                if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
                {
                    transform.position = targetPosition; // ��Ȯ�� ��ǥ ��ġ�� ����
                    //sr.flipY = !sr.flipY;
                    transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
                    currentState = SpikeState.Idle; // ���¸� Idle�� ����
                    isMoving = false;
                    Debug.Log(gameObject.name + " ������ũ�� ��ǥ ������ �����߽��ϴ�.");
                }
                break;

            case SpikeState.MovingUp:
                // �ʱ� ��ġ�� �ö󰩴ϴ�.
                if (!isMoving)
                    isMoving = true;
                transform.position = Vector3.MoveTowards(transform.position, initialPosition, _moveSpeed * Time.fixedDeltaTime);

                // �ʱ� ��ġ�� ���� ���������� Idle ���·� ��ȯ
                if (Vector3.Distance(transform.position, initialPosition) < 0.01f)
                {
                    transform.position = initialPosition; // ��Ȯ�� �ʱ� ��ġ�� ����
                    currentState = SpikeState.Idle; // ���¸� Idle�� ����
                    //filpY�� ����
                    //sr.flipY = !sr.flipY;
                    transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
                    isMoving = false;
                    Debug.Log(gameObject.name + " ������ũ�� �ʱ� ��ġ�� ���ƿԽ��ϴ�.");
                }
                break;
        }
    }

    // ������ũ�� ����߸��� �ܺ� ȣ�� �Լ�
    public void StartMovingDown()
    {
        if (currentState != SpikeState.MovingDown)
        {
            currentState = SpikeState.MovingDown;
            targetPosition = initialPosition + (Vector3)moveDirection.normalized * moveDistance;
            Debug.Log(gameObject.name + " ������ũ�� " + moveDirection + " �������� �����̱� �����մϴ�!");
        }
    }

    // ������ũ�� �ʱ� ��ġ�� �ǵ����� �ܺ� ȣ�� �Լ�
    public void StartMovingUp()
    {
        if (currentState != SpikeState.MovingUp)
        {
            currentState = SpikeState.MovingUp;
            Debug.Log(gameObject.name + " ������ũ�� �ʱ� ��ġ�� ���ư��� �����մϴ�.");
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
