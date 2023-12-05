using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoLevelTutorial : MonoBehaviour
{
    public GameObject LevelOneButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Main.CompletedLevels.Contains("Tutorial"))
        {
            LevelOneButton.SetActive(true);
        }

    }

    public void SwitchToTutorial()
    {  

        SceneManager.LoadScene("Tutorial");
    }
}
