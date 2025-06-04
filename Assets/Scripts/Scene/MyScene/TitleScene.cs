using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
    public TitleScene() { SceneType = MyScene.Title; }

    void Awake()
    {
        Init();
    }
}
