using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purchase : MonoBehaviour {

    public bool needsUnlock; //varies by type, manually set

    //index in Main.prices of what cell can be bought
    public GameObject type;
    public int cost;
    public void Start(){
        cost = Main.priceMap[type];
    }

    public void OnMouseOver(){
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
            // helps users with macs, where difference between middle click and left cick is blurred
        {
            print("clicking");
            cost = Main.priceMap[type];
            print("cost:" + cost);
            if(Main.chemokines >= cost && (Main.unlockedTCells || !needsUnlock)){
                print("reaching?");
                Main.chemokineIncrement(cost * -1);
                if(type != null){
                    //create new immune cell
                    GameObject go = Instantiate<GameObject>(type);
                    print(type.name);
                    print("is go null? : " + go == null);
                    //prevent collisions before being placed
                    go.layer = 5;
                    //follow the mouse until placed
                    go.GetComponent<ImmuneCell>().followMouse = true;
                } else {
                    Main.unlockedTCells = true;
                    Destroy(gameObject);
                }
            }
        }
    }
}
