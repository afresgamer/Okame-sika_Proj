using UnityEngine;
using UnityEngine.UI;

public class DollInput : MonoBehaviour {
    
    private Vector3 BeforePos;
    private Vector3 AfterPos;
    //タップ回数
    private static int m_Tapcount = 0;
    //パッカ回数
    [Header("パッカ回数")]
    public Text PakkaCount;
    private static int m_Score = 0;
    private static int m_PakkaCount = 0;
    [SerializeField, Header("最大タップ数")]
    private int max_count = 3;
    //スピン回数
    private static int m_SpinCount = 0;
    float m_power = 0;
    //タップ間の距離
    float distance;
    //タップする力
    [SerializeField, Header("タップする力")]
    private int Tap_Power = 5;
    //タッチオブジェクト格納用変数
    private GameObject m_target;

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
            BeforePos = AppUtil.GetTouchPosition();
            m_target = Obj_Check();
            if(m_target.tag != "Room") { GameController.instance.isCameraMove = false; }
        }
        else if (GameController.instance.info == TouchInfo.Ended)
        {
            AfterPos = AppUtil.GetTouchPosition();
            distance = Vector3.Distance(BeforePos, AfterPos);
            if (m_power >= 1 && !GameController.instance.isSpin)
            { GameController.instance.isSpin = true; }
            //Debug.Log(m_target.tag);
            //分岐処理
            Matoryosika_CheckInput();
        }
        //1回転
        if (!GameController.instance.isSpin && distance > 100) { Spin(m_target, m_power); }
        
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
    //回転関数
    public void Spin(Rigidbody rb, float power)
    {
        if(rb != null)
        {
            Vector3 beforeRot = rb.transform.eulerAngles;
            Vector3 afterRot = new Vector3(rb.transform.eulerAngles.x
                , rb.transform.eulerAngles.y - 360, rb.transform.eulerAngles.z);

            rb.transform.eulerAngles = Vector3.Lerp(beforeRot, afterRot, Time.deltaTime * power * 0.05f);
        }
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
        }
    }
    //ぱっか関数
    public void Pakka(GameObject target, float power)
    {
        Rigidbody rb = target.GetComponent<Rigidbody>();
        if(rb != null)
        {
            rb.AddForce(target.transform.up * power * 2);
        }
        m_Score++;
        m_PakkaCount++;
        PakkaCount.text = m_Score.ToString();
    }
    //タップ関数
    public void Tapping(GameObject target, int _count)
    {
        m_Tapcount++;
        target.transform.position += -target.transform.forward * Tap_Power * Time.deltaTime;
        if (m_Tapcount > _count) {
            //ここでタップ上限に達したらリストから削除
            Destroy(target);
            //MatryoshkaController.Matryoshka_List.Remove(target);
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
                    Pakka(m_target, distance);
                    break;
                case "Noroi-sika":
                    if (GameController.instance.isSpin)
                    {
                        GameController.instance.isSpin = false;
                        m_SpinCount++;
                        //SpinCount.text = m_SpinCount.ToString();
                    }
                    break;
                case "Kabuki-sika":
                    Tapping(m_target, max_count);
                    break;
                case "Room":
                    GameController.instance.isCameraMove = true;
                    break;
            }
        }
        else { GameController.instance.isCameraMove = true; }//カメラ回転オン
    }
    //各種回数ゲッター
    public static int AddPakkaCount
    {
        get { return m_PakkaCount; }
        set { m_PakkaCount = value; }
    }
    public static int AddSpinCount
    {
        get { return m_SpinCount; }
        set { m_SpinCount = value; }
    }
    public static int AddTapCount
    {
        get { return m_Tapcount; }
        set { m_Tapcount = value; }
    }
    public static int GetScore
    {
        get { return m_Score; }
    }
}
