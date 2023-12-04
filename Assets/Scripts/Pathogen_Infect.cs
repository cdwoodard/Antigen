using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathogen_Infect : Pathogen {
    public int count = 0;
    public GameObject duplicate;
    public override void Move(){
        Vector2 tempPos = pos;
        GameObject target = FindClosestEpidermalCell();
        
        //makes sure target exists
        if (target != null){
            Vector2 targetPos = target.transform.position;
            if((targetPos - pos).sqrMagnitude > 1.5){
                count = 0;
                //set step amount according to speed
                var step = Time.deltaTime * speed;
                pos = Vector2.MoveTowards(tempPos, targetPos, step);
            } else { //if already nearby, attack 
                //if has "attacked" 20 times, kill cell and duplicate
                if (count >= 20){
                    ImmuneCell i = target.GetComponent<ImmuneCell>();
                    i.Attacked(i.health);
                    for(int j = 0; j < 10; j++){
                        GameObject go = Instantiate<GameObject>(duplicate);
                        go.transform.position = transform.position;
                        count = 0;
                    }
                } else if(attackReady(lastAttackTime)){
                    count++;
                    lastAttackTime = Time.time;
                }
            }
        }
    }

    public override GameObject FindClosestEpidermalCell()
    {
        GameObject[] gos2;
        gos2 = GameObject.FindGameObjectsWithTag("Epidermal");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos2)
        {
            Vector2 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance){
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
