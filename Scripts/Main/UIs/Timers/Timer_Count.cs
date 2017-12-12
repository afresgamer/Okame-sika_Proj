using UnityEngine;
using UnityEngine.UI;

/*
タイマーの表示。00:00から08:00へのカウント方式（デジタル時計）
1秒ごとに15分進み、合計で32秒かけて08:00になる
*/
public class Timer_Count : MonoBehaviour {

    Text TimerText;
    [SerializeField, Header("時間制限(ここ変えると制限時間が変わります)")]
    private float MaxTimerCount = 32.0f;
    [SerializeField, Header("タイマーのスピード")]
    private float Timer_Count_Speed = 1;

    private void Awake()
    {
        //ここで制限時間セット
        GameController.instance.Timer = MaxTimerCount;
    }

    void Start () {
        TimerText = GetComponent<Text>();
        string _timer = GameController.instance.Timer.ToString("f2");
        TimerText.text = _timer.Replace(".", ":");
    }
	
	void Update () {
        if (GameState.instance.m_gameState == GameState._GameState.Main)
        {
            GameController.instance.Timer -= 0.1f * Time.deltaTime * Timer_Count_Speed;
            if (GameController.instance.Timer <= 0)
            {
                GameController.instance.Timer = 00.00f;
                GameController.instance.m_Move_Flag = false;
                GameController.instance.ChangeScene(2);
                GameState.instance.m_gameState = GameState._GameState.Result;
                GameController.instance.Matryoshka_List.Clear();
            }
            string _timer = GameController.instance.Timer.ToString("f2");
            string now_timer = _timer.Replace(".", ":");

            TimerText.text = now_timer;
        }
        
    }
}
