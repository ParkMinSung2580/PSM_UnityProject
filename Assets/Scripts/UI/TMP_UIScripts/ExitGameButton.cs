using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitGameButton : MonoBehaviour
{
    public Button exitGameButton;

    void Start()
    {
        exitGameButton.onClick.AddListener(OnExitGameButtonClicked);
    }

    void OnExitGameButtonClicked()
    {
        Debug.Log(exitGameButton);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif      
    }
}
