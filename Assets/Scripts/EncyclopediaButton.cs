using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EncyclopediaButton : MonoBehaviour { 

    // General Sprites
    public GameObject[] eObjects;

    public void BackToGame()
    {
        foreach (GameObject go in eObjects){
            go.SetActive(false);
        }

        Time.timeScale = 1;


        //tutorial code
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "Tutorial" && Main.tutorialProgress == 24){
            Main.tutorialProgress++;
        }
    }

}
