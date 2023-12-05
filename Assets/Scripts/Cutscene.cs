using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour {
    public string type;
    public Sprite image;
    
    public void TakeVaccine(){
        Main.Advance();
        if (!Encyclopedia.encyclopedia.ContainsKey(type)){
            Encyclopedia.addEntry(type, image, 2);
        } else {
            Encyclopedia.SetStrength(type, 2); //extra strength cause of vaccine
        }
        
    }

    public void RefuseVaccine(){
        Main.Advance();
    }
}
