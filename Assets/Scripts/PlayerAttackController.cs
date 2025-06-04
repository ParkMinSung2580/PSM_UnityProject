using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _poolSize;
    [SerializeField] private PlayerController1 playerController; // 플레이어 컨트롤러 참조

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
        if(Input.GetKeyDown(KeyCode.X)) //키를 입력하면 공격실행
        {
            GameObject bulletObj = _objectPool.GetInactive();   //놀고있는 오브젝트 들고오기
            if (bulletObj != null)  //만약 들고 왔다면 
            {
                bulletObj.transform.position = transform.position; // 해당 들고온 오브젝트를 발사 위치 설정
                Bullet bullet = bulletObj.GetComponent<Bullet>();  //해당 오브젝트가 Bullet이라는 컴포넌트를 들고와서 Bullet객체에 담음
                if (bullet != null)                                 //bullet 변수에 오브젝트가 Bullet 컴포너틑가 존재한다면
                {
                    bullet.SetDirection(GetPlayerDirection());  //해당 bullet의 방향을 플레이어의 방향에 맞게 설정 
                }
                bulletObj.SetActive(true); //해당 총알을 활성화시킴
            }
            else
            {
                Debug.Log("총알이 준비되어 있지 않습니다");
            }
        }
    }
}
