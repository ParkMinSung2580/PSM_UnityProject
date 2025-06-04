using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUI : MonoBehaviour
{
    private Dictionary<string, GameObject> gameObjectsdic;  //게임오브젝트 딕셔너리
    private Dictionary<string, Component> componentsdic;    //컴포넌트 딕셔너리 -> 게임 오브젝트의 이름_컴포넌트 이름 형태로 저장

    //게임 초반에 딕셔너리에 저장
    private void Awake()
    {
        //UI요소들은 Rect Transform 컴포넌트가 존재한다.       
        RectTransform[] transforms = GetComponentsInChildren<RectTransform>(true);
        gameObjectsdic = new Dictionary<string, GameObject>(transforms.Length * 4); //capacity 지정 -> 효율상승 dic 70~80 효율 떨어짐
        foreach(RectTransform child in transforms)  //순환반복
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
        return component as T; //as 캐스팅 -> 형변환
    }
}



    /// <summary>
    /// GetComponentsInChildren함수 
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
