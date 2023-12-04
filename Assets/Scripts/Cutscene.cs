using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour {
    public string type;
    public Sprite image;
    
    public void TakeVaccine(){
        SceneManager.LoadScene("Leveln+1");
        if (!Encyclopedia.encyclopedia.ContainsKey(type)){
            Encyclopedia.addEntry(type, image);
        }
    }

    public void RefuseVaccine(){
        SceneManager.LoadScene("Leveln+1");
    }
}
