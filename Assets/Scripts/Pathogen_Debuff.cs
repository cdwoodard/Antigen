using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathogen_Debuff : Pathogen {

    public override void Move(){
        Vector2 tempPos = pos;
        GameObject target = FindClosestEpidermalCell();
        
        //makes sure target exists
        if (target != null){
            Vector2 targetPos = target.transform.position;
            if((targetPos - pos).sqrMagnitude > 1.5){
                //set step amount according to speed
                var step = Time.deltaTime * speed;
                pos = Vector2.MoveTowards(tempPos, targetPos, step);
            } else { //if already nearby, attack 
                if(attackReady(lastAttackTime)){
                    ImmuneCell i = target.GetComponent<ImmuneCell>();
                    i.speed = i.speed / 2;
                    i.slowed = true;
                    lastAttackTime = Time.time;
                }
                
            }
        }
    }

    public override GameObject FindClosestEpidermalCell()
    {
        GameObject[] gos2;
        gos2 = GameObject.FindGameObjectsWithTag("Immune");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos2)
        {
            Vector2 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance){
                print("skaskdjaskldj");
                ImmuneCell i = go.GetComponent<ImmuneCell>();
                if(!i.slowed){
                    closest = go;
                    distance = curDistance;
                }
            }
        }
        return closest;
    }
}
