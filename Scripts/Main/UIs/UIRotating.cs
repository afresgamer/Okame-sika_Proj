using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRotating : MonoBehaviour {

    [Header("回転スピード")]
    public float Rot_speed = 1;

    void Update () {
        transform.Rotate(0, -Rot_speed * 10 * Time.deltaTime, 0);
	}
}
