using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmuneCell : MonoBehaviour {
    public int health = 10;
    
    public float speed = 0.5f;

    public float attackSpeed = 1f; //attacks every 1 second

    public float lastAttackTime = 0; //for tracking attack frequency

    public bool followMouse = false; //for placing new cells down

    public SpriteRenderer spriteRenderer;

    public Sprite explodedSprite; // sprite appearance that the white blood cell should change to once it explodes

    public Vector3 doubleSize;


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

    void Start()
    {
        // spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        // doubleSize = this.transform.localScale * 1.2f;
        // doubleSize.x *= 1.3f;
        // doubleSize.y *= 1.3f;
        // doubleSize.z *= 1.3f;

    }

    // Update is called once per frame
    public virtual void Update() {
        if(followMouse){
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        } else {
            Move();

            if (health < 1){
                Destroy(gameObject);
            }
        }
        int num = Main.checkPathogenNum();
        if(num == 0){
            Main.NoPathogens();
        }
    }

    void SwitchSprite()
    {
        spriteRenderer.sprite = explodedSprite;
        // if (this.transform.localScale.x < doubleSize.x && this.transform.localScale.y < doubleSize.y && this.transform.localScale.z < doubleSize.z)
        // {
        //     Vector3 v = this.transform.localScale;
        //     v.x *= 1.3f;
        //     v.y *= 1.3f;
        //     v.z *= 1.3f;
        //     this.transform.localScale = v;
        // }
    }

    public virtual void Move(){
        Vector2 tempPos = pos;
        GameObject target = FindClosestPathogen();
        
        //makes sure target exists
        if (target != null){
            Vector2 targetPos = target.transform.position;
            if((targetPos - pos).sqrMagnitude > 1.7){
                //set step amount according to speed
                var step = Time.deltaTime * speed;
                pos = Vector2.MoveTowards(tempPos, targetPos, step);
            } else { //if already nearby, attack 
                if(attackReady(lastAttackTime)){
                    Pathogen p = target.GetComponent<Pathogen>();
                    //SwitchSprite();
                    p.Attacked();
                    lastAttackTime = Time.time;
                }
                
            }
        }
    }

    public GameObject FindClosestPathogen()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Pathogen");
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

    public virtual void OnMouseOver(){
        if(Input.GetMouseButtonDown(0)){
            if (followMouse) {
                followMouse = false;
                gameObject.layer = 6;
            }
        }
    }

    public virtual void Attacked() {
        if(!followMouse) health--;
    }
}
