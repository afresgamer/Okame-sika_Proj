using UnityEngine;

public class MissionAnimController : MonoBehaviour {

    //アニメーションの条件ハッシュ値
    private int anim_Hash = Animator.StringToHash("Order");
    Animator anim;
    int count = 0;
	
	void Start () {
        anim = GetComponent<Animator>();
	}
	
    //ボタン用アニメーション
    public void AppearOrder()
    {
        count++;
        if(count > 1) { count = 0; }
        anim.SetBool(anim_Hash, count > 0);
    }

    //指令アニメーション
    public void AppearOrderAnim()
    {
        anim.SetBool(anim_Hash, true);
        AnimatorStateInfo animStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if(animStateInfo.normalizedTime > 1)
        {
            anim.SetBool(anim_Hash, false);
        }
    }
}
