using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerContriller_New : MonoBehaviour
{
    public LayerMask movementMask;
    public Camera cam;
    public Interactable focus;
    PlayerMotor motor;


    private float nextFire = 0.0f;
    private float fireRate = 0.5f;
    public GameObject spell_FireBolt;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    void Update()
    {
        //If leftclick, move
        if(Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                motor.MoveToPoint(hit.point);
                RemoveFocus();
            }
        }

        //if rightclick, interact
        if (Input.GetMouseButton(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if(interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }

        //Cast spell 1
        if (Input.GetKey(KeyCode.Alpha1) && Time.time > nextFire)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                nextFire = Time.time + fireRate;
                shootEnergyBolt(hit);
            }
        }
    }

    void SetFocus(Interactable newFocus)
    {
        if(newFocus != focus)
        {
            if(focus != null)
            {
                focus.OnDefocused();
            }
            focus = newFocus;
            motor.FollowTarget(newFocus);
        }

        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if(focus != null)
        {
            focus.OnDefocused();
        }
        focus = null;
        motor.StopFollowingTarget();
    }



    void shootEnergyBolt(RaycastHit hit)
    {
        float manaCost = 10f;
        CharacterStats ps = gameObject.GetComponent<CharacterStats>();
        EnergyBallSpell ebs = spell_FireBolt.GetComponent<EnergyBallSpell>();
        if(ps != null)
        {
            if(ps.currentPlayerMana >= manaCost)
            {
                ps.DecreaseMana(manaCost); //Remove X amount of mana from the player
                GameObject spellGo = (GameObject)Instantiate(spell_FireBolt, transform.position, transform.rotation);
                EnergyBallSpell fb = spellGo.GetComponent<EnergyBallSpell>();

                if (fb != null)
                {
                    fb.Seek(hit.point);
                }
            } else
            {
                print("Not enough mana");
            }
        } else
        {
            print("MISSING PlayerStats script");
        }
    }
}
