using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController2 : MonoBehaviour
{
    //��������
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

        //if    �÷��̾ ���� ������ �ڿ��� Ÿ�� ����
        //else  �÷��̾ ���߿� ������ ����
        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else 
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        //if    //����Ű ������ ���� �ð� ����
        //else  //�ð��� ������ ����
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        //���� �� ���� ���� ���� ��
        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping)
        {

            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            jumpBufferCounter = 0f;
            StartCoroutine(JumpCooldown());
            // ù ���� �� ���� ���� �����ϵ��� ����
            canDoubleJump = true;
        }
        // --- ���ο� ���: ���� ���� ���� ---
        else if (Input.GetButtonDown("Jump") && canDoubleJump)
        {
            // ���� ���� ����
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower * 0.5f);
            canDoubleJump = false; // ���� ���� ��� �� ��Ȱ��ȭ
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

    //���߿� ���⵵ �߰��Ҳ� �����ؼ� Sprite Render�� Flip x�� �������ٴ� ������ �ٲ����� 
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
        Debug.Log("�÷��̾� ����");
    }
}
