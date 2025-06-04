using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//최상위 베이스 씬
public class BaseScene : MonoBehaviour
{
    public MyScene SceneType { get; protected set; } = MyScene.Unknown; // 디폴트로 Unknow 이라고 초기화
    
    //protected virtual void Init() { Debug.Log("Create Scene = " + SceneType.ToString()); }

    protected virtual void Init()
    {
        Debug.Log("Create Scene = " + SceneType.ToString());
        if (MySceneManager.Instance != null)
        {
            MySceneManager.Instance.SetCurrentScene(this); // 'this'는 씬에 있는 실제 BaseScene 컴포넌트를 참조합니다.
        }
    }
}
