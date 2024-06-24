using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private AudioMixer musicMixer;
    [SerializeField]
    private AudioMixer soundMixer;
    private AudioSource m_AudioSourse;
    [SerializeField]
    private GameObject menuPanel;
    private bool isPlaying;

    void Start()
    {
        LoadParams();
        m_AudioSourse = GetComponent<AudioSource>();
        isPlaying = true;
        menuPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isPlaying)
        {
            if (menuPanel != null)
            {
                menuPanel.SetActive(true);
                m_AudioSourse.Pause();
                isPlaying = false;
                SetIsPlay(isPlaying);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !isPlaying)
        {
            if (menuPanel != null)
            {
                menuPanel.SetActive(false);
                m_AudioSourse.Play();
                isPlaying = true;
                SetIsPlay(isPlaying);
            }
        }
    }

    public void ContinueClick()
    {
        menuPanel.SetActive(false);
        m_AudioSourse.Play();
        SetIsPlay(isPlaying);
        float musicVol, soundVol;
        soundMixer.GetFloat("MasterVolume", out soundVol);
        musicMixer.GetFloat("MasterVolume", out musicVol);
        SaveParams(soundVol, musicVol);
    }

    public void OnSliderValueChangeSound(float value)
    {
        soundMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
    }

    public void OnSliderValueChangeMusic(float value)
    {
        musicMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
    }


    public void SetIsPlay(bool val)
    {
        gameObject.transform.parent.GetComponent<MoveController>().IsPlay = val;
        gameObject.transform.parent.GetComponent<PlayerAnimationContrroller>().IsPlay = val;
        gameObject.transform.parent.GetComponent<ShootController>().IsPlay = val;
        GameObject[] massObj = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject obj in massObj)
            obj.GetComponentInChildren<PatrolController>().IsPlay = val;
        massObj = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject obj in massObj)
            obj.GetComponent<BulletController>().IsPlay = val;
        massObj = GameObject.FindGameObjectsWithTag("Check");
        foreach (GameObject obj in massObj)
            obj.GetComponent<CheckController>().IsPlay = val;
    }

    private void SaveParams(float soundVol, float musicVol)
    {
        PlayerPrefs.SetFloat("SoundVol", soundVol);
        PlayerPrefs.SetFloat("MusicVol", musicVol);
        PlayerPrefs.Save();
    }

    private void LoadParams()
    {
        if (PlayerPrefs.HasKey("SoundVol"))
            soundMixer.SetFloat("MasterVolume", Mathf.Log10(PlayerPrefs.GetFloat("SoundVol")) * 20);
        else

            musicMixer.SetFloat("MasterVolume", Mathf.Log10(0) * 20);
        if (PlayerPrefs.HasKey("MusicVol"))
            musicMixer.SetFloat("MasterVolume", Mathf.Log10(PlayerPrefs.GetFloat("MusicVol")) * 20);
        else
            musicMixer.SetFloat("MasterVolume", Mathf.Log10(0) * 20);
    }

}
