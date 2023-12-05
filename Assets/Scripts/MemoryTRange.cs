using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MemoryTRange : MonoBehaviour {
    protected void OnTriggerEnter2D(Collider2D coll) {
        GameObject otherGO = coll.gameObject;

        Pathogen p = otherGO.GetComponent<Pathogen>();
        if (p != null) { // if collided with a pathogen
            //check if pathogen is in encylcopedia already
            if (!Encyclopedia.encyclopedia.ContainsKey(p.type)){
                Encyclopedia.addEntry(p.type, p.image);
            }

            //tutorial code
            Scene scene = SceneManager.GetActiveScene();
            if(scene.name == "Tutorial" && Main.tutorialProgress == 22){
                Main.tutorialProgress++;
            }
        } else {
            //print("memory hit by non-pathogen: " + otherGO.name);
        }
    }

}
