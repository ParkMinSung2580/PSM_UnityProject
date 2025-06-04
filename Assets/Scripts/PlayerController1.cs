using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController1 : MonoBehaviour
{
    //코요태 시간, 점프 버퍼링 적용
    private float horizontal;
    private float speed = 2f;
    private float jumpingPower = 5f;
    private bool isFacingRight = true;

    private bool isJumping;

    private float coyoteTime = 0.15f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


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
            anim.SetTrigger("jump");
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
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

            coyoteTimeCounter = 0f;
        }

        Flip();
        anim.SetBool("run", horizontal != 0);
        anim.SetBool("grounded", IsGrounded());
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

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

    public bool IsFacingRight()
    {
        return isFacingRight;
    }
}
