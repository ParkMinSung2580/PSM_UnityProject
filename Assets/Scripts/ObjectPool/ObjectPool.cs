using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ObjectPool
{
    private GameObject[] _pool;

    //������
    public ObjectPool(int size,GameObject target,GameObject parent)
    {
        CreatePool(size,target,parent);
    }

    //���ϴ� ��ġ�� ������ ������Ʈ Ǯ�� ������
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

    //��Ȱ��ȭ�� ��� ������ ��ü ã��
    public GameObject GetInactive()
    {
        for (int i = 0; i < _pool.Length; i++)
        {
            if (!_pool[i].activeSelf) // ��Ȱ��ȭ�� ��ü ã��
            {
                return _pool[i]; // ã���� �ش� ��ü ��ȯ
            }
        }
        return null; // ��� ������ ��ü�� ������ null ��ȯ
    }

    //�迭�� ��ȸ�Ͽ� ���� destroy��Ű���Լ�
    public void DestroyAll()
    {
        for(int i = 0; i < _pool.Length; i++)
        {
            MonoBehaviour.Destroy(_pool[i]);
        }
        _pool = null;
        //������Ʈ Ǯ �ʱ�ȭ
    }


}
