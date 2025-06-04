using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClear : MonoBehaviour
{
    public GameObject clearUI;
    public Button exitButton;

    //임시로 게임 끝낼려고 만든 스크립트
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
        Application.Quit(); // 어플리케이션 종료
#endif     
    }


}
