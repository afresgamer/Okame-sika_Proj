using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;

    //各種サウンド
    public AudioSource BGM;
    //サウンド設定用変数
    public SoundSetting m_SoundSetting;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }

    void Start () {
        //BGMの設定
        BGM.volume = m_SoundSetting.BGM;
        BGM.mute = m_SoundSetting.Mute;
	}
	
	void Update () {

	}

    public void PlayBGM()
    {
        if (BGM.isPlaying) BGM.Stop();
        else { BGM.Play(); }
    }

    public void PlaySE(AudioSource se,int index, bool isLoop)
    {
        if (!m_SoundSetting.Mute)
        {
            se.loop = isLoop;
            if (se.isPlaying) se.Stop();
            if (se.loop) { se.Play(); }
            else { se.PlayOneShot(m_SoundSetting.se_AudioClip[index]); }
        }
    }

    public void StopBGM()
    {
        BGM.Stop();
        BGM.time = 0;
    }

    public void StopSE(AudioSource se,int index)
    {
        se.Stop();
        se.time = 0;
    }
}
