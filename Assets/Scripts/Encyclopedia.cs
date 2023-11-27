using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encyclopedia : MonoBehaviour { 

    public SpriteRenderer spriteRenderer;
    public Sprite updatedEncyclopedia; // sprite appearance once a new enemy is defeated

    public void SwitchSprite()
    {
        spriteRenderer.sprite = updatedEncyclopedia;
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
