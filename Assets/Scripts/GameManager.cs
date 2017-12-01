using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public PlayerBrain Player;
    public bool fadeAtStart = false;
    public bool cursorVisible = false;

    public static GameManager instance = null;
    private List<BeatleTestBehaviur> _beetles = new List<BeatleTestBehaviur>();

    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        if (!cursorVisible) { 
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        } 
    }

    void Start() { 
        if(fadeAtStart) {
            MyUiManager.instance.BlackScreen();
            MyUiManager.instance.OnFadeIn();
        }
        EventManager.instance.SubscribeEvent("LoadPlayer", LoadCheckPoint);
        EventManager.instance.ExecuteEvent("LoadPlayer");
        //EventManager.instance.SubscribeEvent("FinalPart",FinalPart);
        EventManager.instance.SubscribeEvent("FinishGame",FinalPart);
        //print("subscribo load player");
    }

    private void FinalPart(object[] parameterContainer)
    {
        Application.Quit();
    }

    internal void AlertBeetles(Vector3 soundPosition) {
        foreach (var beetles in _beetles) {
            beetles.hearSound(soundPosition); 
        }
    }

    internal void PlayerDie() { 
        ResetScene();
    }

    public void DoDamageTo(IDamagable damageable, int damage) {
        damageable.TakeDamage(damage);
    }
    
    public void DoEffectDamageTo(IDamagable damageable, int damage, int effect, float intervalBetweenDamage, int quantityOfHits) {
        damageable.TakeDamage(damage);
        StartCoroutine(effectDamageRoutine(damageable,effect,intervalBetweenDamage,quantityOfHits));
    }

    //CUANDO MUERA EL PLAYER HAY QUE PARAR TODAS LAS COURUTINAS IN GAME CON EL EVENT MANAGER
    IEnumerator effectDamageRoutine(IDamagable damageable ,int effectDamage, float intervalBetweenDamage, int quantityOfHits) {
        var wait = new WaitForSeconds(intervalBetweenDamage);
        yield return wait;
        for (int i = 0; i < quantityOfHits; i++) {
            damageable.TakeDamage(effectDamage);
            yield return wait;
        }
        yield return null;
    }

    public void StunAndDamageTo(IDamagable damageable, int damage, float timeStunned){ 
        damageable.TakeDamage(damage); 
    }


    public void ResetScene() { 
        MySceneManager.instance.LoadCurrentScene();
        EventManager.instance.SubscribeEvent("LoadPlayer", LoadCheckPoint);
        //print("subscribo load player");
    }

    internal void LoadCheckPoint(params object[] parametersWrapper) {
        PlayerBrain player = Player.GetComponent<PlayerBrain>();
        Vector3? playerPosition = CheckPointManager.instance.playerPosition;
        float? playerLife = CheckPointManager.instance.playerLife;
        Quaternion? playerRotation = CheckPointManager.instance.playerRotation;

        player.SetPosition(playerPosition != null ? (Vector3)playerPosition : player.transform.position)
           // .Setlife(playerLife != null ? (int)playerLife : player.life)
            .SetRotation(playerRotation != null ? (Quaternion)playerRotation : player.transform.rotation);

        CameraManager.instance.OnLoadCheckpoint(CheckPointManager.instance.cameraLocalEulerRot);
    }

    internal void AddBeetle(BeatleTestBehaviur beetle) {
        _beetles.Add(beetle);
    } 

    public void EndLevel() { 
        MyUiManager.instance.OnEndFadeOut();
    } 
}
