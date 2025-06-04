using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClear : MonoBehaviour
{
    public GameObject clearUI;
    public Button exitButton;

    //�ӽ÷� ���� �������� ���� ��ũ��Ʈ
    private void Awake()
    {
        exitButton.onClick.AddListener(Clear);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            clearUI.SetActive(true);
        }
    }  

    public void Clear()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // ���ø����̼� ����
#endif     
    }


}
