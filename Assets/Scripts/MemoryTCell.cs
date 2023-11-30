using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryTCell : ImmuneCell {

    public int maxHealth = 20;

    public GameObject go;
    private GameObject range;

    public void Start(){
        health = maxHealth; //overrides default health value
    }

    public override void Update(){
        if(followMouse){
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        } else {
            if(health < 1){
                Destroy(range);
                Destroy(gameObject);
            }
        }
    }

    public override void OnMouseOver(){
        if(Input.GetMouseButtonDown(0)){
            if (followMouse) {
                followMouse = false;
                gameObject.layer = 6;
                //creates a MEmoryTRange object, which has a larger hitbox, and detects when a pathogen is nearby
                range = Instantiate<GameObject>(go);
                range.transform.position = transform.position;
            }
        }
    }
}
