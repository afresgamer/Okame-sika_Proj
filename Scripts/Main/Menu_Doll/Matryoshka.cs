using UnityEngine;

[CreateAssetMenu(fileName = "Matoryo-sika")]
public class Matryoshka : ScriptableObject {

    [Header("XXXX-シカの移動スピード")]
    public float m_MoveSpeed = 1;
    [Header("XXXX-シカの回転スピード")]
    public float m_RotSpeed = 1;
    [Header("XXXX-シカのサイズ")]
    public float m_MatryoshkaScale = 1;
    //XXX-シカの入力タイプ
    public enum DollInput_Type { None, Spin, Pakka, Tap }
    [Header("XXX-シカの入力タイプ")]
    public DollInput_Type dollInput_Type;

    public DollInput_Type Get_dollInput_Type
    {
        get { return dollInput_Type; }
    }

    //マトリョーシカの移動関数
    public static void Move(Rigidbody rb, Vector3 Pos, Vector3 TargetPos, float speed)
    {
        if (speed > 0) { rb.position = Vector3.MoveTowards(Pos, TargetPos, speed * Time.deltaTime); }
    }

    //マトリョーシカの目標視点転換関数
    public static void SetRot(GameObject m_XXX_sika, Vector3 pos, float speed)
    {
        Quaternion target = Quaternion.LookRotation(pos);
        m_XXX_sika.transform.rotation = Quaternion.Slerp(m_XXX_sika.transform.rotation, target, speed * Time.deltaTime * 2);
    }

    //マトリョーシカの移動停止関数
    public static void Stop(Rigidbody rb, Vector3 StopPos)
    {
        rb.position = StopPos;
    }
}
