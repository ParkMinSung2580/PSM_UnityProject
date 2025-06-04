using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _distance = 10f;
    private Vector2 _fireDir = Vector2.right;

    private Vector2 _startPosition;

    private void OnEnable() //Bullet 객체가 활성화 될 때
    {
        _startPosition = transform.position; // 활성화될 때마다 시작 위치 저장
        //transform.SetParent(null); // 플레이어 움직임에 영향받지 않도록
    }

    private void Update()
    {
        //한쪽만 나간다 플레이어의 FlipX를 통한 방향을 알아와서 오른쪽 왼쪽 구분하여 나가게 해주기
        Shot(_fireDir);

        // 화면 밖으로 나가면 비활성화
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

    // 외부에서 방향을 설정하는 메서드
    public void SetDirection(Vector2 direction)
    {
        _fireDir = direction;
    }

}
