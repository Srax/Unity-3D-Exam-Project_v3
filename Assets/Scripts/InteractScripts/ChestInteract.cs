using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteract : Interactable
{
    Animator anim;
    public GameObject gm;
    public GameObject portal;

    void Start()
    {
        anim = GetComponent<Animator>();
        gm = GameObject.FindGameObjectWithTag("GameMaster");
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
        gm.GetComponent<GameMasterScript>().quest.goal.BossChestInteracted(); //Complete BossKill quest
        PlayerPrefs.SetInt("havePortalKey", 1); //Set the PlayerPrefs "havePoralKey" integer to 1.
        portal.SetActive(true); //Activating the portal
    }
}
