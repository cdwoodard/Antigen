using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enzyme : MonoBehaviour {

    public Rigidbody2D rigid;

    // Start is called before the first frame update
    void Awake() {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {

    }

    public Vector2 vel{
        get { return rigid.velocity; }
        set { rigid.velocity = value; }
    }

    protected void OnTriggerEnter2D(Collider2D coll) {
        GameObject otherGO = coll.gameObject;

        Pathogen p = otherGO.GetComponent<Pathogen>();
        if (p != null) { // if collided with a pathogen
            //check if pathogen is in encylcopedia already
            if (Encyclopedia.encyclopedia.ContainsKey(p.type)){
                int strength = Encyclopedia.encyclopedia[p.type].strength;
                p.Attacked(strength);
                Destroy(gameObject);
            }
        } else {
            print("enzyme hit a non-encyclopedia-pathogen: " + otherGO.name);
        }
    }
}
