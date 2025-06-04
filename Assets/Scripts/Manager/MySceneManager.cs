using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager Instance;
    public BaseScene _currentScene;
    //�� ���� ���Ŵ���
    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        Debug.Log("MySceneManager initialized.");
    }

    private void Init()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetCurrentScene(BaseScene scene)
    {
        _currentScene = scene;
        Debug.Log($"���Ŵ��� ���� �� ���� �Ϸ�: {_currentScene.SceneType}");
    }

    public MyScene GetCurrentSceneName()
    {
        return this._currentScene.SceneType;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Debug.Log(sceneName + "������ ���� �Ǿ����ϴ�");
    }
    public void LoadSceneAsync(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName.ToString());
        Debug.Log(sceneName + "������ ����Ǿ����ϴ�(Async)");
    }
}
