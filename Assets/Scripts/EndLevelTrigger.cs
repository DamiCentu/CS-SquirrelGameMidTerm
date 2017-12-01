using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelTrigger : MonoBehaviour {
 //   public Text fadeImage;

 //   bool _endGame;

	//void Start () {
 //       _endGame = false;
 //   }
	
	//void Update () {
 //       if (_endGame) {

 //       }
	//}

    void OnTriggerEnter(Collider o) {
        //_endGame = true;
        GameManager.instance.EndLevel();
    }
}
