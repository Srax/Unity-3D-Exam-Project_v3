using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalInteract : Interactable
{
    Animator anim;
    public GameObject gm;
    public GameObject lvlLoad;

    void Start()
    {
        anim = GetComponent<Animator>();
        gm = GameObject.FindGameObjectWithTag("GameMaster");
    }
    //Override the Interact() function in the Interactable Script.
    public override void Interact()
    {
        base.Interact();
        UsePortal();
    }

    void UsePortal()
    {
        if(PlayerPrefs.GetInt("havePortalKey") == 1)
        {
            lvlLoad.GetComponent<LevelLoader>().LoadLevel("WinScreen");
        }
    }
}
