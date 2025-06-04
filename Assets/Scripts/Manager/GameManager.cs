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
        _lastPlayerPosition = new Vector2(-8.366f, 4.52f); //�ʱ⼼�ð�
        //����
        //MySceneManager.Instance.SetCurrentScene(new TitleScene());
    }

    private void Update()
    {
        //Debug.Log("���� �� : " + MySceneManager.Instance.GetCurrentSceneName());
        //���������� ������Ʈ�� �÷��̾��� ��ġ�� �����ϰ� LoadScene?
        //�ӽ÷� Title�� �� ���߿� inGame���� ������ ����
        if (Input.GetKeyDown(KeyCode.R) && MySceneManager.Instance.GetCurrentSceneName() == MyScene.InGame)
        {
            Debug.Log("�ΰ����� rŰ Ȱ��ȭ");
            MySceneManager.Instance.LoadScene("InGame");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            MySceneManager.Instance.LoadScene("InGame");
        }
    }
}
