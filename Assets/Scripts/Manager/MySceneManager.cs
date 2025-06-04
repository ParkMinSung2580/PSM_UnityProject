using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager Instance;
    public BaseScene _currentScene;
    //내 게임 씬매니저
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
        Debug.Log($"씬매니저 현재 씬 설정 완료: {_currentScene.SceneType}");
    }

    public MyScene GetCurrentSceneName()
    {
        return this._currentScene.SceneType;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Debug.Log(sceneName + "씬으로 변경 되었습니다");
    }
    public void LoadSceneAsync(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName.ToString());
        Debug.Log(sceneName + "씬으로 변경되었습니다(Async)");
    }
}
