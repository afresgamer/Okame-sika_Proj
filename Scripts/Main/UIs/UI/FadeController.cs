using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour {

    /// <summary>
    /// フェード処理用　管理スクリプト
    /// 基本的にはUIのテキストとイメージとレンダラーにフェード処理できるようにしています。
    /// </summary>

    private GameObject FadeObj;
    //透明度用変数
    float alpha = 1;
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
        alpha = 1;
        FadeObj = gameObject;
        //goto テキストとイメージとモデル用に初期化処理を作る
        if (FadeObj.GetComponent<Text>() != null) SetAlpha(FadeObj.GetComponent<Text>(), alpha);
        if (FadeObj.GetComponent<Image>() != null) SetAlpha(FadeObj.GetComponent<Image>(), alpha);
        if (FadeObj.GetComponent<Renderer>() != null) SetAlpha(FadeObj.GetComponent<Renderer>(), alpha);
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
        //テキストとイメージとモデル用に更新処理を作る　
        if (FadeObj.GetComponent<Text>() != null) SetAlpha(FadeObj.GetComponent<Text>(), alpha);
        if (FadeObj.GetComponent<Image>() != null) SetAlpha(FadeObj.GetComponent<Image>(), alpha);
        if (FadeObj.GetComponent<Renderer>() != null) SetAlpha(FadeObj.GetComponent<Renderer>(), alpha);
	}

    //テキスト用
    void SetAlpha(Text _text ,float Alpha)
    {
        _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, Alpha);
    }

    //画像用
    void SetAlpha(Image _image, float Alpha)
    {
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, Alpha);
    }

    //レンダラー用
    void SetAlpha(Renderer _renderer, float Alpha)
    {
        _renderer.material.color = new Color(_renderer.material.color.r
            , _renderer.material.color.g, _renderer.material.color.b, Alpha);
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
