using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Okame_Order_Controller : MonoBehaviour {

    //現在指令数
    private int Now_Order_count = 0;
    [SerializeField,Header("現在指令数用テキスト")]
    private Text Order_CountText;
    [SerializeField,Header("指令内容用テキスト")]
    private Text OrderText;
    //指令データ(難易度簡単、普通、難しい)
    [SerializeField, Header("指令データ(ここを変えると表示内容が変わります)")]
    private Order[] OrderS;
    //取得用指令変数
    private int OrderNum;
    private Order NowOrder;
    private Order.Order_LEVEL NowOrderLevel = Order.Order_LEVEL.Easy;
    //難易度別指令数
    const int Easy_OrderCount = 5;
    const int Normal_OrderCount = 7;
    //難易度ハードの時の時間制限用変数
    private string Fast = "素早く";
    private float TimerSpeed = 2.0f;
    //経過時間保存用変数
    private float NowTime = 0;
    [SerializeField, Header("素早くの最大経過時間")]
    private float time = 3;
    private bool IsTimer = false;
    [SerializeField, Header("指令表示アニメーション")]
    private MissionAnimController MissionAnimCon;

    private void Awake()
    {
        GameController.instance.ClearOrderCount = 0;
    }

    void Start()
    {
        //初期化
        NowOrder = Get_RandomOrder(OrderS, 0, 2);
        OrderNum = NowOrder.GetOrder_ParameterCount;
        SelectOrder();
        NowTime = time;
    }

    void Update()
    {
        //メイン状態のときだけ処理
        if(GameState.instance.m_gameState == GameState._GameState.Main)
        {
            //タイマー
            if (IsTimer)
            {
                NowTime -= TimerSpeed * Time.deltaTime;
                if (NowTime <= 0) { SelectOrder(); NowTime = time; IsTimer = false; }
            }

            //難易度を取得
            //if (Now_Order_count < Easy_OrderCount)
            //{ NowOrderLevel = Order.Order_LEVEL.Easy; }
            //else if (Now_Order_count < Normal_OrderCount)
            //{ NowOrderLevel = Order.Order_LEVEL.Normal; }
            //else { NowOrderLevel = Order.Order_LEVEL.Hard; }

            //難易度ごとに処理
            //Debug.Log(NowOrder.dollInput_Type.Length);
            switch (NowOrderLevel)
            {
                case Order.Order_LEVEL.Easy:
                    //タイプ数がいくつあるか確認　そのタイプごとに回数を取得
                    //達成回数をクリアしてるか確認
                    //達成してたら次の指令を発行
                    OrderSuccess(NowOrder.dollInput_Type.Length, 0, 2);
                    break;
                case Order.Order_LEVEL.Normal:

                    break;
                case Order.Order_LEVEL.Hard:

                    break;
            }
        }
    }

    //長さによって成功しているか判定
    public void OrderSuccess(int length, int min, int max)
    {
        if(length > 0 && max > min)
        {
            if (length == 1)
            {
                if (Check(NowOrder.Order_Parameter[OrderNum], TypeOrderCount(NowOrder.dollInput_Type[0])))
                {
                    NowOrder = Get_RandomOrder(OrderS, min, max);
                    OrderNum = NowOrder.GetOrder_ParameterCount;
                    SelectOrder(NowOrder.Order_Parameter[OrderNum]);
                }
            }
            else if (length == 2)
            {
                if (Check(NowOrder.Order_Parameter[OrderNum],
                    TypeOrderCount(NowOrder.dollInput_Type[0]), TypeOrderCount(NowOrder.dollInput_Type[1])))
                {
                    NowOrder = Get_RandomOrder(OrderS, min, max);
                    OrderNum = NowOrder.GetOrder_ParameterCount;
                    SelectOrder(NowOrder.Order_Parameter[OrderNum]);
                }
            }
            else if (length == 3)
            {
                if (Check(NowOrder.Order_Parameter[OrderNum],
                    TypeOrderCount(NowOrder.dollInput_Type[0]),
                    TypeOrderCount(NowOrder.dollInput_Type[1]), TypeOrderCount(NowOrder.dollInput_Type[2])))
                {
                    NowOrder = Get_RandomOrder(OrderS, min, max);
                    OrderNum = NowOrder.GetOrder_ParameterCount;
                    SelectOrder(NowOrder.Order_Parameter[OrderNum]);
                }
            }
        }
    }
    //goto ここに時間制限判定入れる
    //成功確認関数
    public bool Check(Order.Order_ParameterS orderParameter, int num, int num2 = 0, int num3 = 0)
    {
        int length = orderParameter.orderPoint.Length;
        int count = orderParameter.orderPoint[0];
        //入力が一個の場合
        if (length == 1 && num >= count && num2 == 0) { num = 0; return true; }
        //入力が二個の場合
        else if (length == 2 && num2 != 0 && num3 == 0)
        {
            if (num >= count && num2 >= orderParameter.orderPoint[1]) { num = 0; num2 = 0; return true; }
        }
        //入力が三個の場合
        else if (length == 3 && (num2 != 0 && num3 != 0))
        {
            if (num >= count && num2 >= orderParameter.orderPoint[1] && num3 >= orderParameter.orderPoint[2])
            { num = 0; num2 = 0; num3 = 0; return true; }
        }
        //他は全部失敗
        return false;
    }
    //指令決定関数
    public void SelectOrder()
    {
        Now_Order_count++;
        Order_CountText.text = Now_Order_count.ToString();
        NowOrder = Get_RandomOrder(OrderS, 0, 2);
        OrderNum = NowOrder.GetOrder_ParameterCount;
        OrderText.text = NowOrder.Order_Parameter[OrderNum].orderContent;
    }
    public void SelectOrder(Order.Order_ParameterS orderParameter)
    {
        Now_Order_count++;
        GameController.instance.ClearOrderCount++;
        Order_CountText.text = Now_Order_count.ToString();
        OrderText.text = orderParameter.orderContent;
    }
    //ランダム指令型選択関数
    public Order Get_RandomOrder(Order[] orderS, int min, int max)
    {
        int random_num = Random.Range(min, max);
        return orderS[random_num];
    }
    //タイプ別回数取得関数
    public int TypeOrderCount(Matryoshka.DollInput_Type dollInputType)
    {
        if(dollInputType == Matryoshka.DollInput_Type.Pakka) { return DollInput.AddPakkaCount; }
        else if(dollInputType == Matryoshka.DollInput_Type.Spin) { return DollInput.AddSpinCount; }
        else if(dollInputType == Matryoshka.DollInput_Type.Tap) { return DollInput.AddTapCount; }
        return 0;
    }
}
