using UnityEngine;
using UnityEngine.UI;

public class Okame_Gauge : MonoBehaviour {

    [SerializeField, Header("おかめーしかボタン")]
    private Image Okame_sika_UI;
    [SerializeField, Header("おかめーしかボタンの回復スピード")]
    private float Timer_Speed = 1;
    //現在の回復開始時間
    private float Recovery_time = 0;

    void Start () {
        //ボタンゲージの初期化
        Okame_sika_UI.fillAmount = 0;
	}
	
	void Update () {
        if(GameState.instance.m_gameState == GameState._GameState.Main)
        {
            if (Okame_sika_UI.fillAmount <= 1)
            {
                Recovery_time += Time.deltaTime / 10 * Timer_Speed;
            }

            Okame_sika_UI.fillAmount = Recovery_time;
        }
    }

    public void AddGauge()
    {
        if(Okame_sika_UI.fillAmount >= 1) { Recovery_time = 0; }
    } 
}
