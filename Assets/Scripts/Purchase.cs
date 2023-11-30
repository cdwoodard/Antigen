using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purchase : MonoBehaviour {

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
            if(Main.chemokines >= cost){
                Main.chemokineIncrement(cost * -1);
                //create new immune cell
                GameObject go = Instantiate<GameObject>(type);
                //prevent collisions before being placed
                go.layer = 5;
                //follow the mouse until placed
                go.GetComponent<ImmuneCell>().followMouse = true;
            }
        }
    }
}
