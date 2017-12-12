using UnityEngine;
using UnityEngine.UI;

public class ButtonSetEventer : MonoBehaviour {

    Button button;
    [Header("移動するインデックス")]
    public int sceneNum = 1;

	void Start () {
        //シーン遷移のボタンイベントを設定
        button = GetComponent<Button>();
        GameController g = FindObjectOfType<GameController>();
        int num = sceneNum;
        button.onClick.AddListener(() => g.ChangeScene(num));
	}
	
}
