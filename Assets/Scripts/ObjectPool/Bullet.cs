using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _distance = 10f;
    private Vector2 _fireDir = Vector2.right;

    private Vector2 _startPosition;

    private void OnEnable() //Bullet ��ü�� Ȱ��ȭ �� ��
    {
        _startPosition = transform.position; // Ȱ��ȭ�� ������ ���� ��ġ ����
        //transform.SetParent(null); // �÷��̾� �����ӿ� ������� �ʵ���
    }

    private void Update()
    {
        //���ʸ� ������ �÷��̾��� FlipX�� ���� ������ �˾ƿͼ� ������ ���� �����Ͽ� ������ ���ֱ�
        Shot(_fireDir);

        // ȭ�� ������ ������ ��Ȱ��ȭ
        float travelDistance = Vector2.Distance(_startPosition, transform.position);
        if (travelDistance > _distance)
        {         
            gameObject.SetActive(false);
            //transform.position = new Vector2(_startPosition.x, _startPosition.y);
        }
    }

    public void Shot(Vector2 dic)
    {
        transform.Translate(dic.normalized * _speed * Time.deltaTime);
    }

    // �ܺο��� ������ �����ϴ� �޼���
    public void SetDirection(Vector2 direction)
    {
        _fireDir = direction;
    }

}
