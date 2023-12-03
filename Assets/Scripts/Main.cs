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
    public GameObject[] prefabPathogens;
    public int maxPathogens = 3; //max number of pathogens in a level
    public float delayBetweenPathogen = 1f;
    public float gameRestartDelay = 1f;

    //map of all types and costs
    public GameObject[] cells;

    public int[] prices;

    [Header("Dynamic")]
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
        chemokines = 0;
        S.UpdateGUI();
        for(int i = 0; i < cells.Length; i++){
            priceMap.Add(cells[i], prices[i]);
        }
        Invoke(nameof(SpawnPathogen), delayBetweenPathogen);
        numOfPathogen++;
    }

    void Update(){
        epidermalArray = GameObject.FindGameObjectsWithTag("Epidermal");
        if(epidermalArray.Length == 0){
            DelayedRestart();
        }
    }

    public void SpawnPathogen()
    {
        if (spawnPathogens == false | numOfPathogen > maxPathogens)
        {
            //Invoke(nameof(SpawnPathogen), delayBetweenPathogen);
            return;
        }
        else{
            // Pick a random Pathogen to instantiate
            int ndx = Random.Range(0, prefabPathogens.Length);
            GameObject go = Instantiate<GameObject>(prefabPathogens[ndx]);

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

    public static int checkPathogenNum(){
        pathogenArray = GameObject.FindGameObjectsWithTag("Pathogen");
        return pathogenArray.Length;
    }

    void UpdateGUI() {
        // Show the data in the GUITexts
        uitChemokines.text = "C: " + chemokines;
    }

    public static void chemokineIncrement(int incAmount){
        chemokines += incAmount;
        S.UpdateGUI();
    }

    void DelayedRestart()
    {
        Invoke(nameof(Restart), gameRestartDelay);
    }

    void Restart()
    {
        // Reload the original scene
        SceneManager.LoadScene("SampleScene"); 
    }

    static public void HERO_DIED()
    {
        S.DelayedRestart();
    }
}
