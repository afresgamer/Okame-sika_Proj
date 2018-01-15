using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {

    [Header("ライト点滅スピード")]
    public float Light_Speed = 1;
    Light DanroLight;
    bool IsTopRange = false;

	void Start () {
        DanroLight = GetComponent<Light>();
	}
	
	void Update () {
        if (!IsTopRange) { DanroLight.range += Time.deltaTime * Light_Speed; }
        if(DanroLight.range > 10){ IsTopRange = true; }
        if (IsTopRange)
        {
            DanroLight.range -= Time.deltaTime * Light_Speed;
            if (DanroLight.range < 5) { IsTopRange = false; }
        }
        //Debug.Log(DanroLight.range);
	}
}
