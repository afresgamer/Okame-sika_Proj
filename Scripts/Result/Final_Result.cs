using UnityEngine;
using UnityEngine.UI;

/*
最終的な指令の達成回数も表示
シーン遷移のためのボタンも表示（タイトル・メイン両方）
*/
public class Final_Result : MonoBehaviour {

    [SerializeField,Header("指令達成数のテキスト")]
    Text OrderText;
    [SerializeField, Header("おかめーしか獲得数のテキスト")]
    Text Okame_sika_Text;
    [SerializeField, Header("ゲームクリア・オーバーのテキスト")]
    Text ResultText;

    private int over = 0;

    void Start () {
        OrderText.text = GameController.instance.ClearOrderCount.ToString();
        Okame_sika_Text.text = DollInput.GetScore.ToString();

        over = PlayerPrefs.GetInt("over");
	}

    private void Update()
    {
        PlayerPrefs.DeleteKey("over");

        if (over == 10)
        {
            ResultText.text = "ゲームオーバー…";
        }
        else
        {
            ResultText.text = "ゲームクリアー！";
        }
    }
}
