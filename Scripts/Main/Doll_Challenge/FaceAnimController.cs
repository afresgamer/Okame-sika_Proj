using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceAnimController : MonoBehaviour {

    [SerializeField, Header("顔の種類")]
    private Material[] FaceMaterials;
    private Renderer Face_renderer;
    float count = 0;
    [SerializeField, Header("顔の動きスピード")]
    private float speed = 1;

    private void Start()
    {
        Face_renderer = GetComponent<Renderer>();
    }

    void Update () {
        count += Time.deltaTime * speed;
        if(count >= FaceMaterials.Length) { count = 0; }
        Face_renderer.material = FaceMaterials[(int)count];
        //if (GameController.instance.Timer > 0)
        //{
            
        //}
	}
}
