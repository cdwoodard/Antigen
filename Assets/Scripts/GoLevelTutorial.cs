using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoLevelTutorial : MonoBehaviour
{
    public void SwitchToTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
