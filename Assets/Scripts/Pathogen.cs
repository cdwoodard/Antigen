using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathogen : MonoBehaviour {

    public string type;
    public Sprite image;
    public int health = 10;
    public float speed = 1f;

    public float attackSpeed = 1f; //attacks every 1 second

    public float lastAttackTime = 0; //for tracking attack frequency

    public SpriteRenderer spriteRenderer;

    public Vector2 pos {
        get
        {
            return this.transform.position;
        }
        set
        {
            this.transform.position = value;
        }
    }

    // Update is called once per frame
    void Update() {
        Move();

        if(health < 1){
            Destroy(gameObject); // death
        }
    }

    public virtual void Move(){
        Vector2 tempPos = pos;
        GameObject target = FindClosestImmuneCell();
        
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
                    i.Attacked();
                    lastAttackTime = Time.time;
                }
                
            }
        }
    }

    public GameObject FindClosestImmuneCell()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Immune");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector2 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    public bool attackReady(float lastAttackTime){
        if(Time.time >= lastAttackTime + attackSpeed){
            return true;
        }
        return false;
    }

    public virtual void Attacked() {
        health--;
        var trans = 0.5f;
        var col = gameObject.GetComponent<Renderer>().material.color;
        col.a = trans;
    }

}
