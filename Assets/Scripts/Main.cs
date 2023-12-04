using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour {
    static private Main S;

    [Header("Inscribed")]
    public TextMeshProUGUI uitChemokines;
    public bool spawnPathogens = true;
    public GameObject[] initialPathogens;
    public GameObject[] wavePathogens;
    public int maxInitialPathogens = 3; //max number of initial pathogens in a level
    public int maxWavePathogens = 5;//max number of wave pathogens in a level
    public float delayBetweenPathogen = 2f;
    public float gameRestartDelay = 1f;

    public static List<string> LevelStrings = new List<string> {"Tutorial", "Leveln", "Cutscene", "Leveln+1" };
    public static HashSet<string> CompletedLevels = new HashSet<string> { };
    public static string IntendedLevel = "Tutorial";

    //map of all types and costs
    public GameObject[] cells;

    public int[] prices;



    //tons of tutorial objects listed here
    
    public GameObject purchase1;
    public GameObject purchase2;
    public GameObject purchase3;

    public GameObject box1;
    public GameObject box2;
    public GameObject box3;
    public GameObject box4;
    public GameObject box5;

    public GameObject arrow1;
    public GameObject arrow2;
    public GameObject arrow3;    

    public GameObject text1;
    public GameObject text2;
    public GameObject text1image;
    public GameObject button1;

    [Header("Dynamic")]
    public static int tutorialProgress = 0;


    public static bool unlockedTCells = false;
    public int numOfPathogen = 0;

    

    public GameObject[] epidermalArray;
    public static GameObject[] pathogenArray;
    public static Dictionary<GameObject, int> priceMap = new Dictionary<GameObject, int>();
    public static int chemokines{        
        get;
        private set;
    }

    public static List<GameObject> epidermalCells = new List<GameObject>();

    // Start is called before the first frame update
    void Start() {
        S = this;
        //set (or reset) some starting variables
        chemokines = 0;
        unlockedTCells = false;
        tutorialProgress = 0;

        S.UpdateGUI();
        for(int i = 0; i < cells.Length; i++){
            if(!priceMap.ContainsKey(cells[i])){
                priceMap.Add(cells[i], prices[i]);
            }
        }

        Scene scene = SceneManager.GetActiveScene();

        if(scene.name == "Leveln" || scene.name == "Leveln+1"){
            Invoke(nameof(SpawnPathogen), delayBetweenPathogen);
            numOfPathogen++;
        }
    }

    void Update(){
        epidermalArray = GameObject.FindGameObjectsWithTag("Epidermal");
        
        Scene scene = SceneManager.GetActiveScene();

        //checks for lose condition
        if(epidermalArray.Length == 0 && scene.name != "Menu" && scene.name != "Levels" && scene.name != "Cutscene")
        {
            DelayedRestart();
        }
        //checks for win condition
        int num = checkPathogenNum();
        if(num == 0 && numOfPathogen >= maxInitialPathogens + maxWavePathogens && scene.name != "Tutorial"){
            S.DelayedAdvance();
        }


        //massive switch statement that progresses the tutorial
        if(scene.name == "Tutorial"){
            switch (tutorialProgress) {
                case 0:{
                    purchase1.SetActive(false);
                    purchase2.SetActive(false);
                    purchase3.SetActive(false);
                    Invoke(nameof(SpawnPathogen), delayBetweenPathogen);
                    tutorialProgress++;
                    break;
                }
                case 1:{
                    if(chemokines >= 3){
                        box1.SetActive(true);
                        tutorialProgress++;
                    }
                    break;
                }
                //case 2 is handled in Purchase.cs (checks if clicked)
                case 3:{
                    box1.SetActive(false);
                    arrow1.SetActive(true);
                    tutorialProgress++;
                    break;
                }
                //case 4 is handled in ImmuneCell.cs (checks if placed)
                case 5:{
                    arrow1.SetActive(false);
                    tutorialProgress++;
                    break;
                }
                //case 6 handled by Pathogen.cs (checks if killed)
                case 7:{
                    for(int i = 0; i < 5; i++){ 
                        //spawn 5 more pathogens
                        int ndx = Random.Range(0, initialPathogens.Length);
                        GameObject go = Instantiate<GameObject>(initialPathogens[ndx]);
                        // Put the pathogen in a random y position at the right of the screen
                        float pathogenYpos = Random.Range(-5f,5f); //randomly pick from 0 to 1
                    
                        // Set the initial position for the spawned pathogen
                        Vector3 pos = Vector3.zero;
                        pos.x = 10f;
                        pos.y = pathogenYpos;
                        go.transform.position = pos;
                        numOfPathogen++;
                    }
                    tutorialProgress++;
                    break;
                }
                //case 8 handled by immune cell, checks if killed
                case 9:{
                    if(chemokines >= 10){
                        purchase3.SetActive(true);
                        box2.SetActive(true);
                        tutorialProgress++;
                    }
                    break;
                }
                //case 10 handled by Purchase.cs (checks if clicked)
                case 11:{
                    box2.SetActive(false);
                    purchase2.SetActive(true);
                    tutorialProgress++;
                    break;
                }
                case 12:{
                    if(chemokines >= 5){
                        box3.SetActive(true);
                        tutorialProgress++;
                    }
                    break;
                }
                //case 13 handled by Purchase.cs
                case 14:{
                    box3.SetActive(false);
                    arrow2.SetActive(true);
                    tutorialProgress++;
                    break;
                }
                //case 15 handled by ImmuneCell.cs (checks if placed)
                case 16:{
                    arrow2.SetActive(false);
                    text1.SetActive(true);
                    text1image.SetActive(true);
                    button1.SetActive(true);
                    Time.timeScale = 0;
                    tutorialProgress++;
                    break;
                }
                //case 17 handled by TutorialButton (checks if pressed)
                case 18:{
                    text1.SetActive(false);
                    text1image.SetActive(false);
                    button1.SetActive(false);
                    box4.SetActive(true);
                    purchase1.SetActive(true);
                    Time.timeScale = 1;
                    tutorialProgress++;
                    break;
                }
                //case 19 handled by Purchase.cs
                case 20:{
                    box4.SetActive(false);
                    arrow3.SetActive(true);
                    tutorialProgress++;
                    break;
                }
                //case 21 handled by MemoryTCell.cs (checks if new pathogen detected)
                case 22:{
                    arrow3.SetActive(false);
                    box5.SetActive(true);
                    tutorialProgress++;
                    break;
                }
                //case 23 handled by Encyclopedia.cs (checks if updated)
                case 24:{
                    box5.SetActive(false);
                    tutorialProgress++;
                    break;
                }
                case 25:{
                    //when all the pathogens are dead, win game
                    int pNum = checkPathogenNum();
                    if(pNum == 0){
                        text2.SetActive(true);
                        text1image.SetActive(true);
                        button1.SetActive(true);
                        Time.timeScale = 0;
                        tutorialProgress++;
                    }
                    break;
                }
                //case 26 handled by TutorialButton
                case 27:{
                    text2.SetActive(false);
                    text1image.SetActive(false);
                    button1.SetActive(false);
                    Time.timeScale = 1;
                    DelayedAdvance(); //you won!
                    tutorialProgress++;
                    break;
                }
            }
        }
    }

    public void SpawnPathogen(){ 
        //check if supposed to spawn pathogens
        if(spawnPathogens && numOfPathogen < maxInitialPathogens + maxWavePathogens){
            //if done with initial, begin with wave
            if (numOfPathogen > maxInitialPathogens) {
                for(int i = 0; i < maxWavePathogens; i++){
                    int ndx = Random.Range(0, wavePathogens.Length);
                    GameObject go = Instantiate<GameObject>(wavePathogens[ndx]);
                    // Put the pathogen in a random y position at the right of the screen
                    float pathogenYpos = Random.Range(-5f,5f); //randomly pick from 0 to 1
                
                    // Set the initial position for the spawned pathogen
                    Vector3 pos = Vector3.zero;
                    pos.x = 10f;
                    pos.y = pathogenYpos;
                    go.transform.position = pos;
                    numOfPathogen++;
                }
                return;
            //trickle in pathogens one by one
            } else {
                // Pick a random Pathogen to instantiate
                
                int ndx = Random.Range(0, initialPathogens.Length);
                GameObject go = Instantiate<GameObject>(initialPathogens[ndx]);

                // Put the pathogen in a random y position at the right of the screen
                float pathogenYpos = Random.Range(-5f,5f); //randomly pick from 0 to 1
            
                // Set the initial position for the spawned pathogen
                Vector3 pos = Vector3.zero;
                pos.x = 10f;
                pos.y = pathogenYpos;
                go.transform.position = pos;
                Invoke(nameof(SpawnPathogen), delayBetweenPathogen);
                numOfPathogen++;
            }
        }
        return;
    }

    public static int checkPathogenNum(){
        pathogenArray = GameObject.FindGameObjectsWithTag("Pathogen");
        return pathogenArray.Length;
    }

    void UpdateGUI() {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name != "Menu" && scene.name != "Levels" && scene.name != "Cutscene"){
            // Show the data in the GUITexts
            uitChemokines.text = "C: " + chemokines;
        }
    }

    public static void chemokineIncrement(int incAmount){
        chemokines += incAmount;
        S.UpdateGUI();
    }

    void DelayedRestart()
    {
        Invoke(nameof(Restart), gameRestartDelay);
    }

    void DelayedAdvance()
    {
        Invoke(nameof(Advance), gameRestartDelay);
    }

    void Restart()
    {
        //roll back encyclopedia
        Encyclopedia.RollBack();
        // Reload the current scene
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    void Advance() //advance to the next level
    {
        // Reload the original scene
        // SceneManager.LoadScene("Leveln+1");

        // Reload the original scene
        print("entering advance?");
        Scene scene = SceneManager.GetActiveScene();
        string curr_scene = scene.name;

        int current_index = LevelStrings.IndexOf(curr_scene);
        int next_index = current_index + 1;
        if (next_index == LevelStrings.Count)
        {
            next_index = current_index;
        }
        CompletedLevels.Add(LevelStrings[current_index]);
        string next_scene = LevelStrings[next_index];
        IntendedLevel = next_scene;
        SceneManager.LoadScene(next_scene);

    }
}
