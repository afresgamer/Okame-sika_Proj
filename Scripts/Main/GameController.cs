using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    public static GameController instance;

    //カメラ移動フラグ
    [HideInInspector]
    public bool isCameraMove = false;
    //フラグの共通化(ここで移動と回転のオンオフをチェックする)
    [HideInInspector]
    public bool m_Move_Flag = false;
    //タッチ情報
    [HideInInspector]
    public TouchInfo info;
    //スピンフラグ
    [HideInInspector]
    public bool isSpin = true;
    //タイマー
    [HideInInspector]
    public float Timer = 32.00f;
    //マトリョーシカ生成数管理リスト
    [HideInInspector]
    public List<GameObject> Matryoshka_List = new List<GameObject>();
    //指令達成数
    [HideInInspector]
    public int ClearOrderCount = 0;

    private void Awake()
    {
        //シングルトン化
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }

    public void ChangeScene(int num)
    {
        SceneManager.LoadScene(num);
    }
}
