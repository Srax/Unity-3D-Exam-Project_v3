using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyFireSpell : MonoBehaviour
{
    public Rigidbody spell;
    public float projectileSpeed = 10;

    public int pooledAmount = 10;
    public List<Rigidbody> spellPool;


    public float fireRate = 0.5F;
    private float nextFire = 0.0F;

    // Start is called before the first frame update
    void Start()
    {
        spellPool = new List<Rigidbody>();
        for (int i = 0; i < pooledAmount; ++i)
        {
            Rigidbody obj = (Rigidbody)Instantiate(spell);
            obj.gameObject.SetActive(false);
            spellPool.Add(obj);
        }
    }

    public void Fire()
    {

        for (int i = 0; i < spellPool.Count; ++i)
        {
            if (!spellPool[i].gameObject.activeInHierarchy)
            {
                spellPool[i].transform.position = transform.position;
                spellPool[i].transform.rotation = transform.rotation;
                spellPool[i].velocity = transform.forward * projectileSpeed;
                spellPool[i].gameObject.SetActive(true);
                break;
            }
        }

    }
}
