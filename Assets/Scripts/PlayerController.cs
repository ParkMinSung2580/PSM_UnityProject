using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;

    //�÷��̾� ��ǲ Horizontal��
    private float inputX;
    private Rigidbody2D playerRb;
    private SpriteRenderer playerSr;
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerSr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        PlayerInput();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            playerJump();
        }
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }
    private void PlayerInput()
    {
        inputX = Input.GetAxis("Horizontal");
    }

    private void PlayerMove() 
    {
        if (inputX == 0)
        {
            //�ƹ��͵� �������� ���߿� Idle �۵�
            return;
        }

        /*// Surface Effector�� ��ǲ���� �߰��ϴ� ���.
        // 1. ��ǲ�� ���� Surface Effector 2D �� �������� ���� �߰�.
        // 2. velocity ������ �ƴ� transform,AddForce �̵� ���.
        if (surfaceEffector == null)
        {
            rigid.velocity = new Vector2(inputX * moveSpeed, rigid.velocity.y);
        }
        else
        {
            rigid.velocity = new Vector2((inputX * moveSpeed) + surfaceEffector.speed, rigid.velocity.y);
        }*/
        playerRb.velocity = new Vector2(inputX * moveSpeed, playerRb.velocity.y);
        if (inputX < 0)
        {
            playerSr.flipX = true;
        }
        else
        {
            playerSr.flipX = false;
        }

    }

    private void playerJump()
    {
        playerRb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
    }
}
