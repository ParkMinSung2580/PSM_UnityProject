using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;

    //플레이어 인풋 Horizontal값
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
            //아무것도 하지않음 나중에 Idle 작동
            return;
        }

        /*// Surface Effector에 인풋값을 추가하는 방식.
        // 1. 인풋에 직접 Surface Effector 2D 에 가해지는 힘을 추가.
        // 2. velocity 조정이 아닌 transform,AddForce 이동 사용.
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
