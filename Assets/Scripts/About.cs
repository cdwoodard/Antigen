using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class About : MonoBehaviour {
    public GameObject AboutMessage;
    public GameObject uitAbout;

    public bool open = false;

    public void Click(){
        if(open){
            open = false;
            AboutClose();
        } else {
            open = true;
            AboutOpen();
        }
    }

    public void AboutOpen(){
        AboutMessage.SetActive(true);
        uitAbout.SetActive(true);
    }

    public void AboutClose(){
        AboutMessage.SetActive(false);
        uitAbout.SetActive(false);
    }
}
