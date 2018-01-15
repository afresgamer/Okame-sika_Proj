using UnityEngine;
using UnityEngine.UI;

public class ScenarioController : MonoBehaviour {
    //タイトル
    [SerializeField, Header("タイトル(プランナーはいじらないで)")]
    private GameObject Title;
    //シナリオ背景
    [SerializeField,Header("シナリオ背景")]
    private GameObject ScenarioBackground;
    //スタートボタン
    [SerializeField, Header("スタートボタン(プランナーはいじらないで)")]
    private Button StartButton;
    //表示テキスト背景
    [SerializeField, Header("表示テキスト背景(プランナーはいじらないで)")]
    private Button ScenarioImage;
    //表示用テキスト
    [SerializeField, Header("表示用テキスト(プランナーはいじらないで)")]
    private Text Scenario;
    //シナリオデータ
    [SerializeField, Header("シナリオデータ(ここのデータをプランナーはいじれます)")]
    private ScenarioData sd;
    //文字の最大数
    const int Scenario_length = 15;
    //シナリオ現在番号
    private int ScenarioNum = 0;

    private void Awake()
    {
        ScenarioNum = 0;
        Scenario.text = sd.ScenarioS[ScenarioNum];
    }

    void Start()
    {
        Scenario.gameObject.SetActive(false);
        ScenarioImage.gameObject.SetActive(false);
        ScenarioBackground.SetActive(false);
    }

    private void Update()
    {
        //Debug.Log(ScenarioNum);
        if (GameState.instance.m_gameState == GameState._GameState.Title && ScenarioImage.gameObject.activeSelf)
        {
            SubMain();
        }
    }

    //背景用処理
    //public void Background()
    //{

    //}

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
        ScenarioBackground.SetActive(true);
    }

    public void Indicate_Scenario()
    {
        if (sd.ScenarioS.Length - 1 > ScenarioNum && sd.ScenarioS[ScenarioNum].Length <= Scenario_length)
        {  
            ScenarioNum++;
        }
        else if (ScenarioNum == sd.ScenarioS.Length - 1)
        {
            GameController.instance.ChangeScene(1);
            GameState.instance.m_gameState = GameState._GameState.Tutorial;
            ScenarioNum = 0;
        }
    }
    
}
