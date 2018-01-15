using UnityEngine;
using System.Collections;

public class MissionAnimController : MonoBehaviour {

    //アニメーションの条件ハッシュ値
    private int anim_Hash = Animator.StringToHash("Order");
    Animator m_anim;
    int count = 0;
    [SerializeField, Header("アニメーションが戻るときの待機時間")]
    private float Back_AnimTime = 3;

    void Start()
    {
        m_anim = GetComponent<Animator>();
    }

    //ボタン用アニメーション
    public void AppearOrder()
    {
        count++;
        if(count > 1) { count = 0; }
        m_anim.SetBool(anim_Hash, count > 0);
    }

    //指令アニメーション
    public void AppearOrderAnim(Animator anim)
    {
        anim.SetBool(anim_Hash, true);
        StartCoroutine(BackOrderAnim());
    }

    //指令戻すアニメーション処理
    IEnumerator BackOrderAnim()
    {
        yield return new WaitForSeconds(Back_AnimTime);
        m_anim.SetBool(anim_Hash, false);
    }
}
