using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Main : MonoBehaviour {
    static private Main S;

    [Header("Inscribed")]
    public TextMeshProUGUI uitChemokines;
    public bool spawnPathogens = true;
    public GameObject[] prefabPathogens;
    public float pathogenSpawnPerSecond = 0.5f;

    //map of all types and costs
    public GameObject[] cells;

    public int[] prices;

    [Header("Dynamic")]
    public static Dictionary<GameObject, int> priceMap = new Dictionary<GameObject, int>();
    public static int chemokines{        
        get;
        private set;
    }

    //awake and spawn enemies
    public void Awake(){
        Invoke(nameof(SpawnPathogen), 1f / pathogenSpawnPerSecond);
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
    }

    public void SpawnPathogen()
    {
        if (!spawnPathogens)
        {
            Invoke(nameof(SpawnPathogen), 1f / pathogenSpawnPerSecond);
            return;
        }

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

        // Invoke SpawnPathogen Again
        Invoke(nameof(SpawnPathogen), 1f / pathogenSpawnPerSecond);
    }

    void UpdateGUI() {
        // Show the data in the GUITexts
        uitChemokines.text = "C: " + chemokines;
    }

    public static void chemokineIncrement(int incAmount){
        chemokines += incAmount;
        S.UpdateGUI();
    }
}
