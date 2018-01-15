using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YazirusiController : MonoBehaviour {
    
    public enum UI_Type { NULL,PakkaUI,TapUI, SpinUI }
    [Header("UIの種類")]
    public UI_Type m_UI_Type;
    //動かすアニメーター
    [HideInInspector]
    public Animator YazirusiAnim;
}
