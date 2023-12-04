using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class About : MonoBehaviour
{
    public GameObject AboutPanel;
    public GameObject AboutMessage;
    public GameObject CloseButton;

    public void AboutOpen(){
        AboutPanel.SetActive(true);
        AboutMessage.SetActive(true);
        CloseButton.SetActive(true);
    }

    public void AboutClose(){
        AboutPanel.SetActive(false);
        AboutMessage.SetActive(false);
        CloseButton.SetActive(false);
    }
}
