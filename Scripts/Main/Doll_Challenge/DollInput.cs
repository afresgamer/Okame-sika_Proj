using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DollInput : MonoBehaviour {
    
    private Vector3 BeforePos;
    private Vector3 AfterPos;
    //タップ回数
    private static int m_Tapcount = 0;
    //パッカ回数
    [Header("パッカ回数(プランナーはいじらないで)")]
    public Text PakkaCountText;
    private static int m_Score = 0;
    private static int m_PakkaCount = 0;
    [SerializeField, Header("最大タップ数")]
    private int Max_Tap_count = 1;
    //ぱっかする力
    [SerializeField, Header("ぱっかする力")]
    private float Pakka_Power = 10;
    //スピン回数
    [SerializeField, Header("最大回転数")]
    private int Max_Spin_count = 1;
    private static int m_SpinCount = 0;
    float m_power = 0;
    //タップ間の距離
    float distance;
    //タップする力
    [SerializeField, Header("タップする力")]
    private int Tap_Power = 5;
    //タッチオブジェクト格納用変数
    private GameObject m_target;
    //消えてから削除するまでの時間
    [SerializeField, Header("消えてから削除するまでの時間")]
    private float DestroyTime = 1.5f;
    
    private void Awake()
    {
        //初期化
        BeforePos = Vector3.zero;
        AfterPos = Vector3.zero;
        m_target = null;
        m_Tapcount = 0;
        m_PakkaCount = 0;
        m_SpinCount = 0;
        m_Score = 0;
    }

    //void Start () {
        
    //}
	
	void Update () {
        //タッチ情報を毎フレーム取得
        GameController.instance.info = AppUtil.GetTouch();
        if (GameController.instance.info == TouchInfo.Began)
        {
            distance = 0;
            BeforePos = AppUtil.GetTouchPosition();
            m_target = Obj_Check();
            if(m_target.tag != "Room") { GameController.instance.isCameraMove = false; }
        }
        else if (GameController.instance.info == TouchInfo.Ended)
        {
            AfterPos = AppUtil.GetTouchPosition();
            distance = Vector3.Distance(BeforePos, AfterPos);
            //スピン入力のためにコルーチンを使って入力終了を待つ
            if (m_power >= 1 && !GameController.instance.isSpin)
            { GameController.instance.isSpin = true; m_power = 0; }
            //Debug.Log(m_target.name);
            //分岐処理
            Matoryosika_CheckInput();
        }
        //1回転
        if (!GameController.instance.isSpin && distance > 100)
        { Spin(m_target,m_power); }
        
    }

    //オブジェクト取得用関数
    public GameObject Obj_Check()
    {
        GameObject objectName = null;
        Ray ray = Camera.main.ScreenPointToRay(AppUtil.GetTouchPosition());
        // Rayの当たったオブジェクトの情報を格納する
        RaycastHit hit = new RaycastHit();
        // オブジェクトにrayが当たった時
        if (Physics.Raycast(ray, out hit, 20))
        {
            // rayが当たったオブジェクトの名前を取得
            objectName = hit.collider.gameObject;
        }
        return objectName;
    }
    //ぱっかオブジェクトの判定処理
    public void SwitchObj(GameObject obj, float power)
    {
        //親オブジェクトだったら親セット
        if (obj.name.Contains("Matoryousika")) { if (power > 100) Pakka(obj, Pakka_Power); }
        //それ以外は親を指定してセット
        else { if (power > 100) Pakka(obj.transform.parent.gameObject, Pakka_Power); }
    }
    
    //１回転関数
    public void Spin(GameObject target, float power)
    {   
        if(target != null)
        {
            if (power < 1f)
            {
                m_power += Time.deltaTime;
                float prevT = power;
                power = Mathf.Min(1f, power + Time.deltaTime); // 360度を超えないように
                float dt = power - prevT;
                target.transform.RotateAround(target.transform.position, Vector3.up, -360 * dt);
            }
            if (target.GetComponentInChildren<YazirusiController>() != null)
            { Destroy(target.GetComponentInChildren<YazirusiController>().gameObject); }
        }
    }
    //ぱっか関数
    public void Pakka(GameObject target, float power)
    {
        //Debug.Log(target.transform.childCount);
        if(target != null)
        {
            if (target.transform.GetChild(0).GetChild(0).GetComponent<Rigidbody>() != null)
            {
                Rigidbody rb = target.transform.GetChild(0).GetChild(0).GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce(target.transform.GetChild(0).GetChild(0).up * 100 * power);
                    rb.useGravity = true;
                }
            }
            
            if (target.transform.childCount >= 2)
            {
                //透明化処理
                StartCoroutine(VisibleObj(target.transform.GetChild(1).GetChild(0).GetChild(1).gameObject));
                StartCoroutine(VisibleObj(target.transform.GetChild(1).GetChild(0).GetChild(0).gameObject));
                if (target.transform.childCount == 4)
                {
                    target.transform.GetChild(3).gameObject.SetActive(true);
                    target.transform.GetChild(3).parent = null;
                }
                if (target.transform.childCount == 5)
                {
                    target.transform.GetChild(4).gameObject.SetActive(true);
                    target.transform.GetChild(4).parent = null;
                }
            }
            
            m_PakkaCount++;
            if (GameState.instance.m_gameState == GameState._GameState.Main)
            { m_Score++; PakkaCountText.text = m_Score.ToString(); }
        }
    }
    //タップ関数
    public void Tapping(GameObject target, int _count)
    {
        m_Tapcount++;
        if (target != null)
        { target.transform.position += -target.transform.forward * Tap_Power * Time.deltaTime; }
        if(target.transform.childCount == 4)
        {
            Destroy(target.transform.GetChild(2).gameObject);
            Destroy(target.transform.GetChild(3).gameObject);
        }
        if (m_Tapcount > _count) {
            //ここでタップ上限に達したらリストから削除
            StartCoroutine(DestroyObj(target));
            m_Tapcount = 0;
        }
    }
    //オブジェクトによって分岐処理
    public void Matoryosika_CheckInput()
    {
        if (m_target != null)
        {
            GameController.instance.isCameraMove = false;//カメラ回転オフ
            switch (m_target.tag)
            {
                case "Okame-sika":
                    SwitchObj(m_target, distance);
                    break;
                case "Noroi-sika":
                    if (GameController.instance.isSpin)
                    {
                        GameController.instance.isSpin = false;
                        m_SpinCount++;
                        if (m_SpinCount >= Max_Spin_count)
                        {
                            StartCoroutine(DestroyObj(m_target));
                            m_SpinCount = 0;
                        }
                    }
                    break;
                case "Kabuki-sika":
                    Tapping(m_target, Max_Tap_count);
                    break;
                case "Room":
                    GameController.instance.isCameraMove = true;
                    break;
            }
        }
        else { GameController.instance.isCameraMove = true; }//カメラ回転オン
    }

    //消える処理
    IEnumerator VisibleObj(GameObject target)
    {
        if(target.GetComponent<FadeController>() != null)
        {
            target.GetComponent<FadeController>().FadeIn();
        } 
        yield return new WaitForSeconds(DestroyTime);
        target.GetComponentInParent<Collider>().isTrigger = true;
    }

    //入力したあとの破壊待機時間
    IEnumerator DestroyObj(GameObject _target)
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(_target);
        GameController.instance.RemoveMemory(_target);
    }

    //各種回数ゲッター
    public static int PakkaCount
    {
        get { return m_PakkaCount; }
        set { m_PakkaCount = value; }
    }
    public static int SpinCount
    {
        get { return m_SpinCount; }
        set { m_SpinCount = value; }
    }
    public static int TapCount
    {
        get { return m_Tapcount; }
        set { m_Tapcount = value; }
    }
    public static int GetScore
    {
        get { return m_Score; }
    }
}
