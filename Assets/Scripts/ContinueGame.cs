using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNewGame()
    {
        Main.CompletedLevels.Clear();
        Encyclopedia.encyclopedia.Clear();
        Main.IntendedLevel = "Tutorial";
        SwitchToSampleScene();
    }

    public void SwitchToSampleScene(){
        //roll back encyclopedia
        Encyclopedia.RollBack();
        SceneManager.LoadScene(Main.IntendedLevel);
    }
}
