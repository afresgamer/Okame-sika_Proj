using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_Controller : MonoBehaviour,IDollInputTypeInterface
{
    [Header("最低限の順番づけのみ。今後細部を修正予定")]

    [SerializeField, Header("チュートリアル用指令アニメーション(プランナーはいじらないで)")]
    private MissionAnimController m_MissionAnimCon;
    [SerializeField, Header("チュートリアル用指令")]
    private Order[] m_OrderS;
    [SerializeField, Header("チュートリアル用指令テキスト(プランナーはいじらないで)")]
    private Text TutorialOrderText;
    [SerializeField, Header("チュートリアル用指令数テキスト(プランナーはいじらないで)")]
    private Text TutorialOrderCountText;

    [SerializeField, Header("呼び出し用XXX－シカ")]
    private GameObject[] matryoshka;

    GameObject okame;
    GameObject head;
    GameObject kabuki;
    GameObject hannya;
    Image yajirusi1;
    Image yajirusi2;
    int first = 0;
    int test = 0;

    int count = 0;
    private Order Noworder;

    private void Awake()
    {
        count = 0;
    }

    void DollPos()
    {
        if(count == 0)
        {
            okame = Instantiate(matryoshka[0], new Vector3(-8f, 0, 2.2f), Quaternion.identity);
            kabuki = Instantiate(matryoshka[1], new Vector3(-9.5f, 0, 2.2f), Quaternion.identity);
            hannya = Instantiate(matryoshka[2], new Vector3(-6.5f, 0, 2.2f), Quaternion.identity);
            okame.name = matryoshka[0].name;
            kabuki.name = matryoshka[1].name;
            hannya.name = matryoshka[2].name;

            //head = okame.gameObject.transform.FindChild("ue").gameObject;
        }
    }

    void Start ()
    {
        DollPos();
        Set_TutorialOrder();

        yajirusi1 = GameObject.Find("vector1").GetComponent<Image>();
        yajirusi2 = GameObject.Find("vector2").GetComponent<Image>();
        yajirusi1.gameObject.SetActive(false);
        yajirusi2.gameObject.SetActive(false);
    }
	
	void Update () {
        //チュートリアルのときだけチュートリアル処理
        if (GameState.instance.m_gameState == GameState._GameState.Tutorial)
        {
            GameController.instance.info = AppUtil.GetTouch();
            
            if (count == 0)
            {
                if(first == 10)
                {
                    yajirusi1.gameObject.SetActive(true);
                    yajirusi2.gameObject.SetActive(true);

                    if (GameController.instance.info == TouchInfo.Began) { }
                    else if (GameController.instance.info == TouchInfo.Ended)
                    {
                        first = 20;
                    }
                }
                else if (first == 20)
                {
                    yajirusi1.gameObject.SetActive(false);
                    yajirusi2.gameObject.SetActive(false);
                    first = 30;
                    count++;
                    Set_TutorialOrder();
                }
            }
            else if(count >= 1)
            {
                //各入力をしたか確認していき、全てをこなしたらメイン状態にする
                if (Check(TypeOrderCount(m_OrderS[count].dollInput_Type[0])))
                {
                    if (count >= m_OrderS.Length - 1)
                    {
                        StartCoroutine(MainMove());
                    }
                    if (count < m_OrderS.Length - 1)
                    {
                        if (count == 1)
                        {
                            if (head == null)
                            {
                                count++;
                                Set_TutorialOrder();
                            }
                        }
                        else if(count == 2 || count == 3)
                        {
                            count++;
                            Set_TutorialOrder();
                        }
                    }
                }
            }
        }
        //Debug.Log(GameState.instance.m_gameState);
    }
    //チュートリアル用指令表示
    void Set_TutorialOrder()
    {
        Noworder = m_OrderS[count];
        TutorialOrderText.text = Noworder.Order_Parameter[0].orderContent;
        TutorialOrderCountText.text = (count + 1).ToString();
        m_MissionAnimCon.AppearOrderAnim(m_MissionAnimCon.GetComponent<Animator>());

        StartCoroutine(DollMove());
    }
    //入力をクリアしたか
    bool Check(int _count)
    {
        if (Noworder.Order_Parameter[0].orderPoint[0] <= _count)
        {
            return true;
        }
        return false;
    }

    //タイプ別回数取得関数
    public int TypeOrderCount(Matryoshka.DollInput_Type dollInputType)
    {
        if (dollInputType == Matryoshka.DollInput_Type.Pakka) { return DollInput.PakkaCount; }
        else if (dollInputType == Matryoshka.DollInput_Type.Spin) { return DollInput.SpinCount; }
        else if (dollInputType == Matryoshka.DollInput_Type.Tap) { return DollInput.TapCount; }
        return 0;
    }

    private IEnumerator DollMove()
    {
        if(count == 0)
        {
            yield return new WaitForSeconds(4.0f);
            first = 10 ;
        }
        else
        {
            yield return new WaitForSeconds(4.0f);

            if (count == 1)
            {
                okame.transform.position = new Vector3(0f, 0f, 3.5f);
            }
            else if (count == 2)
            {
                kabuki.transform.position = new Vector3(1f, 0f, 3.5f);
            }
            else if (count == 3)
            {
                Destroy(kabuki);
                hannya.transform.position = new Vector3(-1f, 0f, 3.5f);
            }
        }
    }
    private IEnumerator MainMove()
    {
        yield return new WaitForSeconds(2.5f);
        //シーン遷移はGameControllerに処理があるので、そちらを使用してください。
        GameController.instance.ChangeScene(2);
        GameState.instance.m_gameState = GameState._GameState.Main;
    }
}
