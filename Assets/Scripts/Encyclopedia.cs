using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Encyclopedia : MonoBehaviour { 

    public SpriteRenderer spriteRenderer;
    public Sprite updatedEncyclopedia; // sprite appearance once a new enemy is found

    // General Sprites
    public GameObject EncyclopediaPanel;
    public GameObject EncyclopediaImage;
    public GameObject BackButton;

    // Chicken Pox Sprites
    public GameObject ChickenPox_Image;
    public GameObject ChickenPox_Text;
    public GameObject ChickenPox_Resistance;

    //Strep Sprites
    public GameObject Strep_Image;
    public GameObject Strep_Text;
    public GameObject Strep_Resistance;

    // Adenovirus Sprites
    public GameObject Aden_Image;
    public GameObject Aden_Text;
    public GameObject Aden_Resistance;

    // Myco Sprites
    public GameObject Myco_Image;
    public GameObject Myco_Text;
    public GameObject Myco_Resistance;

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
        print("hello?");
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

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            print("on Mouse Down?");
            Time.timeScale = 0;
            EncyclopediaPanel.SetActive(true);
            EncyclopediaImage.SetActive(true);
            BackButton.SetActive(true);

            if (encyclopedia.ContainsKey("Chickenpox"))
            {
                ChickenPox_Image.SetActive(true);
                ChickenPox_Text.SetActive(true);
                ChickenPox_Resistance.SetActive(true);
            }

            if (encyclopedia.ContainsKey("Streptococcus"))
            {
                Strep_Image.SetActive(true);
                Strep_Text.SetActive(true);
                Strep_Resistance.SetActive(true);
            }

            if (encyclopedia.ContainsKey("Adenovirus"))
            {
                Aden_Image.SetActive(true);
                Aden_Text.SetActive(true);
                Aden_Resistance.SetActive(true);
            }

            if (encyclopedia.ContainsKey("Mycobacterium ulcerans"))
            {
                Myco_Image.SetActive(true);
                Myco_Text.SetActive(true);
                Myco_Resistance.SetActive(true);
            }

        }
    }

    public void BackToGame()
    {
        EncyclopediaPanel.SetActive(false);
        EncyclopediaImage.SetActive(false);
        BackButton.SetActive(false);

        ChickenPox_Image.SetActive(false);
        ChickenPox_Text.SetActive(false);
        ChickenPox_Resistance.SetActive(false);

        Strep_Image.SetActive(false);
        Strep_Text.SetActive(false);
        Strep_Resistance.SetActive(false);

        Aden_Image.SetActive(false);
        Aden_Text.SetActive(false);
        Aden_Resistance.SetActive(false);

        Myco_Image.SetActive(false);
        Myco_Text.SetActive(false);
        Myco_Resistance.SetActive(false);

        Time.timeScale = 1;


        //tutorial code
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "Tutorial" && Main.tutorialProgress == 23){
            Main.tutorialProgress++;
        }
    }

}
