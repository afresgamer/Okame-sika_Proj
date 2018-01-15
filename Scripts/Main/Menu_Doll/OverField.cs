using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverField : MonoBehaviour
{
    private List<GameObject> m_hitObjects = new List<GameObject>();

    [Header("範囲内に入るとゲームオーバーの人形数")]
    public int DollCount = 2;
    int over = 0;

    void FixedUpdate()
    {
        if(m_hitObjects.Count >= DollCount)
        {
            over = 10;
            PlayerPrefs.SetInt("over", over);
            StartCoroutine(NextScene());
        }

        m_hitObjects.Clear();
    }

    void OnTriggerStay(Collider i_other)
    {
        if(i_other.tag == "Noroi-sika" || i_other.tag == "Kabuki-sika")
        {
            m_hitObjects.Add(i_other.gameObject);
        }
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(0.1f);
        GameController.instance.ChangeScene(3);
    }
}
