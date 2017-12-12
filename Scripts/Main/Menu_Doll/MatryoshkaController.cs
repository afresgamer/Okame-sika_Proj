using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatryoshkaController : MonoBehaviour {

    [SerializeField, Header("XXX－シカの種類(通常サイズ)")]
    private GameObject[] BigMatryoshkaS;
    [SerializeField, Header("XXX－シカの種類(中くらいサイズ)")]
    private GameObject[] MiddleMatryoshkaS;
    [SerializeField, Header("XXX－シカの種類(小さいサイズ)")]
    private GameObject[] SmallMatryoshkaS;
    //オカメーシカの配列インデックス
    const int matoryo_Num = 0;
    //ノロイーシカの配列インデックス
    const int Noroi_Num = 1;
    //縦と横の長さ
    [SerializeField, Header("縦の長さ")]
    private float width = 4;
    [SerializeField, Header("横の長さ")]
    private float height = 4;
    //XXX－シカの生成最大数
    [SerializeField, Header("XXX－シカの生成最大数")]
    private float Max_Create_Matryoshka_count = 6;

    //和田加筆
    public static int pakka;
    private int rand;
    DollInput dollInp;
    private Collider col;
    private GameObject m_target;
    //private Matryoshka.DollInput_Type m_dollInput_Type;
    //終

    //位置決定関数
    public Vector3 MatryoshkaPos()
    {
        float random_w = Random.Range(-width, width);
        float random_h = Random.Range(-height, height);
        Vector3 max_w_pos = new Vector3(width, 0, random_h);//幅が一定パターン(最高)
        Vector3 min_w_pos = new Vector3(-width, 0, random_h);//幅が一定パターン(最低)
        Vector3 max_h_pos = new Vector3(random_w, 0, height);//高さが一定パターン(最高)
        Vector3 min_h_pos = new Vector3(random_w, 0, -height);//高さが一定パターン(最低)
        Vector3[] random_pos = { max_w_pos, min_w_pos, max_h_pos, min_h_pos };
        int randon_num = Random.Range(0, random_pos.Length);
        return random_pos[randon_num];
    }

    //XXX-シカの選択関数
    public GameObject CreateNoroi()
    {
        int random_Matryoshka = Random.Range(Noroi_Num, BigMatryoshkaS.Length);
        return BigMatryoshkaS[random_Matryoshka];
    }

    //おかめーしか呼び出し関数
    public GameObject CreateOkame()
    {
        return BigMatryoshkaS[matoryo_Num];
    }
    
    void Start () {
        //移動フラグ起動
        GameController.instance.m_Move_Flag = true;

        // 和田加筆
        dollInp = FindObjectOfType<DollInput>();
        m_target = null;
        //m_dollInput_Type = Matryoshka.DollInput_Type.None;
        // 終
    }

    void Update () {
        if(GameState.instance.m_gameState == GameState._GameState.Main)
        {
            //Debug.Log(GameController.instance.Matryoshka_List.Count);
            //指定数よりも少なくなったら生成
            if (GameController.instance.Matryoshka_List.Count < Max_Create_Matryoshka_count)
            {
                CreateOkame_sika();
            }

            // 和田加筆
            GameController.instance.info = AppUtil.GetTouch();
            pakka = DollInput.AddPakkaCount;
            if (GameController.instance.info == TouchInfo.Began)
            {
                System.Random r = new System.Random();
                rand = r.Next(1, 6);
                m_target = dollInp.Obj_Check();
                col = m_target.GetComponent<Collider>();
            }
            else if (GameController.instance.info == TouchInfo.Ended)
            {
                if (m_target.tag == "Okame-sika")
                {
                    DollVariation();
                    if (pakka != 0) { col.isTrigger = true; }
                }
            }
            // 終
        }
    }

    // 和田加筆
    public void DollVariation()
    {
        if (pakka % 3 != 0)
        {
            if (rand == 1)
            {
                GameObject Noroi = Instantiate(CreateNoroi(), m_target.transform.position, Quaternion.identity);
                GameController.instance.Matryoshka_List.Add(Noroi.gameObject);
            }
            else
            {
                GameObject Okame = Instantiate(CreateOkame(), m_target.transform.position, Quaternion.identity);
                GameController.instance.Matryoshka_List.Add(Okame.gameObject);
            }
        }
    }
    // 終

    /// <summary>
    /// ボタンイベント用関数　↓
    /// </summary>

    //XXX-シカの生成関数
    public void MatryoshkaCreate()
    {
        GameObject matoryo_sika = Instantiate(CreateNoroi(), MatryoshkaPos(), Quaternion.identity);
        GameController.instance.Matryoshka_List.Add(matoryo_sika);
    }

    //おかめーしか生成関数
    public void CreateOkame_sika()
    {
        GameObject okame_sika = Instantiate(CreateOkame(), MatryoshkaPos(), Quaternion.identity);
        okame_sika.name = "Okame_sika";
        GameController.instance.Matryoshka_List.Add(okame_sika);
    }

    //全ノロイーシカ削除と生成関数(オカメモード)
    public void DestroyMatryoshka(Image ButtonLife)
    {
        if(ButtonLife.fillAmount >= 1)
        {
            XXX_sika[] Noroi_sikaS = FindObjectsOfType<XXX_sika>();
            if (Noroi_sikaS.Length > 0)
            {
                for (int i = 0; i < Noroi_sikaS.Length; i++)
                {
                    Matryoshka.DollInput_Type dollInput_Type = Noroi_sikaS[i].matryoshka.Get_dollInput_Type;
                    if(dollInput_Type == Matryoshka.DollInput_Type.Spin || dollInput_Type == Matryoshka.DollInput_Type.Tap)
                    {
                        Destroy(Noroi_sikaS[i].gameObject);
                        GameController.instance.Matryoshka_List.Remove(Noroi_sikaS[i].gameObject);
                        CreateOkame_sika();
                    }
                }
            }
        }
    }
}
