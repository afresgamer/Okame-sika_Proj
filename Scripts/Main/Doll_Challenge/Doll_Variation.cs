using UnityEngine;
using UnityEngine.UI;

/*
中から現れる人形のバリエーション分け
呪い人形が出てくる確率は1/6
*/
public class Doll_Variation : MonoBehaviour
{
    int testStart = 0;
    int testEnd = 0;
    public int loop = 0;

    public int rand1;
    Image button;

    public GameObject test;

    public GameObject noroiPrefab1;
    public GameObject noroiPrefab2;
    public GameObject okamePrefab;
    const int noroisikas = 2;
    int noroiCount;

    //GameObject NoroiDoll ()
    //{
    //    GameObject prefab = null;

    //    if(noroiCount % noroisikas == 0)
    //    {
    //        prefab = noroiPrefab1;
    //    }
    //    else
    //    {
    //        prefab = noroiPrefab2;
    //    }

    //    noroiCount++;

    //    return prefab;
    //}
    //GameObject OkameDoll()
    //{
    //    GameObject prefab = null;

    //    prefab = okamePrefab;

    //    return prefab;
    //}

    //Vector3 DollPosition()
    //{
    //    return test.transform.position;
    //}

    void Start ()
    {
        button = GameObject.Find("test_button").GetComponent<Image>();
    }
	
	void Update ()
    {
        if(loop == 3)
        {
            testEnd = 10;
        }
        if(testEnd == 10)
        {
            button.gameObject.SetActive(false);
        }
	}

    public void PushButton()
    {
        if(testStart == 0)
        {
            testStart = 10;

            System.Random r = new System.Random();
            rand1 = r.Next(1, 6);
            //button.gameObject.SetActive(false);
        }
        else if(testStart == 10)
        {
            if(loop != 3)
            {
                if (rand1 == 1)
                {
                    //GameObject noroisika = (GameObject)Instantiate(
                    //    NoroiDoll(),
                    //    DollPosition(),
                    //    Quaternion.identity);

                    testEnd = 10;
                }
                else
                {
                    //GameObject okamesika = (GameObject)Instantiate(
                    //    OkameDoll(),
                    //    DollPosition(),
                    //    Quaternion.identity);

                    System.Random r = new System.Random();
                    rand1 = r.Next(1, 6);

                    loop += 1;
                }
            }
        }
    }
}
