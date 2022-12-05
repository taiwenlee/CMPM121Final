using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    public void Setup()
    {
        if (EnemyAI.PlayerDeadth == true){
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
            gameObject.SetActive(true);
        } else{
            gameObject.SetActive(false);
        }
    }
}