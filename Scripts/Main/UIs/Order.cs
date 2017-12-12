using UnityEngine;

[CreateAssetMenu(fileName = "Order")]
public class Order : ScriptableObject {

    public enum Order_LEVEL { Easy, Normal, Hard }
    public Order_LEVEL order_Level;
    public Matryoshka.DollInput_Type[] dollInput_Type;
    [System.Serializable]
    public struct Order_ParameterS
    {
        public string orderContent;
        public int[] orderPoint;
    };
    [Header("指令の内容")]
    public Order_ParameterS[] Order_Parameter;

    //指令難易度ゲッター
    public Order_LEVEL Get_OrderLevel
    {
        get { return order_Level; }
    }
    //ランダム指令ゲッター
    public int GetOrder_ParameterCount
    {
        get
        {
            int random_order = Random.Range(0, Order_Parameter.Length - 1);
            return random_order;
        }
    }
}
