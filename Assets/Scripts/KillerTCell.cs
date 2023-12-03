using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerTCell : ImmuneCell {

    public GameObject enzymePrefab;


    // Start is called before the first frame update
    void Start() {
        
    }

    public override void Move(){
        Vector2 tempPos = pos;
        GameObject target = FindClosestPathogenInEncyclopedia();
        
        //makes sure target exists
        if (target != null){
            Vector2 targetPos = target.transform.position;

            if(attackReady(lastAttackTime)){

                Enzyme e;

                Vector2 vel = targetPos - pos; 

                e = MakeEnzyme();
                e.vel = vel;

                
                lastAttackTime = Time.time;
            }
        }
    }

    public GameObject FindClosestPathogenInEncyclopedia() {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Pathogen");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos){
            Vector2 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance) {
                Pathogen p = go.GetComponent<Pathogen>();
                if (Encyclopedia.encyclopedia.ContainsKey(p.type)){
                    closest = go;
                    distance = curDistance;
                }
            }
        }
        return closest;
    }

    private Enzyme MakeEnzyme()
    {
        GameObject go;
        go = Instantiate<GameObject>(enzymePrefab);
        Enzyme e = go.GetComponent<Enzyme>();

        Vector2 pos = transform.position;
        e.transform.position = pos;

        return e;
    }
}
