using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Main : MonoBehaviour {
    static private Main S;

    [Header("Inscribed")]
    public TextMeshProUGUI uitChemokines;


    //map of all types and costs
    public GameObject[] cells;

    public int[] prices;

    [Header("Dynamic")]
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
