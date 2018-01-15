using UnityEngine;
using UnityEngine.UI;

public class ButtonSetEventer : MonoBehaviour {

    Button button;
    [Header("移動するインデックス")]
    public int sceneNum = 1;
    [Header("シーン移動を使用するかどうか")]
    public bool isSceneMove = false;
    [Header("シーン移動の時にステートを渡すかどうか")]
    public bool isStateDispatcher = false;
    [Header("渡すステート番号")]
    public int stateNum = 0;

    void Start()
    {
        //シーン遷移のボタンイベントを設定
        button = GetComponent<Button>();
        GameController g = FindObjectOfType<GameController>();
        int num = sceneNum;
        if (isSceneMove) { button.onClick.AddListener(() => g.ChangeScene(num)); }
        GameState state = FindObjectOfType<GameState>();
        if (isStateDispatcher)
        {
            if(stateNum == 0) { button.onClick.AddListener(() => state.InitState()); }
            if(stateNum == 1) { button.onClick.AddListener(() => state.TutorialState()); }
            if(stateNum == 2) { button.onClick.AddListener(() => state.MainState()); }
        }
    }

    public void Tutorial(GameObject tutorialObj)
    {
        MissionAnimController MissionCon = GetComponent<MissionAnimController>();
        if(MissionCon != null)  button.onClick.AddListener(() => MissionCon.AppearOrder());
    }

}
