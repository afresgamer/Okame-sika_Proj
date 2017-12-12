using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XXX_sika : MonoBehaviour {

    //プレイヤー情報
    private GameObject m_player;
    Rigidbody m_rb;
    //Animator m_anim;
    [Header("XXXーシカのパラメータ")]
    public Matryoshka matryoshka;
    [Header("拡大スピード")]
    public float m_ScaleSpeed = 1.0f;

    void Start () {
        //ターゲットの取得
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_rb = GetComponent<Rigidbody>();
        if(matryoshka.dollInput_Type == Matryoshka.DollInput_Type.Pakka)
        {
            //重力起動
            m_rb.useGravity = true;
        }
        ScaleAnim(gameObject, matryoshka.m_MatryoshkaScale);
	}
	
	void Update () {
        //Debug.Log(Matryoshka.m_Move_Flag);
        if (GameController.instance.m_Move_Flag)
        {
            Matryoshka.Move(m_rb, transform.position, m_player.transform.position, matryoshka.m_MoveSpeed);
        }
        if (GameController.instance.isSpin)//プレイヤーの方向に向く
        {
            Matryoshka.SetRot(gameObject,
                m_player.transform.position - transform.position - (Camera.main.transform.position / 2.5f), 
                matryoshka.m_RotSpeed);
        }
        //スケールの長さが3(スケールを全部足すと3だから)よりも小さい場合スケールアニメーション
        //if(transform.lossyScale.magnitude <= 3) { ScaleAnim(gameObject,matryoshka.m_MatryoshkaScale); }
	}

    //拡大関数
    public void ScaleAnim(GameObject target, float size)
    {
        float scale = Mathf.Lerp(size, 1, Time.time * m_ScaleSpeed);
        target.transform.localScale = new Vector3(scale, scale, scale); 
    }

    //オカメーシカ用
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            GameController.instance.Matryoshka_List.Remove(gameObject);
            GameController.instance.Timer -= 1;
        }
    }

    //ノロイーシカ用
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            GameController.instance.Matryoshka_List.Remove(gameObject);
            GameController.instance.Timer -= 1;
        }
        if (other.gameObject.tag == "Line")//ライン表示
        {
            other.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Line")
        {
            other.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
