using UnityEngine;

/*
中から現れる人形のバリエーション分け
呪い人形が出てくる確率は1/6(数的に2/3)
*/
public class Doll_Variation : MonoBehaviour
{
    [Header("マトリョーシカの確率最大値")]
    public int rand;
    [Header("マトリョーシカの種類")]
    public GameObject[] matryosikaS;

    private void Start()
    {
        GameObject XXX_sika = Select_Matryosika();
        GameObject matryosika = Instantiate(XXX_sika, transform.position, Quaternion.identity);
        matryosika.name = XXX_sika.name;
        matryosika.transform.SetParent(transform.parent);
        matryosika.SetActive(false);
    }

    GameObject Select_Matryosika()
    {
        int randomNum = Random.Range(0, rand);
        GameObject XXX_sika = matryosikaS[randomNum];
        XXX_sika.name = matryosikaS[randomNum].name;
        return matryosikaS[randomNum];
    }

}
