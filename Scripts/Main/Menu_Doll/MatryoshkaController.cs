using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatryoshkaController : MonoBehaviour {

    [SerializeField, Header("XXX－シカの種類")]
    private GameObject MatryoshkaS;
    //縦と横の長さ
    [SerializeField, Header("縦の長さ")]
    private float width = 4;
    [SerializeField, Header("横の長さ")]
    private float height = 4;
    //XXX－シカの生成最大数
    [SerializeField, Header("XXX－シカの生成最大数")]
    private float Max_Create_Matryoshka_count = 6;

    //位置決定関数
    public Vector3 MatryoshkaPos()
    {
        float random_w2 = Random.Range(1, width);//暖炉避け処理
        float random_w = Random.Range(-width, width);
        float random_h = Random.Range(-height, height);
        Vector3 max_w_pos = new Vector3(width, 0, random_h);//幅が一定パターン(最高)
        Vector3 min_w_pos = new Vector3(-width, 0, random_h);//幅が一定パターン(最低)
        Vector3 max_h_pos = new Vector3(random_w, 0, height);//高さが一定パターン(最高)
        Vector3 min_h_pos = new Vector3(random_w2, 0, -height);//高さが一定パターン(最低)
        Vector3[] random_pos = { max_w_pos, min_w_pos, max_h_pos, min_h_pos };
        int randon_num = Random.Range(0, random_pos.Length);
        return random_pos[randon_num];
    }
    
    void Start () {
        for(int i = 0; i < Max_Create_Matryoshka_count; i++)
        {
            CreateOkame_sika();
        }
    }
    void Update () {
        //Debug.Log(GameController.instance.Matryoshka_List.Count);
        if(GameState.instance.m_gameState == GameState._GameState.Main)
        {
            //移動フラグ起動
            GameController.instance.m_Move_Flag = true;
            //指定数よりも少なくなったら生成
            if (GameController.instance.Matryoshka_List.Count < Max_Create_Matryoshka_count)
            {
                CreateOkame_sika();
            }
        }
    }

    //おかめーしか生成関数
    public void CreateOkame_sika()
    {
        GameObject okame_sika = Instantiate(MatryoshkaS, MatryoshkaPos(), Quaternion.identity);
        okame_sika.name = MatryoshkaS.name;
        GameController.instance.Matryoshka_List.Add(okame_sika);
    }

    /// <summary>
    /// ボタンイベント用関数　↓
    /// </summary>
    
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
                    Matryoshka.DollInput_Type dollInput_Type = Noroi_sikaS[i].matryoshka.dollInput_Type;
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
