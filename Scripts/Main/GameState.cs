using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

    public static GameState instance;
    //現在のゲーム状態
    public enum _GameState { Title, Tutorial = 1, Main, Result }
    [HideInInspector,Header(("現在のゲーム状態"))]
    public _GameState m_gameState = _GameState.Title;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }

    public void InitState()
    {
        m_gameState = _GameState.Title;
    }

    public void MainState()
    {
        m_gameState = _GameState.Main;
    }
    public void TutorialState()
    {
        m_gameState = _GameState.Tutorial;
    }

}
