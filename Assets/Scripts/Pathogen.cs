using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pathogen : MonoBehaviour {

    public string type;
    public Sprite image;
    public int health = 10;
    public float speed = 1f;
    public int damage = 1;

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
            //checks if killed
            Scene scene = SceneManager.GetActiveScene();
            if(scene.name == "Tutorial" && Main.tutorialProgress == 6){
                Main.tutorialProgress++;
            }

            Destroy(gameObject); // death
        }
    }

    public virtual void Move(){
        Vector2 tempPos = pos;
        GameObject target = FindClosestEpidermalCell();
        
        //makes sure target exists
        if (target != null){
            Vector2 targetPos = target.transform.position;
            if((targetPos - pos).sqrMagnitude > 1.8){
                //set step amount according to speed
                var step = Time.deltaTime * speed;
                pos = Vector2.MoveTowards(tempPos, targetPos, step);
            } else { //if already nearby, attack 
                if(attackReady(lastAttackTime)){
                    ImmuneCell i = target.GetComponent<ImmuneCell>();
                    i.Attacked(damage);
                    lastAttackTime = Time.time;
                }
                
            }
        }
    }

    public virtual GameObject FindClosestEpidermalCell()
    {
        GameObject[] gos1, gos2;
        gos1 = GameObject.FindGameObjectsWithTag("Immune");
        gos2 = GameObject.FindGameObjectsWithTag("Epidermal");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos1)
        {
            Vector2 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        foreach (GameObject go in gos2)
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

    public virtual void Attacked(int amount) {
        health -= amount;
        var trans = 0.5f;
        var col = gameObject.GetComponent<Renderer>().material.color;
        col.a = trans;
    }

}
