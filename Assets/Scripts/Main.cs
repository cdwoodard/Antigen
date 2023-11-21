using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {
    //map of all types and costs
    public GameObject[] cells;

    public int[] prices;
    public static Dictionary<GameObject, int> priceMap = new Dictionary<GameObject, int>();
    public static int chemokines{        
        get;
        private set;
    }

    public static List<GameObject> epidermalCells = new List<GameObject>();

    // Start is called before the first frame update
    void Start() {
        chemokines = 0;
        for(int i = 0; i < cells.Length; i++){
            priceMap.Add(cells[i], prices[i]);
        }
    }

    // Update is called once per frame
    void Update() {
        
    }

    public static void chemokineIncrement(int incAmount){
        chemokines += incAmount;
    }
}
