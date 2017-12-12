using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioController : MonoBehaviour {
    //タイトルロゴ
    [SerializeField, Header("タイトルロゴ")]
    private GameObject Title;
    //スタートボタン
    [SerializeField, Header("スタートボタン")]
    private Button StartButton;
    //表示テキスト背景
    [SerializeField, Header("表示テキスト背景")]
    private Button ScenarioImage;
    //表示用テキスト
    [SerializeField, Header("表示用テキスト")]
    private Text Scenario;
    //シナリオデータ
    [SerializeField, Header("シナリオデータ(ここのデータをプランナーはいじれます)")]
    private ScenarioData sd;
    //文字の最大数
    const int Scenario_length = 15;
    //テキスト生成フラグ
    private bool isFollow = false;
    //シナリオ現在番号
    private int ScenarioNum = 0;
    //シナリオ現在の文字番号
    private int NowScenarioLine = 0;
    //テキスト表示完了時間
    private string NowScenario = string.Empty;
    [SerializeField, Header("1文字の表示にかかる時間"),Range(0.1f, 0.3f)]
    float ScenarioFollowSpeed = 0.1f;  // 1文字の表示にかかる時間
    private float timeUntilDisplay = 0;     // 表示にかかる時間
    private float timeElapsed = 1;          // 文字列の表示を開始した時間
    private int lastUpdateCharacter = -1;		// 表示中の文字数

    void Start()
    {
        Scenario.gameObject.SetActive(false);
        ScenarioImage.gameObject.SetActive(false);
        SetNextLine();
    }

    private void Update()
    {
        if (GameState.instance.m_gameState == GameState._GameState.Title && ScenarioImage.gameObject.activeSelf)
        {
            //Main();
            SubMain();
        }
    }

    //public void Background()
    //{

    //}

    //一文字ずつ描画する処理
    private void Main()
    {
        if (NowScenarioLine < sd.ScenarioS[ScenarioNum].Length && isFollow)
        {
            SetNextLine();
            //Debug.Log("SCENARIO FOLLOW");
        }

        // クリックから経過した時間が想定表示時間の何%か確認し、表示文字数を出す
        int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - timeElapsed) / timeUntilDisplay) * NowScenario.Length);

        // 表示文字数が前回の表示文字数と異なるならテキストを更新する
        if (displayCharacterCount != lastUpdateCharacter)
        {
            Scenario.text = NowScenario.Substring(0, displayCharacterCount);
            lastUpdateCharacter = displayCharacterCount;
        }
    }

    //全文字表示
    private void SubMain()
    {
        Scenario.text = sd.ScenarioS[ScenarioNum];
    }

    public void StartScenario()
    {
        Title.SetActive(false);
        StartButton.gameObject.SetActive(false);
        Scenario.gameObject.SetActive(true);
        ScenarioImage.gameObject.SetActive(true);
    }

    public void Indicate_Scenario()
    {
        if (sd.ScenarioS[ScenarioNum].Length < Scenario_length && sd.ScenarioS.Length - 1 > ScenarioNum)
        {  
            ScenarioNum++;
        }
        else if (ScenarioNum == sd.ScenarioS.Length - 1)
        { GameController.instance.ChangeScene(1); GameState.instance.m_gameState = GameState._GameState.Tutorial; }
    }

    public void Display_Scenario()
    {
        if (sd.ScenarioS[ScenarioNum].Length < Scenario_length && sd.ScenarioS.Length - 1 > ScenarioNum)
        {
            ScenarioNum++;
            NowScenario = sd.ScenarioS[ScenarioNum];
            SetNextLine();
        }
        else if (ScenarioNum == sd.ScenarioS.Length - 1)
        { GameController.instance.ChangeScene(1); GameState.instance.m_gameState = GameState._GameState.Tutorial; }
    }

    public void SelectScenario()
    {
        if (sd.ScenarioS[ScenarioNum].Length < Scenario_length && sd.ScenarioS.Length > ScenarioNum)
        {
            NowScenario = sd.ScenarioS[ScenarioNum];
            SetNextLine();
        }
    }

    void SetNextLine()
    {
        // 想定表示時間と現在の時刻をキャッシュ
        timeUntilDisplay = NowScenario.Length * ScenarioFollowSpeed;
        timeElapsed = Time.time;

        // 文字カウントを初期化
        lastUpdateCharacter = -1;
    }
    
}
