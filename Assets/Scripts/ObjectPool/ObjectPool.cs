using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ObjectPool
{
    private GameObject[] _pool;

    //생성자
    public ObjectPool(int size,GameObject target,GameObject parent)
    {
        CreatePool(size,target,parent);
    }

    //원하는 위치에 생성할 오브젝트 풀링 생성자
    public ObjectPool(int size, GameObject target, Vector2 SpawnPosition)
    {
        CreatePool(size, target, SpawnPosition);
    }

    public void CreatePool(int size,GameObject target,GameObject parent)
    {
        _pool = new GameObject[size];

        for(int i = 0; i < size; i++)
        {
            GameObject obj = MonoBehaviour.Instantiate(target,parent.transform);
            _pool[i] = obj;
            _pool[i].SetActive(false);
        }
    }

    public void CreatePool(int size, GameObject target, Vector2 SpawnPosition)
    {
        _pool = new GameObject[size];

        for (int i = 0; i < size; i++)
        {
            GameObject obj = MonoBehaviour.Instantiate(target,SpawnPosition,Quaternion.identity);
            _pool[i] = obj;
            _pool[i].SetActive(false);
        }
    }

    public void Activate(bool flag)
    {
        for (int i = 0; i < _pool.Length; i++)
        {
            if (_pool[i].activeSelf != flag)
            {
                _pool[i].SetActive(flag);
                return;
            }
        }
    }

    //비활성화된 사용 가능한 객체 찾기
    public GameObject GetInactive()
    {
        for (int i = 0; i < _pool.Length; i++)
        {
            if (!_pool[i].activeSelf) // 비활성화된 객체 찾기
            {
                return _pool[i]; // 찾으면 해당 객체 반환
            }
        }
        return null; // 사용 가능한 객체가 없으면 null 반환
    }

    //배열을 순회하여 전부 destroy시키는함수
    public void DestroyAll()
    {
        for(int i = 0; i < _pool.Length; i++)
        {
            MonoBehaviour.Destroy(_pool[i]);
        }
        _pool = null;
        //오브젝트 풀 초기화
    }


}
