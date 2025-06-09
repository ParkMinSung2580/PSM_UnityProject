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

    private float coyoteTime = 0.5f;
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

        //플레이어가 벽에 붙는 문제가 아직 존재

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
        anim.SetBool("grounded", IsGrounded()); //두번 호출 하는 것 보다 변수를 쓰는게 좋을 듯
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        // 1차 : 일단 플레이어 밑에 레이쏴서 Ground레이어 총돌제가 없으면 return 있으면 2차 감지
        RaycastHit2D rayHit = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.05f, groundLayer);

        if (rayHit.collider != null)
        {
            // 2차: OverlapCircle로 안정성 확보
            return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        }
        else
        {
            return false;
        }
        
        //return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
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

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.blue;
            //Gizmos.DrawWireSphere(groundCheck.position, 0.03f);
            Gizmos.DrawLine(groundCheck.transform.position, groundCheck.transform.position - new Vector3(0, 0.05f, 0));
        }
    }
}
