using UnityEngine;
using UnityEngine.UI;

/*
タイマーの時間帯（値）によって朝・夜などの表示をする
*/
public class Morning_Night : MonoBehaviour {

    //時間帯
    [SerializeField, Header("時間の流れるスピード")]
    private float Time_Zone_speed = 1;

    Text Time_Zone_text;
    //太陽の動き
    [SerializeField, Header("太陽")]
    private Light SunLight;

	void Start () {
        Time_Zone_text = GetComponent<Text>();
        Time_Zone_text.text = "朝";
	}
	
	void Update () {
        if(GameState.instance.m_gameState == GameState._GameState.Main)
        {
            SunLight.transform.rotation *= Quaternion.Euler(Time.deltaTime * 10 * Time_Zone_speed, 0, 0);
            Quaternion target = SunLight.transform.rotation;
            float target_axis = target.eulerAngles.x;
            //Debug.Log(target_axis);
            if (target_axis >= 0 && target_axis < 180)
            {
                Time_Zone_text.text = "朝";
            }
            if (target_axis >= 180 && target_axis < 360)
            {
                Time_Zone_text.text = "夜";
            }
        }
        
    }
}
