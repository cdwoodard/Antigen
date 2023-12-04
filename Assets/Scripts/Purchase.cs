using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                    go.tag = "Untagged";
                    //follow the mouse until placed
                    go.GetComponent<ImmuneCell>().followMouse = true;

                    //checks if clicked in tutorial
                    Scene scene = SceneManager.GetActiveScene();
                    if(scene.name == "Tutorial" && Main.tutorialProgress == 2 || Main.tutorialProgress == 13 || Main.tutorialProgress == 19){
                        Main.tutorialProgress++;
                    }
                } else {
                    Main.unlockedTCells = true;
                    
                    //checks if clicked in tutorial
                    Scene scene = SceneManager.GetActiveScene();
                    if(scene.name == "Tutorial" && Main.tutorialProgress == 10){
                        Main.tutorialProgress++;
                    }

                    Destroy(gameObject);
                }
            }
        }
    }
}
