using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Spell_Attack : MonoBehaviour
{

    public Transform spawnPoint;
    public GameObject fireSpell;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            print("Firing");
           Instantiate(fireSpell, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }
    }

}