using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyUiManager : MonoBehaviour {
    public Image fadeImage;
    public float endFadeOutDuration = 5f;
    public float fadeInDuration = 1f;
    public float deathFadeOutDuration = 2f;
    public static MyUiManager instance { get; private set; }
    private float percentageLife = 1;
    public Image lifeBar;


    void Awake() {
        Debug.Assert(FindObjectsOfType<MyUiManager>().Length == 1);
        if (instance == null)
            instance = this;
    }

    bool _running = false; 

    public void BlackScreen() {
        var c = fadeImage.color;
        c.a = 1f;
        fadeImage.color = c;
    }

     public void TransparentScreen() {
        var c = fadeImage.color;
        c.a = 0f;
        fadeImage.color = c;
    }

	public void OnEndFadeOut() {
        if (!_running) {
            _running = true;
            StartCoroutine(fadeOutRoutine(endFadeOutDuration,true));
        }
    }

    public void OnDeathFadeOut() {
        if (!_running) {
            _running = true;
            StartCoroutine(fadeOutRoutine(deathFadeOutDuration,false));
        }
    }

    IEnumerator fadeOutRoutine(float time, bool finishGame) {
        var c = fadeImage.color;
        var blackFadeOutValue = 0f;
        while(blackFadeOutValue < 1f) { 
            blackFadeOutValue += Time.deltaTime / time;
            c.a = blackFadeOutValue;
            fadeImage.color = c;
            yield return null;
        }
        _running = false;
        if (finishGame)
            EventManager.instance.ExecuteEvent("FinishGame");
    }

    public void OnFadeIn() {
        if (!_running) {
            _running = true;
            StartCoroutine(fadeInRoutine());
        }
    }

    IEnumerator fadeInRoutine() {
        var c = fadeImage.color;
        var blackFadeOutValue = 1f;
        while(blackFadeOutValue > 0f) {
            if (blackFadeOutValue > 0.7f)
                blackFadeOutValue -= Time.deltaTime / fadeInDuration / 2;
            else
                blackFadeOutValue -= Time.deltaTime / fadeInDuration;
            c.a = blackFadeOutValue;
            fadeImage.color = c;
            yield return null;
        }
        _running = false;
        EventManager.instance.ExecuteEvent("FadeInDone");
    }

    internal void UpdateLife(float v)
    {
        lifeBar.transform.localScale = new Vector3(v, lifeBar.transform.localScale.y, lifeBar.transform.localScale.z);
    }
}
