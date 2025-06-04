using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController2 : MonoBehaviour
{
    //더블점프
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;

    private bool isJumping;
    private bool canDoubleJump = false;
    private float coyoteTime = 0.15f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    private bool _isdoubleJump = false; 

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
     

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        //if    플레이어가 땅에 있으면 코요테 타임 리셋
        //else  플레이어가 공중에 있으면 감소
        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else 
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        //if    //점프키 누르면 버퍼 시간 설정
        //else  //시간이 지나면 감소
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        //점프 및 점프 버퍼 리셋 쿨
        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping)
        {

            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            jumpBufferCounter = 0f;
            StartCoroutine(JumpCooldown());
            // 첫 점프 후 더블 점프 가능하도록 설정
            canDoubleJump = true;
        }
        // --- 새로운 기능: 더블 점프 로직 ---
        else if (Input.GetButtonDown("Jump") && canDoubleJump)
        {
            // 더블 점프 실행
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower * 0.5f);
            canDoubleJump = false; // 더블 점프 사용 후 비활성화
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

            coyoteTimeCounter = 0f;
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    //나중에 무기도 추가할꺼 생각해서 Sprite Render의 Flip x값 조정보다는 방향을 바꿔주자 
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
    }

    public void Die()
    {
        Debug.Log("플레이어 죽음");
    }
}
