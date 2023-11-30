using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encyclopedia : MonoBehaviour { 

    public SpriteRenderer spriteRenderer;
    public Sprite updatedEncyclopedia; // sprite appearance once a new enemy is found

    public struct entry{
        public Sprite image;
        public int strength;

        public entry(Sprite image, int strength){
            this.image = image;
            this.strength = strength;
        }
    }

    public static Dictionary<string, entry> encyclopedia = new Dictionary<string, entry>();

    private static Encyclopedia E;

    // Start is called before the first frame update
    void Start(){
        E = this;
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    public void SwitchSprite()
    {
        spriteRenderer.sprite = updatedEncyclopedia;
    }

    public static void addEntry(string type, Sprite image){
        encyclopedia.Add(type, new entry(image, 1));
        E.SwitchSprite();
    }

}
