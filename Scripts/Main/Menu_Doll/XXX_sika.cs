using UnityEngine;
using System.Collections;

public class XXX_sika : MonoBehaviour {

    //プレイヤー情報
    private GameObject m_player;
    //Animator m_anim;
    [Header("XXXーシカのパラメータ")]
    public Matryoshka matryoshka;
    [Header("拡大スピード")]
    public float m_ScaleSpeed = 1.0f;
    AudioSource XXX_SIKA_SE;
    [SerializeField, Header("声の間隔")]
    float random_time = 5;

    void Start () {
        XXX_SIKA_SE = GetComponent<AudioSource>();
        //ターゲットの取得
        m_player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(ScaleAnim(gameObject, transform.localScale.x, 1.5f));
	}
	
	void Update () {
        if(GameState.instance.m_gameState == GameState._GameState.Main)
        {
            random_time -= Time.deltaTime;
            if (random_time <= 0)//声ランダム再生
            {
                random_time = Random.Range(3, random_time);
                SoundManager.instance.PlaySE(XXX_SIKA_SE, 4, false);
            }
        }
        //Debug.Log(Matryoshka.m_Move_Flag);
        if (GameController.instance.m_Move_Flag)
        {
            Matryoshka.Move(gameObject, transform.position, m_player.transform.position, matryoshka.m_MoveSpeed);
            SoundManager.instance.PlaySE(XXX_SIKA_SE, 1, true);

        }
        if (GameController.instance.isSpin)//プレイヤーの方向に向く
        {
            Matryoshka.SetRot(gameObject, m_player.transform.position - transform.position - (Camera.main.transform.position / 2.5f)
                , matryoshka.m_RotSpeed);
        }
	}

    //拡大関数 goto 拡大にバグあり　拡大するとおかめーしかの中サイズと小サイズは浮く
    public IEnumerator ScaleAnim(GameObject target, float size, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        float scale = Mathf.Lerp(size, 1, Time.time * m_ScaleSpeed);
        target.transform.localScale = new Vector3(scale, scale, scale);
        yield return new WaitForSeconds(waitTime);
        target.GetComponent<Rigidbody>().useGravity = true;
    }

    //オカメーシカ用
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            GameController.instance.Matryoshka_List.Remove(gameObject);
            GameController.instance.Timer -= 1;
            //ダメージを受けた効果音
            SoundManager.instance.PlaySE(XXX_SIKA_SE, 2, false);
        }
    }

    //ライン用
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Line")//ライン表示
        {
            other.GetComponent<MeshRenderer>().enabled = true;
            //ラインに触れているときの音
            SoundManager.instance.PlaySE(other.GetComponent<AudioSource>(), 1, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Line")
        {
            other.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
