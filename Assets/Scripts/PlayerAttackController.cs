using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _poolSize;
    [SerializeField] private PlayerController1 playerController; // �÷��̾� ��Ʈ�ѷ� ����

    private ObjectPool _objectPool;
    private Vector2 GetPlayerDirection()
    {
        return playerController.IsFacingRight() ? Vector2.right : Vector2.left;
    }

    private void Awake()
    {
        _objectPool = new ObjectPool(_poolSize, _prefab, transform.position);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.X)) //Ű�� �Է��ϸ� ���ݽ���
        {
            GameObject bulletObj = _objectPool.GetInactive();   //����ִ� ������Ʈ ������
            if (bulletObj != null)  //���� ��� �Դٸ� 
            {
                bulletObj.transform.position = transform.position; // �ش� ���� ������Ʈ�� �߻� ��ġ ����
                Bullet bullet = bulletObj.GetComponent<Bullet>();  //�ش� ������Ʈ�� Bullet�̶�� ������Ʈ�� ���ͼ� Bullet��ü�� ����
                if (bullet != null)                                 //bullet ������ ������Ʈ�� Bullet �����ʺz�� �����Ѵٸ�
                {
                    bullet.SetDirection(GetPlayerDirection());  //�ش� bullet�� ������ �÷��̾��� ���⿡ �°� ���� 
                }
                bulletObj.SetActive(true); //�ش� �Ѿ��� Ȱ��ȭ��Ŵ
            }
            else
            {
                Debug.Log("�Ѿ��� �غ�Ǿ� ���� �ʽ��ϴ�");
            }
        }
    }
}
