using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoLevelOne : MonoBehaviour
{

    public GameObject LevelOneButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      print("hello?");
      if (Main.CompletedLevels.Contains("Leveln"))
        {
            LevelOneButton.SetActive(true);
        }
    }

    public void SwitchToLevelOne()
    {
        SceneManager.LoadScene("Leveln");
    }
}