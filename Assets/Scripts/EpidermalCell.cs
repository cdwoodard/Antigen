using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EpidermalCell : ImmuneCell {

    public int maxHealth = 100;

    public float reproduceTime = 5; //time in seconds to duplicate
    public float reproduceStartTime;

    public GameObject duplicate;

    public Sprite genSprite;
    public Sprite repSprite;

    public int epidermalIndex;

    //two types of actions an ec can take
    public enum option {
        generate,
        reproduce
    }

    public option mode = option.generate;

    public void Start(){
        health = maxHealth; //overrides default health value
        epidermalIndex =  Main.epidermalCells.Count;
        Main.epidermalCells.Add(gameObject);
        GetComponent<SpriteRenderer>().sprite = genSprite;
        mode = option.generate;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public override void Update(){
        if(health < 1){
            Destroy(gameObject);
            //prevents this cell from being referenced in the epidermal cell list
            Main.epidermalCells[epidermalIndex] = null; //not the best system, but works
        }
        if(mode == option.reproduce){
            if(Time.time >= reproduceStartTime + reproduceTime){
                //duplicate in one of six directions (if no epidermal cell there)
                int hex = Random.Range(0, 6);
                float hexRad = hex * Mathf.PI / 3;
                Vector2 newPos = new Vector2(pos.x + Mathf.Cos(hexRad), pos.y - Mathf.Sin(hexRad));

                //check if position already has epidermal cell
                var checkNum = 0;
                while (checkIfOccupied(newPos) && checkNum <= 6){
                    //if every option has been tried, exit dupe mode
                    hex++;
                    if (hex > 6) hex = 0;
                    hexRad = hex * Mathf.PI / 3;
                    newPos = new Vector2(pos.x + Mathf.Cos(hexRad), pos.y - Mathf.Sin(hexRad));
                    checkNum++;
                }

                if(checkNum <= 6){
                    GameObject go = Instantiate<GameObject>(duplicate);
                    // Set the initial position for the spawned Enemy
                    go.transform.position = newPos;
                } else {
                    swapMode();
                }

                //reset repreoduce clock
                reproduceStartTime = Time.time;
            }
        }
    }

    public override void OnMouseOver(){
        if(Input.GetMouseButtonDown(0)){
            swapMode();
        }
    }

    public override void Attacked(int amount) {
        if (mode == option.generate){
            Main.chemokineIncrement(amount);
        }
        health -= amount;
    }

    //checks through all epidermal cells to see if one is in the position given
    public bool checkIfOccupied(Vector3 newPos){
        for(int i = 0; i < Main.epidermalCells.Count; i++){
            if(Main.epidermalCells[i] != null){ //make sure not a previously deleted index
                if (Vector3.Distance(Main.epidermalCells[i].transform.position, newPos) < 0.5){
                    return true;
                }
            }
        }
        return false;
    }

    //swaps mode to be opposite of current one
    public void swapMode(){
        Scene scene = SceneManager.GetActiveScene();

        if (mode == option.reproduce){
            mode = option.generate;
            GetComponent<SpriteRenderer>().sprite = genSprite;
        } else if (scene.name != "Tutorial"){
            mode = option.reproduce;
            reproduceStartTime = Time.time;
            GetComponent<SpriteRenderer>().sprite = repSprite;
        }
    }
}
