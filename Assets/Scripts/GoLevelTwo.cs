using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoLevelTwo : MonoBehaviour
{
    public GameObject LevelTwoButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Main.CompletedLevels.Contains("Leveln"))
        {
            LevelTwoButton.SetActive(true);
        }
    }

    public void SwitchToLevelTwo()
    {
        SceneManager.LoadScene("Leveln+1");
    }
}
