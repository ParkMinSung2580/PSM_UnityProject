using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUI : MonoBehaviour
{
    private Dictionary<string, GameObject> gameObjectsdic;  //���ӿ�����Ʈ ��ųʸ�
    private Dictionary<string, Component> componentsdic;    //������Ʈ ��ųʸ� -> ���� ������Ʈ�� �̸�_������Ʈ �̸� ���·� ����

    //���� �ʹݿ� ��ųʸ��� ����
    private void Awake()
    {
        //UI��ҵ��� Rect Transform ������Ʈ�� �����Ѵ�.       
        RectTransform[] transforms = GetComponentsInChildren<RectTransform>(true);
        gameObjectsdic = new Dictionary<string, GameObject>(transforms.Length * 4); //capacity ���� -> ȿ����� dic 70~80 ȿ�� ������
        foreach(RectTransform child in transforms)  //��ȯ�ݺ�
        {
            gameObjectsdic.TryAdd(child.gameObject.name, child.gameObject);
        }

        Component[] components = GetComponentsInChildren<Component>(true);
        componentsdic = new Dictionary<string, Component>(components.Length * 4);
        foreach(Component child in components) 
        {
            componentsdic.TryAdd($"{child.gameObject.name}_{child.name}", child);
        }
    }

    public GameObject GetUI(in string name)
    {
        gameObjectsdic.TryGetValue(name, out GameObject gameObject);
        return gameObject;
    }

    public T GetUI<T>(in string name) where T : Component
    {
        componentsdic.TryGetValue($"{name}_{typeof(T).Name}", out Component component);
        return component as T; //as ĳ���� -> ����ȯ
    }
}



    /// <summary>
    /// GetComponentsInChildren�Լ� 
    /// 
    /// </summary>
    /*public T[] GetComponentsInChildren<T>(bool includeInactive)
    {
        return gameObject.GetComponentsInChildren<T>(includeInactive);
    }

    public void GetComponentsInChildren<T>(bool includeInactive, List<T> result)
    {
        gameObject.GetComponentsInChildren(includeInactive, result);
    }

    public T[] GetComponentsInChildren<T>()
    {
        return GetComponentsInChildren<T>(includeInactive: false);
    }

    public void GetComponentsInChildren<T>(List<T> results)
    {
        GetComponentsInChildren(includeInactive: false, results);
    }*/
