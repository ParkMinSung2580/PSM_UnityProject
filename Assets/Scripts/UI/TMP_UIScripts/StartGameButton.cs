using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{
    public Button startGameButton;

    void Start()
    {
        startGameButton.onClick.AddListener(OnStartGameButtonClicked);
    }

    void OnStartGameButtonClicked()
    {
        MySceneManager.Instance.LoadScene("InGame");
    }
}
