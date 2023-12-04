using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialButton : MonoBehaviour {
    public void Click(){
        if(Main.tutorialProgress == 17 || Main.tutorialProgress == 26){
            Main.tutorialProgress++;
        }
    }
}
