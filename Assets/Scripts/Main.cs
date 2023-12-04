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

    public static List<string> LevelStrings = new List<string> {"Tutorial", "Leveln", "Leveln+1" };
    public static HashSet<string> CompletedLevels = new HashSet<string> { };
    public static string IntendedLevel = "Tutorial";

    //map of all types and costs
    public GameObject[] cells;

    public int[] prices;

    
    public GameObject purchase1;
    public GameObject purchase2;
    public GameObject purchase3;

    [Header("Dynamic")]

    public static bool unlockedTCells = false;
    public int numOfPathogen = 0;

    public int tutorialProgress = 0;

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


        S.UpdateGUI();
        for(int i = 0; i < cells.Length; i++){
            if(!priceMap.ContainsKey(cells[i])){
                priceMap.Add(cells[i], prices[i]);
            }
        }

        Scene scene = SceneManager.GetActiveScene();

        if(scene.name != "Menu" && scene.name != "Tutorial"){
            Invoke(nameof(SpawnPathogen), delayBetweenPathogen);
            numOfPathogen++;
        }
    }

    void Update(){
        epidermalArray = GameObject.FindGameObjectsWithTag("Epidermal");
        
        Scene scene = SceneManager.GetActiveScene();

        if(epidermalArray.Length == 0 && scene.name != "Menu" && scene.name != "Levels")
        {
            DelayedRestart();
        }

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
                        
                    }
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
                    Invoke(nameof(SpawnPathogen), delayBetweenPathogen);
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
        if (scene.name != "Menu" && scene.name != "Levels"){
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

    static public void NoEpidermalCells()
    {
        S.DelayedRestart();
    }

    static public void NoPathogens()
    {
        S.DelayedAdvance();
    }
}
