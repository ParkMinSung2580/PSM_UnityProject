using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject player;
    public Vector2 _lastPlayerPosition;
    
    //���ӸŴ��� �̱��� ����
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

    //����
    private void Start()
    {
        //����
        //MySceneManager.Instance.SetCurrentScene(new TitleScene());
    }

    private void Update()
    {
        Debug.Log("���� �� : " + MySceneManager.Instance.GetCurrentSceneName());
        //���������� ������Ʈ�� �÷��̾��� ��ġ�� �����ϰ� LoadScene?
        //�ӽ÷� Title�� �� ���߿� inGame���� ������ ����
        if (Input.GetKeyDown(KeyCode.R) && MySceneManager.Instance.GetCurrentSceneName() == MyScene.InGame)
        {
            Debug.Log("�ΰ����� rŰ Ȱ��ȭ");
            player = GameObject.Find("Player");
            player.transform.position = _lastPlayerPosition;
            
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            MySceneManager.Instance.LoadScene("InGame");
        }
    }
}
