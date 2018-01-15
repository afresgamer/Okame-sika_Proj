using UnityEngine;

[CreateAssetMenu(fileName = "Matoryo-sika")]
public class Matryoshka : ScriptableObject {

    [Header("XXXX-シカの移動スピード")]
    public float m_MoveSpeed = 1;
    [Header("XXXX-シカの回転スピード")]
    public float m_RotSpeed = 1;
    //[Header("XXXX-シカのサイズ")]
    //public float m_MatryoshkaScale = 1;
    //XXX-シカの入力タイプ
    public enum DollInput_Type { None, Spin, Pakka, Tap }
    [Header("XXX-シカの入力タイプ")]
    public DollInput_Type dollInput_Type;

    //マトリョーシカの移動関数
    public static void Move(GameObject _XXX_sika, Vector3 Pos, Vector3 TargetPos, float speed)
    {
        if (speed > 0)
        {
            _XXX_sika.transform.position = Vector3.MoveTowards(Pos, TargetPos, speed * Time.deltaTime);
        }
    }

    //マトリョーシカの目標視点転換関数
    public static void SetRot(GameObject _XXX_Sika, Vector3 pos, float speed)
    {
        Quaternion target = Quaternion.LookRotation(pos);
        _XXX_Sika.transform.rotation = Quaternion.Slerp(_XXX_Sika.transform.rotation, target, speed * Time.deltaTime);
    }

    //マトリョーシカの移動停止関数
    public static void Stop(Rigidbody rb, Vector3 StopPos)
    {
        rb.position = StopPos;
    }
}
