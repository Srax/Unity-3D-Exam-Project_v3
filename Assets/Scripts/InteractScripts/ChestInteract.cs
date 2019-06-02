using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteract : Interactable
{
    Animator anim;
    public LevelLoader lvlLoad;

    void Start()
    {
        anim = GetComponent<Animator>();    
    }
    //Override the Interact() function in the Interactable Script.
    public override void Interact()
    {
        base.Interact();
        PickUp();
    }

    void PickUp()
    {
        print("You got the portal key");
        anim.SetTrigger("openChest"); //Trigger the chest opening animation.
        PlayerPrefs.SetInt("havePortalKey", 1); //Set the PlayerPrefs "havePoralKey" integer to 1.
    }
}
