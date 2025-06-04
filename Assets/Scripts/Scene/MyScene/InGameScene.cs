using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameScene : BaseScene
{
    public InGameScene() { SceneType = MyScene.InGame; }
    public GameObject player;

    void Awake()
    {
        Init();
        SetPlayerPosition();
    }
   

    private void SetPlayerPosition()
    {
        player = GameObject.Find("Player");
        player.transform.position = GameManager.Instance._lastPlayerPosition;         
    }
}
