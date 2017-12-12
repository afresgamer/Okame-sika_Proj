using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour {

    private Text text;
    //透明度用変数
    float alpha = 0;
    //透明度変化速度
    [SerializeField, Header("透明度変化速度")]
    private float A_Speed = 1;
    [SerializeField, Header("フェードオンでの待機時間")]
    private float FadeOnWaitTime = 1;
    //フェードインフラグ
    public bool isFadeIn = false;
    //フェードアウトフラグ
    public bool isFadeOut = false;
    //フェードオンフラグ
    public bool isFadeOn = false;

    private void Start()
    {
        text = GetComponent<Text>();
        setAlpha(alpha);
    }

    void Update () {
        //if (Input.GetMouseButtonDown(0)) { isFadeOn = true; }
        //フェードイン
        if (isFadeIn) {
            //Debug.Log("FADEIN");
            alpha -= A_Speed * Time.deltaTime;
            if(alpha <= 0) {
                alpha = 0;
                isFadeIn = false;
                if (isFadeOn) { StartCoroutine(FadeInAndOut()); }
            }
        }
        //フェードアウト
        if (isFadeOut) {
            //Debug.Log("FADEOUT");
            alpha += A_Speed * Time.deltaTime;
            if(alpha >= 1) { alpha = 1; isFadeOut = false; }
        }
        //フェードオン
        if (isFadeOn) {
            //Debug.Log("FADEON");
            isFadeIn = true;
        }
        setAlpha(alpha);
	}

    void setAlpha(float Alpha)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, Alpha);
    }

    public void FadeIn()
    {
        alpha = 1;
        isFadeIn = true;
    }

    public void FadeOut()
    {
        alpha = 0;
        isFadeOut = true;
    }

    public void FadeOn()
    {
        alpha = 1;
        isFadeOn = true;
        StartCoroutine(FadeInAndOut());
    }

    IEnumerator FadeInAndOut()
    {
        isFadeOn = false;
        yield return new WaitForSeconds(FadeOnWaitTime);
        isFadeOut = true;
    }

}
