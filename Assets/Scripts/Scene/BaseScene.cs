using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ֻ��� ���̽� ��
public class BaseScene : MonoBehaviour
{
    public MyScene SceneType { get; protected set; } = MyScene.Unknown; // ����Ʈ�� Unknow �̶�� �ʱ�ȭ
    
    //protected virtual void Init() { Debug.Log("Create Scene = " + SceneType.ToString()); }

    protected virtual void Init()
    {
        Debug.Log("Create Scene = " + SceneType.ToString());
        if (MySceneManager.Instance != null)
        {
            MySceneManager.Instance.SetCurrentScene(this); // 'this'�� ���� �ִ� ���� BaseScene ������Ʈ�� �����մϴ�.
        }
    }
}
