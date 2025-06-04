using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject player;
    public Vector2 _lastPlayerPosition; 
    
    //게임매니저 싱글톤 생성
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //시작
    private void Start()
    {
        _lastPlayerPosition = new Vector2(-8.366f, 4.52f); //초기세팅값
        //시작
        //MySceneManager.Instance.SetCurrentScene(new TitleScene());
    }

    private void Update()
    {
        //Debug.Log("현재 씬 : " + MySceneManager.Instance.GetCurrentSceneName());
        //마지막으로 업데이트된 플레이어의 위치로 변경하고 LoadScene?
        //임시로 Title로 비교 나중에 inGame으로 변경후 구현
        if (Input.GetKeyDown(KeyCode.R) && MySceneManager.Instance.GetCurrentSceneName() == MyScene.InGame)
        {
            Debug.Log("인게임중 r키 활성화");
            MySceneManager.Instance.LoadScene("InGame");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            MySceneManager.Instance.LoadScene("InGame");
        }
    }
}
